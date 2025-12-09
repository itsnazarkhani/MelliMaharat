namespace MelliMaharat.Models;

public class Attendance : BaseEntity
{
    public bool HasAttended { get; set; } = true;

    [Required(ErrorMessage = "جلسه الزامی است.")]
    [ForeignKey(nameof(SessionId))]
    public Session Session { get; set; }

    [Required(ErrorMessage = "شناسه جلسه الزامی است.")]
    public Guid SessionId { get; set; }
}
