using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.BodyMeasurementLogs.Create;
public sealed record CreateCommand(int UserId, decimal Neck, decimal Shoulder, decimal LeftBiceps, decimal RightBiceps, decimal Chest, decimal Waist, decimal Abs, decimal Hip, decimal LeftTigh, decimal RightTigh, decimal LeftCalf, decimal RightCalf): ICommand<CreateResponse>;
