using System.ComponentModel.DataAnnotations;

namespace MelliMaharat.Web.ViewModels.User
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "رمز عبور فعلی الزامی است")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور فعلی")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "رمز عبور جدید الزامی است")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور جدید")]
        [MinLength(6, ErrorMessage = "رمز عبور باید حداقل ۶ کاراکتر باشد")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "تکرار رمز عبور الزامی است")]
        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور جدید")]
        [Compare("NewPassword", ErrorMessage = "رمز عبور جدید و تکرار آن مطابقت ندارند")]
        public string ConfirmNewPassword { get; set; }
    }
}
