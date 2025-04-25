using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.History;
public class RowData
{
    public int Day { get; set; }

    public List<RowCell> Cells { get; set; } = [];
}
