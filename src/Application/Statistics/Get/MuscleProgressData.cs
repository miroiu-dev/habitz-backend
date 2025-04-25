using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Statistics;

namespace Application.Statistics.Get;
public sealed record MuscleProgressData(
    MuscleProgress? Chest,
    MuscleProgress? Shoulder,
    MuscleProgress? LeftBiceps,
    MuscleProgress? RightBiceps
);
