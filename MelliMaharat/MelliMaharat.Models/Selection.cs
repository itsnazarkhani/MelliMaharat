namespace MelliMaharat.Models;

[EntityTypeConfiguration(typeof(SelectionConfiguration))]
public class Selection : BaseEntity
{
    [Range(typeof(decimal), "0.00", "20.00")]
    public decimal Score { get; set; }

    [ForeignKey(nameof(StudentId))]
    public Student Student{ get; set; }
    public Guid StudentId { get; set; }

    [ForeignKey(nameof(PresentationId))]
    public Presentation Presentation{ get; set; }
    public Guid PresentationId { get; set; }

    public Session Attendance { get; set; }
    public SelectionFeedback SelectionFeedback { get; set; }

    [ForeignKey(nameof(TermId))]
    public Term Term { get; set; }
    public Guid TermId { get; set; }
}