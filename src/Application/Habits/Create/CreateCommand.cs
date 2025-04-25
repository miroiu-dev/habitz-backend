using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Application.Abstractions.Messaging;

namespace Application.Habits.Create;
public record class CreateCommand(int UserId, string Name, string Icon, string Color, TimeOnly? Reminder, List<int> Schedules) : ICommand<CreateResponse>;
