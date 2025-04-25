using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.HabitLogs.History;
public sealed record HistoryQuery(int UserId): IQuery<HistoryResponse>;
