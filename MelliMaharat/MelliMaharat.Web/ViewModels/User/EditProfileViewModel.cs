using System.ComponentModel.DataAnnotations;

namespace MelliMaharat.Web.ViewModels.User
{
    public class EditProfileViewModel
    {
        [Display(Name = "نام")]
        public string? FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        public string? LastName { get; set; }

        [Display(Name = "تاریخ تولد")]
        public string? BirthDate { get; set; }

        [Display(Name = "کد ملی")]
        public string? NationalCode { get; set; }

        [Display(Name = "شماره تماس")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "ایمیل نامعتبر است.")]
        [Required(ErrorMessage = "ایمیل الزامی است.")]
        public string Email { get; set; }
    }
}
