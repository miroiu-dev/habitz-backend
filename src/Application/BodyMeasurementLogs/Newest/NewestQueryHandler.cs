using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Habits.GetById;
using Domain.BodyMeasurementLogs;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SharedKernel;

namespace Application.BodyCompositionLogs.GetNewest;
internal class NewestQueryHandler(IApplicationDbContext context)
    : IQueryHandler<NewestQuery, NewestResponse>
{
    public async Task<Result<NewestResponse>> Handle(NewestQuery request, CancellationToken cancellationToken)
    {
        BodyMeasurementLog? log = await context.BodyMeasurementLogs.Where(x => x.UserId == request.UserId).OrderByDescending(log => log.CreatedAt).FirstOrDefaultAsync(cancellationToken);

        if (log is null)
        {
            return Result.Success(new NewestResponse(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
        }

        return Result.Success(new NewestResponse(log.Neck, log.Shoulder, log.LeftBiceps, log.RightBiceps, log.Chest, log.Waist, log.Abs, log.Hip, log.LeftTigh, log.RightTigh, log.LeftCalf, log.RightCalf, log.WaistHipRatio));
    }
}
