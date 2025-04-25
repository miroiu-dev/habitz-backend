using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.HabitLogs;
using Domain.Habits;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.HabitLogs.Get;

internal sealed class GetQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetQuery, GetResponse>
{
    public async Task<Result<GetResponse>> Handle(GetQuery request, CancellationToken cancellationToken)
    {
        DateTime todayUtc = DateTime.UtcNow.Date;
        DateTime tomorrowUtc = todayUtc.AddDays(1);
        DayOfWeek currentDayOfWeek = todayUtc.DayOfWeek;

        List<Habit> habits = await context.Habits
            .Where(h => h.UserId == request.UserId &&
                        h.HabitSchedules.Any(hs => (DayOfWeek)hs.DayOfWeek == currentDayOfWeek))
            .ToListAsync(cancellationToken);

        var habitIds = habits.Select(x => x.Id).ToList();

        List<HabitLog> habitLogs = await context.HabitLogs
            .Include(hl => hl.Habit)
            .Where(hl => habitIds.Contains(hl.HabitId) &&
                         hl.CreatedAt >= todayUtc && hl.CreatedAt < tomorrowUtc)
            .ToListAsync(cancellationToken);

        var habitsWithoutLogs = habits
            .Where(h => !habitLogs.Any(log => log.HabitId == h.Id))
            .ToList();

        if (habitsWithoutLogs.Any())
        {
            var newLogs = habitsWithoutLogs.Select(habit => new HabitLog
            {
                HabitId = habit.Id,
                CreatedAt = todayUtc,
                IsCompleted = false
            }).ToList();

            await context.HabitLogs.AddRangeAsync(newLogs, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            habitLogs.AddRange(newLogs);
        }

        return Result.Success(new GetResponse(
            [.. habitLogs.Select(hl => new HabitLogResponse(
                hl.Id,
                hl.HabitId,
                hl.Habit.Icon,
                hl.Habit.Color,
                hl.Habit.Name,
                hl.Habit.Reminder,
                hl.IsCompleted))]));
    }
}
