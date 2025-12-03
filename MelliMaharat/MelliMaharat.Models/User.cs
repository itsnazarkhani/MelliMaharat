using MelliMaharat.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MelliMaharat.Models
{
    [EntityTypeConfiguration(typeof(UserConfiguration))]
    public class User : BaseEntity
    {
        public Person PersonInformation { get; set; } = new Person();

        [Required(ErrorMessage = "لطفاً نام کاربری را وارد کنید.")]
        [StringLength(50, ErrorMessage = "نام کاربری نمی‌تواند بیشتر از ۵۰ کاراکتر باشد.")]
        public string Username { get; set; }

        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نیست.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "لطفاً رمز عبور را وارد کنید.")]
        [MaxLength(120, ErrorMessage = "رمز عبور نمی‌تواند بیشتر از ۱۲۰ کاراکتر باشد.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "لطفاً نقش کاربر را انتخاب کنید.")]
        public UserRoles Role { get; set; }

        public Guid AvatarId { get; set; } = Guid.Empty;

        // Navigation Properties
        public Student Student { get; set; }
        public Master Master { get; set; }
    }
}
