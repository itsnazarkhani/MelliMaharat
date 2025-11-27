namespace MelliMaharat.Models;

public class Master : BaseEntity
{
    [Required, StringLength(50)]
    public string Graduation { get; set; }

    [InverseProperty(nameof(Presentation.MasterNavigation))]
    public IEnumerable<Presentation> Presentations { get; set; } = new List<Presentation>();

    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid DepartmentId { get; set; }
    public Department Department { get; set; }
}