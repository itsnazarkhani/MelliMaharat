namespace MelliMaharat.Seeding;

public static class DataSeeder
{
    public static async Task<int> SeedAsync(ApplicationDbContext context, string locale = "en", int mastersCount = 10, int studentsCount = 100, int termsCount = 8, int presentationsCount = 50, int selectionsCount = 25) // change DbContext with ApplicationDbContext
    {
        var masters = MasterFakerGenerator(locale).Generate(mastersCount);
        var students = StudentFakerGenerator(locale).Generate(studentsCount);
        var presentations = PresentationFakerGenerator(locale).Generate(presentationsCount);
        var selections = SelectionFakerGenerator(locale).Generate(selectionsCount);
        var terms = TermFakerGenerator(locale).Generate(termsCount);

        await context.Users.AddAsync(_admin);
        await context.Students.AddRangeAsync(students);
        await context.Masters.AddRangeAsync(masters);
        await context.Departments.AddRangeAsync(_departments);
        await context.Lessons.AddRangeAsync(_lessons);
        await context.Presentations.AddRangeAsync(presentations);
        await context.Selections.AddRangeAsync(selections);
        await context.Terms.AddRangeAsync(terms);

        return await context.SaveChangesAsync();
    }
}