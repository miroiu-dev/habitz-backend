using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BodyMeasurementLogs.Get;
public record class BodyMeasurementLogResponse(int Id, decimal Neck, decimal Shoulder, decimal LeftBiceps, decimal RightBiceps, decimal Chest, decimal Waist, decimal Abs, decimal Hip, decimal LeftTigh, decimal RightTigh, decimal LeftCalf, decimal RightCalf, decimal WaistToHipRatio, DateTime CreatedAt);
