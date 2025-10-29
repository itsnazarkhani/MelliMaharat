namespace DataForge.Models.Configurations;

public class LessonInformationViewConfiguration : IEntityTypeConfiguration<LessonInformationView>
{
    public void Configure(EntityTypeBuilder<LessonInformationView> builder)
    {
        builder
            .HasNoKey()
            .ToView("View_LessonInformation");
    }
}