using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Habits.GetById;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Notifications.Get;
internal class GetQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetQuery, GetResponse>
{
    public async Task<Result<GetResponse>> Handle(GetQuery request, CancellationToken cancellationToken)
    {
        List<NotificationResponse> notifications = await context.Notifications.Where(x => x.Habit.UserId == request.UserId).Select(x => new NotificationResponse(x.Id, x.Habit.Icon, x.Title, x.Description, x.CreatedAt)).ToListAsync(cancellationToken);

        return Result.Success(new GetResponse(notifications));
    }
}
