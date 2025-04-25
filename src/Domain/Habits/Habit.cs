using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.HabitLogs;
using Domain.HabitSchedules;
using Domain.Notifications;
using Domain.Users;

namespace Domain.Habits;

public class Habit
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public string Color { get; set; }
    public List<HabitSchedule> HabitSchedules { get; set; } = [];
    public List<HabitLog> HabitLogs { get; set; } = [];
    public int UserId { get; set; }
    public TimeOnly? Reminder { get; set; }
    public User User { get; set; }

    public List<Notification> Notifications { get; set; }
}
