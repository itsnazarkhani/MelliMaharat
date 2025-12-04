using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MelliMaharat.Models;

[EntityTypeConfiguration(typeof(SelectionConfiguration))]
public class Selection : BaseEntity
{
    [Range(typeof(decimal), "0.00", "20.00", ErrorMessage = "امتیاز باید بین ۰ تا ۲۰ باشد.")]
    public decimal Score { get; set; }

    [Required(ErrorMessage = "لطفاً دانشجو را انتخاب کنید.")]
    [ForeignKey(nameof(StudentId))]
    public Student Student { get; set; }

    [Required(ErrorMessage = "شناسه دانشجو الزامی است.")]
    public Guid StudentId { get; set; }

    [Required(ErrorMessage = "لطفاً ارائه مربوطه را انتخاب کنید.")]
    [ForeignKey(nameof(PresentationId))]
    public Presentation Presentation { get; set; }

    [Required(ErrorMessage = "شناسه ارائه الزامی است.")]
    public Guid PresentationId { get; set; }

    [InverseProperty(nameof(Session.Selection))]
    public IEnumerable<Session> Sessions { get; set; } = new List<Session>();

    public SelectionFeedback SelectionFeedback { get; set; }

    [Required(ErrorMessage = "لطفاً ترم مربوطه را انتخاب کنید.")]
    [ForeignKey(nameof(TermId))]
    public Term Term { get; set; }

    [Required(ErrorMessage = "شناسه ترم الزامی است.")]
    public Guid TermId { get; set; }
}
