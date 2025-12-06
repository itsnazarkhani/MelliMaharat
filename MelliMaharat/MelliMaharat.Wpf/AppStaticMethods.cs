using MelliMaharat.Models.Enums;

namespace MelliMaharat.Wpf;

static class AppStaticMethods
{
    const string _godMode = "god_mode";
    const string _userRole = "user_role";
    const string _currentUser = "current_user";

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
