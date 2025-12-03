using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MelliMaharat.Models;

[EntityTypeConfiguration(typeof(LessonConfiguration))]
public class Lesson : BaseEntity
{
    [Required(ErrorMessage = "لطفاً نام درس را وارد کنید.")]
    [StringLength(50, ErrorMessage = "نام درس نمی‌تواند بیشتر از ۵۰ کاراکتر باشد.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "لطفاً تعداد واحد را وارد کنید.")]
    [Range(1, 10, ErrorMessage = "تعداد واحد باید بین ۱ تا ۱۰ باشد.")]
    public int Unit { get; set; }

    [Required(ErrorMessage = "لطفاً کد درس را وارد کنید.")]
    public int Code { get; set; }

    [InverseProperty(nameof(Presentation.Lesson))]
    public IEnumerable<Presentation> Presentations { get; set; } = new List<Presentation>();
}
