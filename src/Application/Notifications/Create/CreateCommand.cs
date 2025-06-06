﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.Notifications.Create;
public sealed record CreateCommand(int HabitId, int UserId, string Title, string  Description): ICommand<CreateResponse>;
