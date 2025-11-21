namespace DataForge.Models;

[EntityTypeConfiguration(typeof(SelectionConfiguration))]
public class Selection : BaseEntity
{
    [ForeignKey(nameof(StudentId))]
    public Student StudentNavigation { get; set; }
    public int StudentId { get; set; }

    [ForeignKey(nameof(PresentationId))]
    public Presentation PresentationNavigation { get; set; }
    public int PresentationId { get; set; }

    [Range(typeof(decimal), "0.00", "20.00")]
    public decimal Score { get; set; }
    public DateOnly EducationYear { get; set; }
}