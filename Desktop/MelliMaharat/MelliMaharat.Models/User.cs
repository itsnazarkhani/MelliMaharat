namespace MelliMaharat.Models
{
    [EntityTypeConfiguration(typeof(UserConfiguration))]
    public class User : BaseEntity
    {
        public Person PersonInformation { get; set; } = new Person();

        [Required, StringLength(50)]
        public string Username { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required, MaxLength(120)]
        public string Password { get; set; }

        [Required]
        public UserRoles Role { get; set; }

        public Guid AvatarId { get; set; } = Guid.Empty;

        // Navigation Properties
        public Student Student { get; set; }
        public Master Master { get; set; }
    }
}
