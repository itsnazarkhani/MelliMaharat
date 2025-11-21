namespace DataForge.Models.Owned;

[Owned]
public class Person
{
    [Required, StringLength(50)]
    public string FirstName { get; set; }

    [Required, StringLength(50)]
    public string LastName { get; set; }

    [Required]
    public int Age { get; set; }

    [StringLength(20, MinimumLength = 20)]
    public string NationalCode { get; set; }

    [StringLength(20, MinimumLength = 7)]
    public string PhoneNumber { get; set; }

    [StringLength(50)]
    public string Email { get; set; }

    [Required, StringLength(500)]
    public string Password { get; set; }

    public bool IsAdmin { get; set; }

    [Required, StringLength(50)]
    public string Username { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public string FullName;
}