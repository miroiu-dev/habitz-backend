using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Users.GetById;
using Domain.HabitLogs;
using Domain.Habits;
using Domain.History;
using Domain.Statistics;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.HabitLogs.History;
internal sealed class HistoryQueryHandler(IApplicationDbContext context)
    : IQueryHandler<HistoryQuery, HistoryResponse>
{
    public const int MAX_PAD = 9;
    public async Task<Result<HistoryResponse>> Handle(HistoryQuery query, CancellationToken cancellationToken)
    {
        DateTime today = DateTime.UtcNow.Date;
        var startOfMonth = new DateTime(today.Year, today.Month, 1, 0, 0, 0, DateTimeKind.Utc);
        var daysInRange = Enumerable.Range(0, (today - startOfMonth).Days + 1)
                                    .Select(offset => startOfMonth.AddDays(offset))
                                    .ToList();

        List<Habit> habits = await context.Habits
            .Where(h => h.UserId == query.UserId)
            .ToListAsync(cancellationToken);

        List<HabitLog> logs = await context.HabitLogs
            .Where(l => l.Habit.UserId == query.UserId &&
                        l.CreatedAt.Date >= startOfMonth &&
                        l.CreatedAt.Date <= today)
            .ToListAsync(cancellationToken);

        var logLookup = logs.ToDictionary(
            log => (log.CreatedAt.Date, log.HabitId),
            log => log
        );

        var header = new List<HeaderCell>
        {
            new()
        };

        foreach (Habit habit in habits)
        {
            header.Add(new HeaderCell
            {
                Icon = habit.Icon,
                Id = habit.Id,
            });
        }

        int padHeaders = MAX_PAD + 1 - header.Count;

        if (padHeaders > 0)
        {
            header.AddRange(Enumerable.Range(0, padHeaders).Select(_ => new HeaderCell()));
        }

        var rows = new List<RowData>();

        foreach (DateTime day in daysInRange)
        {
            var row = new RowData
            {
                Day = day.Day
            };

            foreach (Habit habit in habits)
            {
                (DateTime day, int Id) logKey = (day, habit.Id);

                if (logLookup.TryGetValue(logKey, out HabitLog? log))
                {
                    row.Cells.Add(new RowCell
                    {
                        Status = log.IsCompleted ? CellStatus.Complete : CellStatus.Incomplete,
                        Color = habit.Color
                    });
                }
                else
                {
                    row.Cells.Add(new RowCell
                    {
                        Status = CellStatus.Incomplete
                    });
                }
            }

            int padCells = MAX_PAD - row.Cells.Count;

            if(padCells > 0)
            {
                row.Cells.AddRange(Enumerable.Range(0, padCells).Select(_ => new RowCell { Status = CellStatus.Empty }));
            }

            rows.Add(row);
        }

        rows = [.. rows.OrderByDescending(r => r.Day)];
        var response = new HistoryResponse(header, rows, DateTime.UtcNow);

        return Result.Success(response);
    }
}
