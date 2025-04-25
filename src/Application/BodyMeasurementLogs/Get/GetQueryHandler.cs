using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.BodyCompositionLogs.GetNewest;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SharedKernel;

namespace Application.BodyMeasurementLogs.Get;
internal class GetQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetQuery, GetResponse>
{
    public async Task<Result<GetResponse>> Handle(GetQuery request, CancellationToken cancellationToken)
    {
        List<BodyMeasurementLogResponse> logs = await context.BodyMeasurementLogs.Where(x => x.CreatedAt.Date == request.Date.Date && x.UserId == request.UserId).OrderByDescending(log => log.CreatedAt).Select(log => new BodyMeasurementLogResponse(log.Id, log.Neck, log.Shoulder, log.LeftBiceps, log.RightBiceps, log.Chest, log.Waist, log.Abs, log.Hip, log.LeftTigh, log.RightTigh, log.LeftCalf, log.RightCalf, log.WaistHipRatio, log.CreatedAt)).ToListAsync(cancellationToken);

        return Result.Success(new GetResponse(logs));
    }
}
