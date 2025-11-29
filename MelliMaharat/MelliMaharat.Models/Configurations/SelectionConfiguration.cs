using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MelliMaharat.Models.Configurations;

public class SelectionConfiguration : IEntityTypeConfiguration<Selection>
{

    public void Configure(EntityTypeBuilder<Selection> builder)
    {
        var gradeConverter = new ValueConverter<decimal, decimal>
            (
                v => Math.Round(v, 2),
                v => v
            );

        builder
            .Property(x => x.Score)
            .HasPrecision(4, 2)
            .HasConversion(gradeConverter);

        builder
            .HasOne(x => x.Student)
            .WithMany(y => y.Selections)
            .HasForeignKey(x => x.StudentId);

        builder
            .HasOne(x => x.Presentation)
            .WithMany(y => y.Selections)
            .HasForeignKey(x => x.PresentationId);

        builder
            .ToTable
            (
                x => x.IsTemporal
                (
                    x =>
                    {
                        x.HasPeriodStart("ValidFrom");
                        x.HasPeriodEnd("ValidTo");
                        x.UseHistoryTable("SelectionAudit", "audit");
                    }
                )
            );
    }
}