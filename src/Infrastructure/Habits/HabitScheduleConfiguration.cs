using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.HabitSchedules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Habits;

internal sealed class HabitScheduleConfiguration : IEntityTypeConfiguration<HabitSchedule>
{
    public void Configure(EntityTypeBuilder<HabitSchedule> builder)
    {
        builder.HasKey(h => h.Id);

        builder.Property(h => h.DayOfWeek).IsRequired();
    
        builder.HasOne(x => x.Habit).WithMany(h => h.HabitSchedules).HasForeignKey(h => h.HabitId);
    }
}
