
namespace DataForge.Models.Configurations;

public class SelectedLessonInformationViewConfiguration : IEntityTypeConfiguration<SelectedLessonsInformationView>
{
    public void Configure(EntityTypeBuilder<SelectedLessonsInformationView> builder)
    {
        builder
            .HasNoKey()
            .ToView("View_SelectedLessonsInformation");
    }
}
