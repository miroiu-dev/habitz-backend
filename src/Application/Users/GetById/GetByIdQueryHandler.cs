using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Users.GetById;

internal sealed class GetByIdQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetByIdQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetByIdQuery query, CancellationToken cancellationToken)
    {
        UserResponse? user = await context.Users
            .Where(u => u.Id == query.UserId)
            .Select(u => new UserResponse
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.Email,
                ActivityLevel = u.ActivityLevel,
                Age = u.Age,
                Gender = u.Gender,
                Goal = u.Goal,
                GoalWeight = u.GoalWeight,
                Height = u.Height,
                WeeklyGoal = u.WeeklyGoal,
                Weight = u.Weight,
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserResponse>(UserErrors.NotFound(query.UserId));
        }

        return user;
    }
}
