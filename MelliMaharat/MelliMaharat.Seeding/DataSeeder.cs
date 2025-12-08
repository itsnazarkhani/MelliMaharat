using MelliMaharat.Models.Enums;
using Microsoft.SqlServer.Server;
using System.ComponentModel;
using System.Net.Security;

namespace MelliMaharat.Seeding;

public static class DataSeeder
{
    public static async Task<int> SeedAsync(ApplicationDbContext context, string locale = "en", int mastersCount = 10, int studentsCount = 100) // change DbContext with ApplicationDbContext
    {
        DateOnly _startDateStudent = new(2006, 0, 0);
        DateOnly _startDateMaster = new(2002, 0, 0);

        Faker<Models.Owned.Person> PersonFakerGenerator(DateOnly date) => 
            new Faker<Models.Owned.Person>(locale)
                .RuleFor(x => x.FirstName, f => f.Person.FirstName)
                .RuleFor(x => x.LastName, f => f.Person.LastName)
                .RuleFor(x => x.BirthDate, f => f.Date.FutureDateOnly(50, date))
                .RuleFor(x => x.NationalCode, f => f.Random.Replace("##########"))
                .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("09#########"));
       Faker<User> UserFakerGenerator(UserRoles role)
       {
            Models.Owned.Person person = role switch
            {
                UserRoles.Student => PersonFakerGenerator(_startDateStudent).Generate(),
                UserRoles.Master => PersonFakerGenerator(_startDateMaster).Generate(),
                _ => throw new InvalidEnumArgumentException(nameof(role))
            };

            return new Faker<User>(locale)
                           .RuleFor(x => x.PersonInformation, f => person)
                           .RuleFor(x => x.Username, (f, x) => f.Internet.UserName(x.PersonInformation.FirstName, x.PersonInformation.LastName))
                           .RuleFor(x => x.Email, (f, x) => f.Internet.Email(x.PersonInformation.FirstName, x.PersonInformation.LastName))
                           .RuleFor(x => x.Password, f => f.Internet.Password())
                           .RuleFor(x => x.Role, f => role);
       }
        //var masterPersonFaker = PersonFakerGenerator(_startDateMaster);
        //var studentPersonFaker = PersonFakerGenerator(_startDateStudent);
        //var userFaker; we need one manager so dont need this.
        //IEnumerable<Graduations> graduations = [Graduations.Doctorate, Graduations.Master, Graduations.Bachelor, Graduations.Associate];


        var student_User_Faker = UserFakerGenerator(UserRoles.Student);
        var master_User_Faker = UserFakerGenerator(UserRoles.Master);
        var department_Faker = new Faker<Department>().RuleFor(x => x.Name, f => f.PickRandom<Departments>());
        var student_Faker = new Faker<Student>().RuleFor(x => x.User, f => student_User_Faker.Generate());
        var master_Faker = new Faker<Master>()
                                   .RuleFor(x => x.User, f => master_User_Faker.Generate())
                                   .RuleFor(x => x.Graduation, f => f.PickRandom<Graduations>())
                                   .RuleFor(x => x.Department, f => department_Faker.Generate());
        IEnumerable<Lesson> lessons = 
        [
            new() {Name = "Circuit Theory I",            Unit = 1, Code = 101},
            new() {Name = "Electromagnetics I",          Unit = 2, Code = 102},
            new() {Name = "Digital Logic Design",        Unit = 3, Code = 103},
            new() {Name = "Signals and Systems",         Unit = 1, Code = 104},
            new() {Name = "Electronics I",               Unit = 2, Code = 105},
            new() {Name = "Electrical Machines I",       Unit = 3, Code = 106},
            new() {Name = "Power Systems Analysis",      Unit = 3, Code = 107},
            new() {Name = "Control Systems",             Unit = 2, Code = 108},
            new() {Name = "Microprocessors",             Unit = 3, Code = 109},
            new() {Name = "Engineering Mathematics",     Unit = 1, Code = 110},
            new() {Name = "Introduction to Programming", Unit = 1, Code = 201},
            new() {Name = "Data Structures",             Unit = 2, Code = 202},
            new() {Name = "Computer Architecture",       Unit = 3, Code = 203},
            new() {Name = "Operating Systems",           Unit = 2, Code = 204},
            new() {Name = "Database Systems",            Unit = 1, Code = 205},
            new() {Name = "Software Engineering",        Unit = 3, Code = 206},
            new() {Name = "Computer Networks",           Unit = 1, Code = 207},
            new() {Name = "Artificial Intelligence",     Unit = 3, Code = 208},
            new() {Name = "Theory of Computation",       Unit = 1, Code = 209},
            new() {Name = "Discrete Mathematics",        Unit = 3, Code = 210},
            new() {Name = "Statics",                     Unit = 2, Code = 301},
            new() {Name = "Mechanics of Materials",      Unit = 2, Code = 302},
            new() {Name = "Fluid Mechanics",             Unit = 3, Code = 303},
            new() {Name = "Structural Analysis I",       Unit = 1, Code = 304},
            new() {Name = "Concrete Structures I",       Unit = 3, Code = 305},
            new() {Name = "Soil Mechanics",              Unit = 3, Code = 306},
            new() {Name = "Transportation Engineering",  Unit = 2, Code = 307},
            new() {Name = "Hydraulics",                  Unit = 3, Code = 308},
            new() {Name = "Construction Management",     Unit = 1, Code = 309},
            new() {Name = "Engineering Geology",         Unit = 1, Code = 310},
        ];
        var presentations_Faker = new Faker<Presentation>()
                                          .RuleFor(x => x.Lesson, f => f.PickRandom(lessons))
                                          .RuleFor(x => x.DayHold, f => f.Date.Weekday())
                                          .RuleFor(x => x.StartTime, f => f.Date.BetweenTimeOnly(new(8, 0), new(15, 0)))
                                          .RuleFor(x => x.EndTime, f => f.Date.BetweenTimeOnly(new(10, 0), new(18, 0)))
                                          .RuleFor(x => x.ExamDate, f => f.Date.SoonDateOnly())
                                          .RuleFor(x => x.ExamStartTime, f => f.Date.BetweenTimeOnly(new(8, 0), new(15, 0)))
                                          .RuleFor(x => x.Master, f => master_Faker.Generate());

        var term_Faker = new Faker<Term>()
                                 .RuleFor(x => x.Year, f => f.Random.Int(2026, 2030))
                                 .RuleFor(x => x.Type, f => f.PickRandom<TermType>())
                                 .RuleFor(x => x.StartTime, (f, x) => f.Date.BetweenDateOnly(new(x.Year, 0, 0), new(x.Year+10, 0, 0)))
                                 .RuleFor(x => x.EndTime, (f,x) => new DateOnly(x.StartTime.Year, x.StartTime.Month + 4, 0));

        var selections_faker = new Faker<Selection>()
                                       .RuleFor(x => x.Score, f => (decimal)f.Random.Float(0.0f, 20f))
                                       .RuleFor(x => x.Student, f => student_Faker.Generate())
                                       .RuleFor(x => x.Presentation, f => presentations_Faker.Generate())
                                       .RuleFor(x => x.Term, f => term_Faker.Generate());

        if (!await context.Users.AnyAsync())
        {
            var admin = new User()
            {
                PersonInformation = new Models.Owned.Person()
                {
                    FirstName = "admin",
                    LastName = "admin",
                    NationalCode = "0000000000",
                    BirthDate = new DateOnly(1990, 1, 1),
                },
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
                Name = Departments.CSE
            };
            await context.Departments.AddAsync(department);
        }
        return await context.SaveChangesAsync();
    }
}