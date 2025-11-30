using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace MelliMaharat.Web.ViewModels
{
    public class AddStudentViewModel
    {
        [Required]
        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "کد ملی")]
        public string NationalCode { get; set; }

        [Required]
        [Display(Name = "تاریخ تولد")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [StringLength(20)]
        [Display(Name = "شماره تماس")]
        public string PhoneNumber { get; set; }

        [Display(Name = "تصویر پروفایل")]
        public IFormFile AvatarFile { get; set; }
    }
}
