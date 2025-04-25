using Application.Abstractions.Data;
using Domain.BodyMeasurementLogs;
using Domain.HabitLogs;
using Domain.Habits;
using Domain.HabitSchedules;
using Domain.Notifications;
using Domain.RefreshTokens;
using Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Infrastructure.Database;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<HabitLog> HabitLogs { get; set; }
    public DbSet<Habit> Habits { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<HabitSchedule> HabitSchedules { get; set; }
    public DbSet<BodyMeasurementLog> BodyMeasurementLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.HasDefaultSchema(Schemas.Default);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        int result = await base.SaveChangesAsync(cancellationToken);
        
        return result;
    }
}
