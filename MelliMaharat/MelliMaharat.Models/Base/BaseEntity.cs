namespace MelliMaharat.Models.Base;

public class BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    [Timestamp]
    public byte[] TimeStamp { get; set; } = Array.Empty<byte>();

    [Required]
    public bool IsDeleted { get; set; } = false;
}