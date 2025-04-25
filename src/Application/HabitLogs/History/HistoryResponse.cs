using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.History;
using Domain.Statistics;

namespace Application.HabitLogs.History;
public record HistoryResponse(List<HeaderCell> Header, List<RowData> Rows, DateTime Date);
