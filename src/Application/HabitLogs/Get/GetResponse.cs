using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Habits;

namespace Application.HabitLogs.Get;
public sealed record GetResponse(List<HabitLogResponse> Data);
