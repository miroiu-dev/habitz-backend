using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BodyCompositionLogs.GetNewest;
public sealed record NewestResponse(decimal Neck, decimal Shoulder, decimal LeftBiceps, decimal RightBiceps, decimal Chest, decimal Waist, decimal Abs, decimal Hip, decimal LeftTigh, decimal RightTigh, decimal LeftCalf, decimal RightCalf, decimal WaistHipRatio);
