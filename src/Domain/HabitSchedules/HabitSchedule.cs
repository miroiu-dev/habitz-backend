using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Habits;

namespace Domain.HabitSchedules;

public class HabitSchedule
{
    public int Id { get; set; }
    public int DayOfWeek { get; set; }
    public int HabitId { get; set; }
    public Habit Habit { get; set; }
}
