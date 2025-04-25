using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Habits;
using Serilog;

namespace Application.Habits.GetById;

public sealed record HabitResponse(int Id, string Name, string Icon, string Color, List<DateTime> Logs, int CurrentStreak, int RecordStreak, string Trend, List<int> ScheduleDays);
