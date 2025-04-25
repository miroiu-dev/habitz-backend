using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.BodyMeasurementLogs.Get;

public sealed record GetQuery(int UserId, DateTime Date): IQuery<GetResponse>;
