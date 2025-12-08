namespace MelliMaharat.Models;

public class Department : BaseEntity
{
    [Required(ErrorMessage = "Please insert University Name."), StringLength(100)]
    public Departments Name { get; set; }

    public ICollection<Master> Masters { get; set; } = new List<Master>();
}
