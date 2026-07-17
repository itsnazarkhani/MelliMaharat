namespace MelliMaharat.Dal.Repos;

public class SelectedLessonInformationViewRepo : ViewRepo<SelectedLessonsInformationView>
{
    public SelectedLessonInformationViewRepo() : base() { }
    public SelectedLessonInformationViewRepo(ApplicationDbContext context) : base(context) { }
}
