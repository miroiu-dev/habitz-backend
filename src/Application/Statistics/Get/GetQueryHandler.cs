using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Habits.GetById;
using Domain.BodyMeasurementLogs;
using Domain.Statistics;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Statistics.Get;
internal class GetQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetQuery, GetResponse>
{
    public async Task<Result<GetResponse>> Handle(GetQuery request, CancellationToken cancellationToken)
    {
        User user = await context.Users
             .Include(u => u.BodyMeasurementLogs.OrderByDescending(b => b.CreatedAt))
             .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<GetResponse>(UserErrors.NotFound(request.UserId));
        }

        MuscleProgressData muscleProgress = CalculateMuscleProgress(user.BodyMeasurementLogs);

        return Result.Success(new GetResponse(
            user.GetCalorieBreakdown(),
            user.GetMacronutrientTargets(),
            muscleProgress));
    }

    private MuscleProgressData CalculateMuscleProgress(List<BodyMeasurementLog> logs)
    {
        if (logs == null || !logs.Any())
        {
            return new MuscleProgressData(null, null, null, null);
        }

        BodyMeasurementLog mostRecent = logs.OrderByDescending(l => l.CreatedAt).First();
        BodyMeasurementLog oldest = logs.Count > 1 ? logs.OrderBy(l => l.CreatedAt).First() : mostRecent;

        decimal chestDifference = mostRecent.Chest - oldest.Chest;
        decimal shoulderDifference = mostRecent.Shoulder - oldest.Shoulder;
        decimal leftBicepsDifference = mostRecent.LeftBiceps - oldest.LeftBiceps;
        decimal rightBicepsDifference = mostRecent.RightBiceps - oldest.RightBiceps;

        decimal chestPercentage = CalculatePercentageChange(oldest.Chest, mostRecent.Chest);
        decimal shoulderPercentage = CalculatePercentageChange(oldest.Shoulder, mostRecent.Shoulder);
        decimal leftBicepsPercentage = CalculatePercentageChange(oldest.LeftBiceps, mostRecent.LeftBiceps);
        decimal rightBicepsPercentage = CalculatePercentageChange(oldest.RightBiceps, mostRecent.RightBiceps);

        var chestProgress = new MuscleProgress(
            oldest.Chest,
            mostRecent.Chest,
            chestDifference,
            chestPercentage,
            oldest.CreatedAt,
            mostRecent.CreatedAt
        );

        var shoulderProgress = new MuscleProgress(
            oldest.Shoulder,
            mostRecent.Shoulder,
            shoulderDifference,
            shoulderPercentage,
            oldest.CreatedAt,
            mostRecent.CreatedAt
        );

        var leftBicepsProgress = new MuscleProgress(
            oldest.LeftBiceps,
            mostRecent.LeftBiceps,
            leftBicepsDifference,
            leftBicepsPercentage,
            oldest.CreatedAt,
            mostRecent.CreatedAt
        );

        var rightBicepsProgress = new MuscleProgress(
            oldest.RightBiceps,
            mostRecent.RightBiceps,
            rightBicepsDifference,
            rightBicepsPercentage,
            oldest.CreatedAt,
            mostRecent.CreatedAt
        );

        return new MuscleProgressData(
            chestProgress,
            shoulderProgress,
            leftBicepsProgress,
            rightBicepsProgress
        );
    }

    private decimal CalculatePercentageChange(decimal oldValue, decimal newValue)
    {
        if (oldValue == 0)
        {
            return 0;
        }

        return Math.Round((newValue - oldValue) / oldValue * 100, 2);
    }
}
