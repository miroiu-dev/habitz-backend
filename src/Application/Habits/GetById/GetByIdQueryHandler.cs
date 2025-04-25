using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Users.GetById;
using Domain.Habits;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Habits.GetById;
internal sealed class GetByIdQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetByIdQuery, HabitResponse>
{
    public async Task<Result<HabitResponse>> Handle(GetByIdQuery query, CancellationToken cancellationToken)
    {
        Habit? habit = await context.Habits
           .Where(h => h.Id == query.HabitId && h.UserId == query.UserId)
           .Include(h => h.HabitSchedules)
           .Include(h => h.HabitLogs)
           .FirstOrDefaultAsync(cancellationToken);

        if (habit is null)
        {
            return Result.Failure<HabitResponse>(HabitErrors.NotFound(query.HabitId));
        }

        var logs = habit.HabitLogs
            .OrderBy(l => l.CreatedAt.Date)
            .Select(l => new { l.CreatedAt.Date, l.IsCompleted })
            .ToList();

        var completedDates = logs
            .Where(l => l.IsCompleted)
            .Select(l => l.Date.Date)
            .Distinct()
            .ToList();

        int currentStreak = CalculateCurrentStreak(completedDates);

        int recordStreak = CalculateRecordStreak(completedDates);

        string trend = CalculateTrend(completedDates);

        var scheduleDays = habit.HabitSchedules.Select(s => s.DayOfWeek).ToList();

        var response = new HabitResponse(
               habit.Id,
               habit.Name,
               habit.Icon,
               habit.Color,
               [.. completedDates.Distinct()],
               currentStreak,
               recordStreak,
               trend,
               scheduleDays
        );

        return Result.Success(response);
    }

    private int CalculateCurrentStreak(List<DateTime> completedDates)
    {
        completedDates = [.. completedDates.OrderByDescending(d => d)];

        int streak = 0;
        DateTime today = DateTime.UtcNow.Date;

        foreach (DateTime date in completedDates)
        {
            if (date == today || date == today.AddDays(-streak))
            {
                streak++;
            }
            else
            {
                break;
            }
        }

        return streak;
    }

    private int CalculateRecordStreak(List<DateTime> completedDates)
    {
        completedDates = [.. completedDates.Order()];

        int maxStreak = 0, currentStreak = 0;
        DateTime? previous = null;

        foreach (DateTime date in completedDates)
        {
            if (previous == null || date == previous.Value.AddDays(1))
            {
                currentStreak++;
            }
            else
            {
                currentStreak = 1;
            }
            maxStreak = Math.Max(maxStreak, currentStreak);
            previous = date;

        }
        return maxStreak;
    }

    private string CalculateTrend(List<DateTime> completedDates)
    {
        DateTime today = DateTime.UtcNow.Date;

        int countLast7 = completedDates.Count(d => d >= today.AddDays(-6) && d <= today);
        int countPrev7 = completedDates.Count(d => d >= today.AddDays(-13) && d <= today.AddDays(-7));

        if (countLast7 > countPrev7)
        {
            return "Trending up";
        }

        if (countLast7 < countPrev7)
        {
            return "Trending down";
        }

        return "Steady";
    }
}
