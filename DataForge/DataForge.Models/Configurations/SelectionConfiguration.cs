namespace DataForge.Models.Configurations;

public class SelectionConfiguration : IEntityTypeConfiguration<Selection>
{
    public void Configure(EntityTypeBuilder<Selection> builder)
    {
        builder
            .HasOne(x => x.StudentNavigation)
            .WithMany(y => y.Selections)
            .HasForeignKey(x => x.StudentId);

        builder
            .HasOne(x => x.PresentationNavigation)
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