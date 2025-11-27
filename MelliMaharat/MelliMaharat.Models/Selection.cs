namespace MelliMaharat.Models;

[EntityTypeConfiguration(typeof(SelectionConfiguration))]
public class Selection : BaseEntity
{
    [Range(typeof(decimal), "0.00", "20.00")]
    public decimal Score { get; set; }
    public DateOnly EducationYear { get; set; }

    [ForeignKey(nameof(StudentId))]
    public Student StudentNavigation { get; set; }
    public Guid StudentId { get; set; }

    [ForeignKey(nameof(PresentationId))]
    public Presentation PresentationNavigation { get; set; }
    public Guid PresentationId { get; set; }

    public Session Attendance { get; set; }
    public SelectionFeedback SelectionFeedback { get; set; }
}