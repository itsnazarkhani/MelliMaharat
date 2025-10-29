namespace DataForge.Models.Configurations;

public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder
            .ToTable
            (
                x => x.IsTemporal
                (
                    x =>
                    {
                        x.UseHistoryTable("LessonAudit", "audit");
                        x.HasPeriodStart("ValidFrom");
                        x.HasPeriodEnd("ValidTo");
                    }
                )
            );
    }
}