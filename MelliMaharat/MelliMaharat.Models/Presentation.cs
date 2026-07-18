using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MelliMaharat.Models;

[EntityTypeConfiguration(typeof(PresentationConfiguration))]
public class Presentation : BaseEntity
{
    [Required(ErrorMessage = "لطفاً روز برگزاری را وارد کنید.")]
    [StringLength(50, ErrorMessage = "نام روز نمی‌تواند بیشتر از ۵۰ کاراکتر باشد.")]
    public string DayHold { get; set; }

    [Required(ErrorMessage = "لطفاً زمان شروع را وارد کنید.")]
    public TimeOnly StartTime { get; set; }

    [Required(ErrorMessage = "لطفاً زمان پایان را وارد کنید.")]
    public TimeOnly EndTime { get; set; }

    public DateOnly ExamDate { get; set; }
    public TimeOnly ExamStartTime { get; set; }

    [InverseProperty(nameof(Selection.Presentation))]
    public IEnumerable<Selection> Selections { get; set; } = new List<Selection>();

    [Required(ErrorMessage = "لطفاً استاد مربوطه را انتخاب کنید.")]
    [ForeignKey(nameof(MasterId))]
    public Master Master { get; set; }

    [Required(ErrorMessage = "شناسه استاد الزامی است.")]
    public Guid MasterId { get; set; }

    [Required(ErrorMessage = "لطفاً درس مربوطه را انتخاب کنید.")]
    [ForeignKey(nameof(LessonId))]
    public Lesson Lesson { get; set; }

    [Required(ErrorMessage = "شناسه درس الزامی است.")]
    public Guid LessonId { get; set; }
}
