namespace DataForge.Models.Configurations;

public class PresentationConfiguration : IEntityTypeConfiguration<Presentation>
{
    public void Configure(EntityTypeBuilder<Presentation> builder)
    {
        builder
            .HasOne(x => x.LessonNavigation)
            .WithMany(y => y.Presentations)
            .HasForeignKey(x => x.LessonId);

        builder
            .HasOne(x => x.MasterNavigation)
            .WithMany(y => y.Presentations)
            .HasForeignKey(x => x.MasterId);

        builder
            .ToTable
            (
                x => x.IsTemporal
                (
                    x => 
                    {
                        x.UseHistoryTable("PresentationAudit", "audit");
                        x.HasPeriodStart("ValidFrom");
                        x.HasPeriodEnd("ValidTo");
                    }
                )
            );
    }
}