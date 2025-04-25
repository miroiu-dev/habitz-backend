using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.BodyMeasurementLogs.Delete;
public sealed record DeleteCommand(int Id, int UserId) : ICommand<DeleteResponse>;
