namespace MelliMaharat.Seeding;

public static class DataSeeder
{
    public static async Task<int> SeedAsync(ApplicationDbContext context, string locale = "en", int mastersCount = 10, int studentsCount = 100) // change DbContext with ApplicationDbContext
    {
        if (!await context.Users.AnyAsync())
        {
            User admin = new User()
            {
                Username = "admin",
                Password = "admin",
                Email = "admin@example.com",
                Role = Models.Enums.UserRoles.Admin,
            };
            await context.Users.AddAsync(admin);
        }

        if (!await context.Departments.AnyAsync())
        {
            Department department = new Department()
            {
                Name = "چمران"
            };
            await context.Departments.AddAsync(department);
        }
        return await context.SaveChangesAsync();
    }
}