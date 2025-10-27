namespace DataForge.Tests.UnitTests
{
    public class LessonUnitTest : IClassFixture<MyFixture>
    {
        LessonRepo repo = new LessonRepo();
        [Fact]
        public void Add()
        {
            Lesson lesson = new Lesson() { Name = "bar", Unit = 2 };
            int result = repo.Add(lesson);
            Assert.Equal(1, result);
            Assert.Equal(3, repo.GetAll().Count());
            repo.Remove(lesson);
        }
        [Fact]
        public void Remove()
        {
            Lesson lesson = repo.GetWhere(x => x.Name.Equals("x")).First();
            int result = repo.Remove(lesson);
            int count = repo.GetAll().Count();
            Assert.Equal(1, result);
            Assert.Equal(1, count);
            repo.Add(new Lesson() { Name = "x", Unit = 1});
        }
        [Fact]
        public void GetNotPresentedLessons()
        {
            var lesson = new Lesson() { Name = "Math", Unit = 3 };
            repo.Add(lesson);
            var lessons = repo.GetNotPresentedLessons();
            var lessonsCount = lessons.ToList().Count;
            Assert.Equal(2, lessonsCount);
            repo.Remove(lesson);
        }
        [Fact]
        public void GetNotPresentedLessonsQuery()
        {
            string query = repo.GetNotPresentedLessonsQuery();
            Assert.NotNull(query);
            Assert.NotEmpty(query);
        }
    }
}
