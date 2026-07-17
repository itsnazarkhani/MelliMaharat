using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MelliMaharat.Models;

public class Master : BaseEntity
{
    [Required(ErrorMessage = "لطفاً مدرک تحصیلی را وارد کنید.")]
    [StringLength(50, ErrorMessage = "مدرک تحصیلی نمی‌تواند بیشتر از ۵۰ کاراکتر باشد.")]
    public string Graduation { get; set; }

    [InverseProperty(nameof(Presentation.Master))]
    public IEnumerable<Presentation> Presentations { get; set; } = new List<Presentation>();

    [Required(ErrorMessage = "لطفاً کاربر مرتبط را انتخاب کنید.")]
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }

    [Required(ErrorMessage = "شناسه کاربر الزامی است.")]
    public Guid UserId { get; set; }

    [Required(ErrorMessage = "لطفاً دانشکده مرتبط را انتخاب کنید.")]
    [ForeignKey(nameof(DepartmentId))]
    public Department Department { get; set; }

    [Required(ErrorMessage = "شناسه دانشکده الزامی است.")]
    public Guid DepartmentId { get; set; }
}
