using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Users.LoginWithRefreshToken;
using Domain.Habits;
using Domain.HabitSchedules;
using Domain.RefreshTokens;
using SharedKernel;

namespace Application.Habits.Create;
internal sealed class CreateCommandHandler(IApplicationDbContext context) : ICommandHandler<CreateCommand, CreateResponse>
{
    public async Task<Result<CreateResponse>> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        if(context.Habits.Any(h => h.Name == request.Name && h.UserId == request.UserId))
        {
            return Result.Failure<CreateResponse>(HabitErrors.AlreadyExists(request.Name));
        }

        var habit = new Habit
        {
            Color = request.Color,
            Icon = request.Icon,
            Name = request.Name,
            Reminder = request.Reminder,
            UserId = request.UserId,
        };

        var habitSchedules = request.Schedules
            .Select(dayOfWeek => new HabitSchedule { Habit = habit, DayOfWeek = dayOfWeek })
            .ToList();
        habit.HabitSchedules = habitSchedules;

        await context.Habits.AddAsync(habit, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(new CreateResponse(habit.Id));
    }
}
