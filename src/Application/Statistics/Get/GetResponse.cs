using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Users;
using static Application.Statistics.Get.GetQueryHandler;

namespace Application.Statistics.Get;
public sealed record GetResponse(CalorieBreakdown CalorieBreakdown, Macros MacroNutrientTargets, MuscleProgressData MuscleProgress);
