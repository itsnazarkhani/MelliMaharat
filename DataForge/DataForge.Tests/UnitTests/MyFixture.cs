namespace DataForge.Tests.UnitTests
{
    public class MyFixture
    {
        public MyFixture()
        {
            var context = new ApplicationDbContextFactory().CreateDbContext();
            
            context.Database.Migrate();

            var presentationRepo = new PresentationRepo();
            var lessonRepo = new LessonRepo();
            var masterRepo = new MasterRepo();

            presentationRepo.RemoveAll();
            lessonRepo.RemoveAll();
            masterRepo.RemoveAll();

            var lesson1 = new Lesson() { Name = "x", Unit = 1 };
            var lesson = new Lesson() { Name = "foo", Unit = 3 };
            var person = new Person() { Age = 22, FirstName = "Ali", LastName = "Kousha" };
            var master = new Master() { Graduation = "Phd", PersonInformation = person };
            var presentation = new Presentation() { LessonNavigation = lesson, MasterNavigation = master, DayHold = "Saturday", StartTime = new TimeOnly(10, 30), EndTime = new TimeOnly(12, 0)};
            
            presentationRepo.Add(presentation);
            lessonRepo.Add(lesson1);
        }
    }
}
