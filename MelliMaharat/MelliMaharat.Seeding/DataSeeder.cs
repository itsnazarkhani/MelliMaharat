namespace MelliMaharat.Seeding;

public static class DataSeeder
{
    public static async Task<int> SeedAsync(ApplicationDbContext context, string locale = "en", int mastersCount = 10, int studentsCount = 100, int termsCount = 9, int presentationsCount = 50, int selectionsCount = 25) // change DbContext with ApplicationDbContext
    {
        Random random = new();
        
        var masters = MasterFakerGenerator(locale).Generate(mastersCount);
        var students = StudentFakerGenerator(locale).Generate(studentsCount);
        var presentations = PresentationFakerGenerator(locale).Generate(presentationsCount);
        var selections = SelectionFakerGenerator(locale).Generate(selectionsCount);
        var allTerms = AllTermGenerator(locale, termsCount);

        foreach (var selection in selections)
        {
            selection.Term = allTerms[random.Next(0, allTerms.Count)];
            selection.Presentation = presentations[random.Next(0, presentations.Count)];
            selection.Student = students[random.Next(0, students.Count)];
        }
        foreach (var presentation in presentations)
        {
            presentation.Lesson = _lessons[random.Next(0, _lessons.Count)];
            presentation.Master = masters[random.Next(0, masters.Count)];
        }
        foreach (var master in masters)
        {
            master.Department = _departments[random.Next(0, _departments.Count)];
        }

        await context.Users.AddAsync(_admin);
        await context.Students.AddRangeAsync(students);
        await context.Masters.AddRangeAsync(masters);
        await context.Departments.AddRangeAsync(_departments);
        await context.Lessons.AddRangeAsync(_lessons);
        await context.Presentations.AddRangeAsync(presentations);
        await context.Selections.AddRangeAsync(selections);
        await context.Terms.AddRangeAsync(allTerms);
        
        return await context.SaveChangesAsync();
    }
}