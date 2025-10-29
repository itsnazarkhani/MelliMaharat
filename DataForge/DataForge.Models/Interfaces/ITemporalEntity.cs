namespace DataForge.Models.Interfaces;

public interface ITemporalEntity<T> where T : BaseEntity
{
    T Entity { get; set; }
    DateTime ValidTo { get; set; }
    DateTime ValidFrom {  get; set; }
}