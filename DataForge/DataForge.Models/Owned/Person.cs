namespace DataForge.Models.Owned;

[Owned]
public class Person
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public int Age { get; set; }

    public string NationalCode { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public string FullName;
}