using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Habits.Create;
using Domain.Habits;
using Domain.HabitSchedules;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Habits.Delete;
internal sealed class DeleteCommandHandler(IApplicationDbContext context)
    : ICommandHandler<DeleteCommand, DeleteResponse>
{
    public async Task<Result<DeleteResponse>> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        Habit? habit = await context.Habits
            .Include(h => h.HabitLogs)
            .Include(h => h.HabitSchedules)
            .FirstOrDefaultAsync(h => h.Id == request.HabitId && h.UserId == request.UserId, cancellationToken);

        if (habit is null)
        {
            return Result.Failure<DeleteResponse>(HabitErrors.NotFound(request.HabitId));
        }

        context.HabitLogs.RemoveRange(habit.HabitLogs);
        context.HabitSchedules.RemoveRange(habit.HabitSchedules);
        context.Habits.Remove(habit);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(new DeleteResponse(habit.Id));
    }
}
