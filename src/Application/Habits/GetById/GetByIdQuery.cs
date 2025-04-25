using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.Habits.GetById;
public sealed record GetByIdQuery(int HabitId, int UserId): IQuery<HabitResponse>;
