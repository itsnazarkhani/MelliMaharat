global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using System.IO;
using System.Reflection;
using System.Text.Json;

namespace MelliMaharat.Wpf;

static class AppStaticMethods
{
    const string _godMode = "god_mode";
    const string _userRole = "user_role";
    const string _currentUser = "current_user";
    const string _databaseName = "MelliMaharat";
    const string _jsonFile = "appsettings.configuration.json";
    const string _resourceName = "MelliMaharat.Wpf.Resources.appsettings.configuration.json";

    internal static Master EmptyMaster => new()
    {
        Graduation = Graduations.None,
        Id = default,
        Department = EmptyDepartment,
        User = EmptyUser
    };
    internal static Department EmptyDepartment => new() 
    {
        Id = default,
        Name = Departments.None
    };
    internal static Person EmptyPerson => new()
    {
        FirstName = Empty,
        LastName = Empty,
        BirthDate = default,
        NationalCode = Empty,
        PhoneNumber = Empty
    };
    internal static User EmptyUser => new()
    {
        Id = default,
        PersonInformation = EmptyPerson,
        Email = Empty,
        Password = Empty,
        Role = UserRoles.None,
        Username = Empty,
    };
    internal static Lesson EmptyLesson => new()
    { 
        Id = default,
        Name = Empty, 
        Unit = default
    };
    internal static Presentation EmptyPresentation => new()
    {
        Id = default,
        Master = EmptyMaster,
        Lesson = EmptyLesson,
        DayHold = Empty,
        StartTime = default,
        EndTime = default,
    };
    internal static Student EmptyStudent => new()
    {
        Id = default,
        User = EmptyUser
    };
    internal static Term EmptyTerm => new()
    {
        Id = default,
        Year = default
    };
    internal static Selection EmptySelection => new()
    {
        Id = default,
        Student = EmptyStudent,
        Presentation = EmptyPresentation,
        Score = default,
        Term = EmptyTerm
    };
    internal static Person PersonInfo => new()
    {
        FirstName = "admin",
        LastName = "admin",
        NationalCode = "0000000000",
        BirthDate = new DateOnly(2004, 1, 1)
    };
    internal static User Admin => new()
    {
        PersonInformation = PersonInfo,
        Username = "admin",
        Password = "admin",
        Email = "admin@example.com",
        Role = UserRoles.Admin
    };
    internal static ApplicationDbContext AppDbContext 
    { 
        get
        {
            var assembly = Assembly.GetExecutingAssembly();

            using Stream? stream = assembly.GetManifestResourceStream(_resourceName)
                                   ?? throw new InvalidOperationException("Embedded Resource Not Found.");

            using var reader = new StreamReader(stream);

            var json = reader.ReadToEnd();

            using var doc = JsonDocument.Parse(json);

            var connStr = doc.RootElement
                             .GetProperty("ConnectionStrings")
                             .GetProperty(_databaseName)
                             .GetString()!;

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connStr);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }

    internal static bool GodMode
    {
        get => (bool)GetProperty(_godMode);
        set => SetProperty(_godMode, value);
    }
    internal static UserRoles CurrentUserRole
    {
        get => (UserRoles)GetProperty(_userRole);
        set => SetProperty(_userRole, value);
    }
    internal static object CurrentUser
    {
        get => (BaseEntity)GetProperty(_currentUser);
        set => SetProperty(_currentUser, value);
    }

    static object GetProperty(string property)
    {
        var variable = Application.Current.Properties[property];
        return variable is null ? throw new ArgumentNullException($"there is no Property Named [{property}] in Application!") : variable;
    }
    static void SetProperty(string property, object value)
    {
        ArgumentNullException.ThrowIfNull(value);
        Application.Current.Properties[property] = value;
    }
    //internal static ApplicationDbContext GetDbContext()
    //{
    //    var assembly = Assembly.GetExecutingAssembly();

    //    var resourceName = "MelliMaharat.Wpf.Resources.appsettings.configuration.json";

    //    using Stream? stream = assembly.GetManifestResourceStream(resourceName) 
    //                           ?? throw new InvalidOperationException("Embedded Resource Not Found.");

    //    using var reader = new StreamReader(stream);

    //    var json = reader.ReadToEnd();

    //    using var doc = JsonDocument.Parse(json);

    //    var connStr = doc.RootElement
    //                     .GetProperty("ConnectionStrings")
    //                     .GetProperty(_databaseName)
    //                     .GetString()!;

    //    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
    //    optionsBuilder.UseSqlServer(connStr);

    //    return new ApplicationDbContext(optionsBuilder.Options);
    //}
}