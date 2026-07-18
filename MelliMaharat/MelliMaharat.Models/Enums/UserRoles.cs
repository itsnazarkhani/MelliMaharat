using System.ComponentModel;

namespace MelliMaharat.Models.Enums;

public enum UserRoles
{
    [Description("دانشجو")]
    Student,
    [Description("استاد")]
    Master,
    [Description("ادمین")]
    Admin,
    [Description("مهمان")]
    None
}
