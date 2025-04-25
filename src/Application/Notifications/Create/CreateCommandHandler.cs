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
using Domain.Notifications;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Notifications.Create;
internal sealed class CreateCommandHandler(IApplicationDbContext context) : ICommandHandler<CreateCommand, CreateResponse>
{
    public async Task<Result<CreateResponse>> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        Habit? habit = await context.Habits.FirstOrDefaultAsync(h => h.Id == request.HabitId && h.UserId == request.UserId, cancellationToken);
    
        if(habit is null)
        {
            return Result.Failure<CreateResponse>(NotificationErrors.NoHabitFound(request.HabitId));
        }

        var notification = new Notification
        {
            CreatedAt = DateTime.UtcNow,
            Description = request.Description,
            Habit = habit,
            HabitId = request.HabitId,
            Title = request.Title,
        };

        await context.Notifications.AddAsync(notification, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(new CreateResponse(notification.Id));
    }
}
