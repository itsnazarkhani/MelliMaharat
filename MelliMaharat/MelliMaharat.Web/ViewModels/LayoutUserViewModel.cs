using MelliMaharat.Models.Enums;

namespace MelliMaharat.Web.ViewModels
{
    public class LayoutUserViewModel
    {
        public string? FullName { get; set; }
        public string? ProfileImagePath { get; set; }
        public UserRoles Role { get; set; } = UserRoles.None;

        public List<LayoutNavItem> NavItems { get; set; } = new();
    }

    public class LayoutNavItem
    {
        public string Title { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
    }
}
