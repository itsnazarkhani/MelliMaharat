namespace DataForge.Tests.UnitTests
{
    public class LessonInformationViewTest : IClassFixture<MyFixture>
    {
        LessonInformationViewRepo repo = new LessonInformationViewRepo();
        [Fact]
        public void GetAll()
        {
            var list = repo.GetAll().ToList();
            var listCount = list.Count;
            Assert.Equal(1, listCount);
        }
    }
}
