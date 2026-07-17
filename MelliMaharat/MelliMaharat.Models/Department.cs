using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MelliMaharat.Models
{
    public class Department : BaseEntity
    {
        [Required(ErrorMessage = "لطفاً نام دانشکده را وارد کنید.")]
        [StringLength(100, ErrorMessage = "نام دانشکده نمی‌تواند بیشتر از ۱۰۰ کاراکتر باشد.")]
        public string Name { get; set; }

        public ICollection<Master> Masters { get; set; } = new List<Master>();
    }
}
