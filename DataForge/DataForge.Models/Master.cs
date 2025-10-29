namespace DataForge.Models;

[EntityTypeConfiguration(typeof(MasterConfiguration))]
public class Master : BaseEntity
{
    public Person PersonInformation { get; set; } = new Person();
    
    [Required]
    public string Graduation { get; set; }

    [InverseProperty(nameof(Presentation.MasterNavigation))]
    public IEnumerable<Presentation> Presentations { get; set; } = new List<Presentation>();
}
