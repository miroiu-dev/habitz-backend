using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.BodyMeasurementLogs.Create;
using Domain.BodyMeasurementLogs;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.BodyMeasurementLogs.Delete;
internal class DeleteCommandHandler(IApplicationDbContext context) : ICommandHandler<DeleteCommand, DeleteResponse>
{
    public async Task<Result<DeleteResponse>> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        BodyMeasurementLog? log = await context.BodyMeasurementLogs.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId, cancellationToken);

        if(log is null)
        {
            return Result.Failure<DeleteResponse>(BodyMeasurementLogErrors.NotFound(request.Id));
        }

        context.BodyMeasurementLogs.Remove(log);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(new DeleteResponse(log.Id));
    }
}
