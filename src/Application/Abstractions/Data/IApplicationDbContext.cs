using Domain.BodyMeasurementLogs;
using Domain.HabitLogs;
using Domain.Habits;
using Domain.HabitSchedules;
using Domain.Notifications;
using Domain.RefreshTokens;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<RefreshToken> RefreshTokens { get; }
    DbSet<HabitLog> HabitLogs { get; }
    DbSet<Habit> Habits { get; }
    DbSet<HabitSchedule> HabitSchedules { get; }
    DbSet<Notification> Notifications { get; }
    DbSet<BodyMeasurementLog> BodyMeasurementLogs { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
