using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.BodyCompositionLogs.GetNewest;
public sealed record NewestQuery(int UserId) : IQuery<NewestResponse>;
