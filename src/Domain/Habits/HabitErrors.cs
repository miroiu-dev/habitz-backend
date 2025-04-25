using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel;

namespace Domain.Habits;
public static class HabitErrors
{
    public static Error NotFound(int habitId) => Error.NotFound(
        "Habit.NotFound",
        $"The habit with the Id = '{habitId}' was not found.");

    public static Error AlreadyExists(string name) => Error.Conflict(
      "Habit.AlreadyExists",
      $"The habit with the name = '{name}' already exists.");
}
