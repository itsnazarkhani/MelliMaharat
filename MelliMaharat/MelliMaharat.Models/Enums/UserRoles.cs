namespace MelliMaharat.Models.Enums;

public enum UserRoles
{
    [Description("تعریف نشده")]
    None,

    [Description("دانشجو")]
    Student,

    [Description("استاد")]
    Master,

    [Description("ادمین")]
    Admin
}
