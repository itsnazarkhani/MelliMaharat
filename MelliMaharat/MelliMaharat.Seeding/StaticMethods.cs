namespace MelliMaharat.Seeding;

static class StaticMethods
{
    internal static readonly DateOnly _start_Date_Student = new(2004, 1, 1);
    internal static readonly DateOnly _start_Date_Master = new(2002, 1, 1);
    internal static readonly List<Lesson> _lessons =
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
    internal static readonly Models.Owned.Person _personInfo = new()
    {
        FirstName = "admin",
        LastName = "admin",
        NationalCode = "0000000000",
        BirthDate = new DateOnly(2004, 1, 1)
    };
    internal static readonly User _admin = new()
    {
        PersonInformation = _personInfo,
        Username = "admin",
        Password = "admin",
        Email = "admin@example.com",
        Role = UserRoles.Admin
    };
    internal static readonly Department _department = new()
    {
        Name = Departments.CSE
    };
    internal static readonly List<Departments> _departmentsEnum = [.. Enum.GetValues<Departments>()];
    internal static readonly List<Department> _departments = [.. _departmentsEnum.Select(x => new Department() { Name = x })];
    
    internal static Faker<Models.Owned.Person> PersonFakerGenerator(DateOnly birthDateFrom, string locale)
    {
        return new Faker<Models.Owned.Person>(locale)
                .RuleFor(x => x.FirstName, f => f.Person.FirstName)
                .RuleFor(x => x.LastName, f => f.Person.LastName)
                .RuleFor(x => x.BirthDate, f => f.Date.FutureDateOnly(50, birthDateFrom))
                .RuleFor(x => x.NationalCode, f => f.Random.Replace("##########"))
                .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("09#########"));
    }
    internal static Faker<User> UserFakerGenerator(UserRoles role, string locale)
    {
        Models.Owned.Person person = role switch
        {
            UserRoles.Student => PersonFakerGenerator(_start_Date_Student, locale).Generate(),
            UserRoles.Master => PersonFakerGenerator(_start_Date_Master, locale).Generate(),
            _ => throw new InvalidEnumArgumentException(nameof(role))
        };

        return new Faker<User>(locale)
                       .RuleFor(x => x.PersonInformation, f => person)
                       .RuleFor(x => x.Username, (f, x) => f.Internet.UserName(x.PersonInformation.FirstName, x.PersonInformation.LastName))
                       .RuleFor(x => x.Email, (f, x) => f.Internet.Email(x.PersonInformation.FirstName, x.PersonInformation.LastName))
                       .RuleFor(x => x.Password, f => f.Internet.Password())
                       .RuleFor(x => x.Role, f => role);
    }
    internal static Faker<Department> DepartmentFakerGenerator(string locale)
    {
        return new Faker<Department>(locale).RuleFor(x => x.Name, f => f.PickRandom<Departments>());
    }
    internal static Faker<Student> StudentFakerGenerator(string locale)
    {
        return new Faker<Student>(locale).RuleFor(x => x.User, f => UserFakerGenerator(UserRoles.Student, locale).Generate());
    }
    internal static Faker<Master> MasterFakerGenerator(string locale)
    {
        return new Faker<Master>(locale)
                                   .RuleFor(x => x.User, f => UserFakerGenerator(UserRoles.Master, locale).Generate())
                                   .RuleFor(x => x.Graduation, f => f.PickRandom<Graduations>())
                                   .RuleFor(x => x.Department, f => DepartmentFakerGenerator(locale).Generate());
    }
    internal static Faker<Presentation> PresentationFakerGenerator(string locale)
    {
        return new Faker<Presentation>(locale)
                                          .RuleFor(x => x.Lesson, f => f.PickRandom(_lessons))
                                          .RuleFor(x => x.DayHold, f => f.Date.Weekday())
                                          .RuleFor(x => x.StartTime, f => f.Date.BetweenTimeOnly(new(8, 0), new(15, 0)))
                                          .RuleFor(x => x.EndTime, f => f.Date.BetweenTimeOnly(new(10, 0), new(18, 0)))
                                          .RuleFor(x => x.ExamDate, f => f.Date.SoonDateOnly())
                                          .RuleFor(x => x.ExamStartTime, f => f.Date.BetweenTimeOnly(new(8, 0), new(15, 0)))
                                          .RuleFor(x => x.Master, f => MasterFakerGenerator(locale).Generate());
    }
    internal static Faker<Term> TermFakerGenerator(string locale, TermType termType)
    {
        DateOnly startTime = new();
        DateOnly endTime = new();

        switch (termType)
        {
            case TermType.Fall:
                startTime = new DateOnly(2025, 9, 21);
                endTime = new DateOnly(2026, 1, 8);
                break;
            
            case TermType.Spring:
                startTime = new DateOnly(2026, 2, 14);
                endTime = new DateOnly(2026, 6, 18);
                break;
            
            case TermType.Summer:
                startTime = new DateOnly(2026, 7, 10);
                endTime = new DateOnly(2026, 8, 30);
                break;
        }            
        
        return new Faker<Term>(locale).RuleFor(x => x.Year, f => f.Random.Int(2026, 2030))
                                 .RuleFor(x => x.Type, f => termType)
                                 .RuleFor(x => x.StartTime, (f, x) => new DateOnly(x.Year, startTime.Month, startTime.Day))
                                 .RuleFor(x => x.EndTime, (f, x) => new DateOnly(x.Type == TermType.Fall ? x.Year + 1 : x.Year, endTime.Month, endTime.Day));
    } 
    internal static Faker<Selection> SelectionFakerGenerator(string locale) // termType shouldnt be assigned here!
    {
        return new Faker<Selection>(locale)
                                       .RuleFor(x => x.Score, f => (decimal)f.Random.Float(0.0f, 20f))
                                       .RuleFor(x => x.Student, f => StudentFakerGenerator(locale).Generate())
                                       .RuleFor(x => x.Presentation, f => PresentationFakerGenerator(locale).Generate());
                                       //.RuleFor(x => x.Term, f => TermFakerGenerator(locale, termType).Generate());
    }
    internal static List<Term> AllTermGenerator(string locale, int termsCount)
    {
        var allTerms = new List<Term>();
        var fallTerms = TermFakerGenerator(locale, TermType.Fall).Generate(termsCount / 3);
        var springTerms = TermFakerGenerator(locale, TermType.Spring).Generate(termsCount / 3);
        var summerTerms = TermFakerGenerator(locale, TermType.Summer).Generate(termsCount / 3);
        allTerms.AddRange(fallTerms);
        allTerms.AddRange(springTerms);
        allTerms.AddRange(summerTerms);
        return allTerms;
    }
}