namespace MelliMaharat.Wpf;

static class AppStaticMethods
{
    const string _godMode = "god_mode";
    const string _userRole = "user_role";
    const string _currentUser = "current_user";

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
    
}
