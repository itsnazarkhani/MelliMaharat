using System.ComponentModel.DataAnnotations;

namespace MelliMaharat.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "لطفاً نام کاربری را وارد کنید.")]
        [StringLength(50, ErrorMessage = "نام کاربری نمی‌تواند بیشتر از ۵۰ کاراکتر باشد.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "لطفاً رمز عبور را وارد کنید.")]
        [DataType(DataType.Password)]
        [StringLength(120, ErrorMessage = "رمز عبور نمی‌تواند بیشتر از ۱۲۰ کاراکتر باشد.")]
        public string? Password { get; set; }
    }
}
