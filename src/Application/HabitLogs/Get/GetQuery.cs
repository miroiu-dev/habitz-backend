using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.HabitLogs.Get;
public record GetQuery(int UserId) : IQuery<GetResponse>;
