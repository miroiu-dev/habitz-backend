using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Habits.Create;
using Domain.BodyMeasurementLogs;
using SharedKernel;

namespace Application.BodyMeasurementLogs.Create;

internal sealed class CreateCommandHandler(IApplicationDbContext context) : ICommandHandler<CreateCommand, CreateResponse>
{
    public async Task<Result<CreateResponse>> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var log = new BodyMeasurementLog
        {
            Abs = request.Abs,
            Neck = request.Neck,
            Shoulder = request.Shoulder,
            LeftBiceps = request.LeftBiceps,
            RightBiceps = request.RightBiceps,
            Chest = request.Chest,
            Waist = request.Waist,
            Hip = request.Hip,
            LeftTigh = request.LeftTigh,
            RightTigh = request.RightTigh,
            LeftCalf = request.LeftCalf,
            RightCalf = request.RightCalf,
            CreatedAt = DateTime.UtcNow,
            UserId = request.UserId,
            WaistHipRatio = request.Waist == 0 ? 0 : request.Waist / request.Hip
        };

        await context.BodyMeasurementLogs.AddAsync(log, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(new CreateResponse(log.Id));
    }
}
