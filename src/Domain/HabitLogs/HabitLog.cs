using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Habits;

namespace Domain.HabitLogs;
public class HabitLog
{
    public int Id { get; set; }
    public int HabitId { get; set; }
    public Habit Habit { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsCompleted { get; set; }
}
