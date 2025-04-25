using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Habits;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Habits;

internal sealed class HabitConfiguration : IEntityTypeConfiguration<Habit>
{
    public void Configure(EntityTypeBuilder<Habit> builder)
    {
        builder.HasKey(h => h.Id);

        builder.Property(h => h.Name).IsRequired();
        builder.Property(h => h.Icon).IsRequired();
        builder.Property(h => h.Color).IsRequired();
        builder.Property(h => h.Reminder);

        builder.HasMany(h => h.HabitSchedules).WithOne();
        builder.HasMany(h => h.HabitLogs).WithOne();
        builder.HasOne(h => h.User).WithMany(u => u.Habits).HasForeignKey(h => h.UserId);  
    }
}
