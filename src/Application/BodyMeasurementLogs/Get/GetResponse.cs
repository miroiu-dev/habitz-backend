using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BodyMeasurementLogs.Get;
public sealed record GetResponse(List<BodyMeasurementLogResponse> Measurements, List<DateTime> AvailableDates);
