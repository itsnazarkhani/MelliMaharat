namespace DataForge.Models.Base
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        
        [Timestamp]
        [Required]
        public byte[] TimeStamp { get; set; }
    }
}
