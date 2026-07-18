namespace MelliMaharat.Models;

[EntityTypeConfiguration(typeof(SelectionConfiguration))]
public class Selection : BaseEntity
{
    [Range(typeof(decimal), "0.00", "20.00", ErrorMessage = "امتیاز باید بین ۰ تا ۲۰ باشد.")]
    public decimal Score { get; set; }

    [Required, ForeignKey(nameof(StudentId))]
    public Student Student { get; set; }

    [Required]
    public Guid StudentId { get; set; }

    [Required, ForeignKey(nameof(PresentationId))]
    public Presentation Presentation { get; set; }

    [Required]
    public Guid PresentationId { get; set; }

    [InverseProperty(nameof(Session.Selection))]
    public IEnumerable<Session> Sessions { get; set; } = new List<Session>();

    public SelectionFeedback SelectionFeedback { get; set; }

    [Required, ForeignKey(nameof(TermId))]
    public Term Term { get; set; }

    [Required]
    public Guid TermId { get; set; }
}
