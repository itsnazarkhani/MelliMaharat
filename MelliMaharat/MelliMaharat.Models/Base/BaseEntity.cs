namespace MelliMaharat.Models.Base;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
    
    [Timestamp]
    [Required]
    public byte[] TimeStamp { get; set; }

    [Required]
    public bool IsDeleted { get; set; } = false;
}