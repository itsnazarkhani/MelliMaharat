namespace MelliMaharat.Models;

public class Term : BaseEntity
{
    [Required]
    public int Year { get; set; }

    [Required]
    public TermType Type { get; set; }

    [Required]
    public DateOnly StartTime { get; set; }

    [Required]
    public DateOnly EndTime { get; set; }

    [InverseProperty(nameof(Selection.Term))]
    public IEnumerable<Selection> Selections { get; set; } = new List<Selection>();

    [InverseProperty(nameof(SelectionTime.Term))]
    public IEnumerable<SelectionTime> SelectionTimes { get; set; } = new List<SelectionTime>();
}
