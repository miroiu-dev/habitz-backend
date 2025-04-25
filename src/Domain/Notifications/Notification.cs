using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Habits;

namespace Domain.Notifications;
public class Notification
{
    public int Id {  get; set; }
    public int HabitId {  get; set; }
    public string Title {  get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public Habit Habit {  get; set; }
}
