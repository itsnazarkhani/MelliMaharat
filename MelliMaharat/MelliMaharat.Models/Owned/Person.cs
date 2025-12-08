namespace MelliMaharat.Models.Owned;

[Owned]
public class Person
{
    [Required]
    public string FirstName { get; set; }

    [Required, StringLength(50)]
    public string LastName { get; set; }

    [Required]
    public DateOnly BirthDate { get; set; }

    [StringLength(10)]
    public string NationalCode { get; set; }

    [StringLength(11)]
    public string PhoneNumber { get; set; }
}