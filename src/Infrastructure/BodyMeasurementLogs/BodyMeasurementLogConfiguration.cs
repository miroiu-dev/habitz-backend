using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.BodyMeasurementLogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.BodyMeasurementLogs;
public class BodyMeasurementLogConfiguration : IEntityTypeConfiguration<BodyMeasurementLog>
{
    public void Configure(EntityTypeBuilder<BodyMeasurementLog> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Waist).IsRequired();
        builder.Property(x => x.Abs).IsRequired();
        builder.Property(x => x.Chest).IsRequired();
        builder.Property(x => x.LeftBiceps).IsRequired();
        builder.Property(x => x.RightBiceps).IsRequired();
        builder.Property(x => x.LeftTigh).IsRequired();
        builder.Property(x => x.RightTigh).IsRequired();
        builder.Property(x => x.LeftCalf).IsRequired();
        builder.Property(x => x.RightCalf).IsRequired();
        builder.Property(x => x.Shoulder).IsRequired();
        builder.Property(x => x.Neck).IsRequired();
        builder.Property(x => x.Hip).IsRequired();
        builder.Property(x => x.WaistHipRatio).IsRequired();

        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UserId).IsRequired();

        builder.HasOne(x => x.User).WithMany(x => x.BodyMeasurementLogs).HasForeignKey(x => x.UserId);
    }
}
