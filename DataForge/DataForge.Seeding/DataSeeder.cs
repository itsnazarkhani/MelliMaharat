namespace DataForge.Seeding;

public static class DataSeeder
{
    public static async Task<int> SeedAsync(ApplicationDbContext context, string locale = "en", int mastersCount = 10, int studentsCount = 100) // change DbContext with ApplicationDbContext
    {
        if (await context.Lessons.AnyAsync())
            return 0; // database already seeded!

        string[] lessonNames = ["Computer", "Electric", "Finance", "Religion", "Marketing"];
        string[] graduations = ["Diploma", "Advanced Diploma", "Bachelor", "Master", "Doctorate", "Post-Doctorate"];

        var fakePerson = new Faker<Models.Owned.Person>(locale)
                                .RuleFor(x => x.FirstName, f => f.Name.FirstName())
                                .RuleFor(x => x.LastName, f => f.Name.LastName())
                                .RuleFor(x => x.Age, f => f.Random.Int(18, 60))
                                .RuleFor(x => x.NationalCode, f => f.Random.Replace("##########"))
                                .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("+98##########"))
                                .RuleFor(x => x.Email, (f, x) => f.Internet.Email(x.FirstName, x.LastName))
                                .RuleFor(x => x.Password, (f, x) => f.Internet.Password())
                                .RuleFor(x => x.Username, (f, x) => f.Internet.UserNameUnicode(x.FirstName, x.LastName));

        var fakeMaster = new Faker<Master>(locale)
                                .RuleFor(x => x.PersonInformation, f => fakePerson.Generate())
                                .RuleFor(x => x.Graduation, f => f.PickRandomParam(graduations));

        var fakeStudent = new Faker<Student>(locale)
                                .RuleFor(x => x.PersonInformation, f => fakePerson.Generate());

        var fakeSelection = new Faker<Selection>(locale)
                                .RuleFor(x => x.Score, f => (decimal)f.Random.Float(0, 20))
                                .RuleFor(x => x.EducationYear, f => f.Date.BetweenDateOnly(new DateOnly(2020, 1, 1), new DateOnly(2026, 1, 1)));

        var fakePresentation = new Faker<Presentation>(locale)
                                .RuleFor(x => x.DayHold, f => f.Date.Weekday())
                                .RuleFor(x => x.StartTime, f => f.Date.BetweenTimeOnly(new TimeOnly(8, 0), new TimeOnly(5, 0)))
                                .RuleFor(x => x.EndTime, f => f.Date.BetweenTimeOnly(new TimeOnly(9, 0), new TimeOnly(6, 0)));


        var masters = fakeMaster.Generate(mastersCount);
        var students = fakeStudent.Generate(studentsCount);
        var lessons = new List<Lesson>();
        var presentations = new List<Presentation>();
        var selections = new List<Selection>();


        foreach (var lessonName in lessonNames)
        {
            Lesson _lesson = new();
            _lesson.Unit = new Random().Next(1, 4);
            _lesson.Name = lessonName;
            lessons.Add(_lesson);
        }

        foreach (var lessonItem in lessons)
        {
            foreach (var masterItem in masters)
            {
                var presentationItem = fakePresentation.Generate();
                presentationItem.MasterNavigation = masterItem;
                presentationItem.LessonNavigation = lessonItem;
                presentations.Add(presentationItem);
            }
        }

        foreach (var studentItem in students)
        {
            foreach (var presentationItem in presentations)
            {
                var selectionItem = fakeSelection.Generate();
                selectionItem.PresentationNavigation = presentationItem;
                selectionItem.StudentNavigation = studentItem;
                selections.Add(selectionItem);
            }
        }

        Models.Owned.Person adminInfo = new() { FirstName = "admin", LastName = "admin", Password = "admin", Username = "admin", IsAdmin = true };
        students.Add(new() { PersonInformation = adminInfo });
        masters.Add(new() { PersonInformation = adminInfo, Graduation = graduations[new Random().Next(graduations.Length + 1)] });

        await context.Masters.AddRangeAsync(masters);
        await context.Lessons.AddRangeAsync(lessons);
        await context.Students.AddRangeAsync(students);
        await context.Presentations.AddRangeAsync(presentations);
        await context.Selections.AddRangeAsync(selections);
        return await context.SaveChangesAsync();
    }
}