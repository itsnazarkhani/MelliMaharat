
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
            .HasOne(s => s.Student)
            .WithMany(st => st.Selections)
            .HasForeignKey(s => s.StudentId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(s => s.Presentation)
            .WithMany(p => p.Selections)
            .HasForeignKey(s => s.PresentationId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(s => s.Term)
            .WithMany(t => t.Selections)
            .HasForeignKey(s => s.TermId)
            .OnDelete(DeleteBehavior.NoAction);

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