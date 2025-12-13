namespace MelliMaharat.Models;

public class Session : BaseEntity
{
    [Required(ErrorMessage = "لطفاً تاریخ جلسه را وارد کنید.")]
    public DateTime SessionDate { get; set; }

    [Required(ErrorMessage = "لطفاً انتخاب واحد مربوطه را مشخص کنید."), ForeignKey(nameof(SelectionId))]
    public Selection Selection { get; set; }

    [Required(ErrorMessage = "شناسه انتخاب الزامی است.")]
    public Guid SelectionId { get; set; }

    [InverseProperty(nameof(Attendance.Session))]
    public Attendance Attendance { get; set; }
}
