using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.HabitLogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Habits;
public class HabitLogConfiguration : IEntityTypeConfiguration<HabitLog>
{
    public void Configure(EntityTypeBuilder<HabitLog> builder)
    {
        builder.HasKey(h => h.Id);

        builder.Property(h => h.IsCompleted).IsRequired();
        builder.Property(h => h.HabitId).IsRequired();
        builder.Property(h => h.CreatedAt).IsRequired();
        builder.Property(h => h.UpdatedAt);

        builder.HasOne(h => h.Habit).WithMany(h => h.HabitLogs).HasForeignKey(h => h.HabitId);
    }
}
