using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel;

namespace Domain.HabitLogs;
public static class HabitLogErrros
{
    public static Error NotFound(int habitLogId) => Error.NotFound(
       "HabitLogs.NotFound",
       $"The habit log with the Id = '{habitLogId}' was not found.");

    public static Error Unauthorized(int habitLogId) => Error.Unauthorized(
       "HabitLogs.Unauthorized",
       $"You are not authorized to access the habit log with Id = '{habitLogId}'.");

    public static Error NotAvailableForDate(DateTime date) => Error.Validation(
       "HabitLogs.NotAvailableForDate",
       $"This habit log from {date.ToShortDateString()} cannot be modified anymore.");

    public static Error NotScheduledForToday(int habitId) => Error.Validation(
       "HabitLogs.NotScheduledForToday",
       $"The habit with Id = '{habitId}' is not scheduled for today.");
}
