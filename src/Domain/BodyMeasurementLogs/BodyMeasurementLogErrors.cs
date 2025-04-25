using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel;

namespace Domain.BodyMeasurementLogs;
public static class BodyMeasurementLogErrors
{
    public static Error NotFound(int bodyCompositionLogId) => Error.NotFound(
     "HabitLogs.NotFound",
     $"The body composition log with the Id = '{bodyCompositionLogId}' was not found.");
}
