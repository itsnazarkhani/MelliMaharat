namespace MelliMaharat.Models;

public class Master : BaseEntity
{
    [Required, StringLength(50)]
    public Graduations Graduation { get; set; }

    [Required, ForeignKey(nameof(UserId))]
    public User User { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required, ForeignKey(nameof(DepartmentId))]
    public Department Department { get; set; }

    [Required]
    public Guid DepartmentId { get; set; }
    
    [InverseProperty(nameof(Presentation.Master))]
    public IEnumerable<Presentation> Presentations { get; set; } = new List<Presentation>();

}