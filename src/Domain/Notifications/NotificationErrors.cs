using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel;

namespace Domain.Notifications;
public static class NotificationErrors
{

    public static Error NoHabitFound(int habitId) => Error.NotFound(
       "Users.NoHabitFound",
       $"No habit found with id = '{habitId}'");
}
