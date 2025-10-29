namespace DataForge.Models.Views;

public class TemporalView<T> : ITemporalEntity<T> where T : BaseEntity
{
    public T Entity { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
}