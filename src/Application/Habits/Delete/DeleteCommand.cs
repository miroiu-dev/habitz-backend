using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Application.Abstractions.Messaging;

namespace Application.Habits.Delete;
public sealed record DeleteCommand(int HabitId, int UserId): ICommand<DeleteResponse>;
