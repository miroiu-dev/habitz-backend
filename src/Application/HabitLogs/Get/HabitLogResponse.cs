using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.HabitLogs.Get;
public sealed record HabitLogResponse(int Id, int HabitId, string Icon, string Color, string Name, TimeOnly? Reminder, bool IsCompleted);
