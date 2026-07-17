using MelliMaharat.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace MelliMaharat.Web.ViewModels.Admin
{
    public class AddMasterViewModel
    {
        // Person info
        [Required(ErrorMessage = "لطفاً نام استاد را وارد کنید.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "لطفاً نام خانوادگی استاد را وارد کنید.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "لطفاً تاریخ تولد را وارد کنید.")]
        public DateOnly BirthDate { get; set; }

        [Required(ErrorMessage = "لطفاً کد ملی را وارد کنید.")]
        [StringLength(10, ErrorMessage = "کد ملی باید ۱۰ رقم باشد.")]
        public string NationalCode { get; set; }

        [Required(ErrorMessage = "لطفاً شماره تماس را وارد کنید.")]
        [StringLength(11, ErrorMessage = "شماره تماس باید ۱۱ رقم باشد.")]
        public string PhoneNumber { get; set; }

        // Master info
        [Required(ErrorMessage = "لطفاً رشته تحصیلی را وارد کنید.")]
        [StringLength(50)]
        public string Graduation { get; set; }

        [Required(ErrorMessage = "لطفاً دانشکده را انتخاب کنید.")]
        public Guid DepartmentId { get; set; }
    }
}
