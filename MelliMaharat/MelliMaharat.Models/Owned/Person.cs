namespace MelliMaharat.Models.Owned;

[Owned]
public class Person
{
    [Required(ErrorMessage = "لطفاً نام را وارد کنید.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "لطفاً نام خانوادگی را وارد کنید.")]
    [StringLength(50, ErrorMessage = "نام خانوادگی نمی‌تواند بیشتر از ۵۰ کاراکتر باشد.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "لطفاً تاریخ تولد را وارد کنید.")]
    public DateOnly BirthDate { get; set; }

    [StringLength(10, ErrorMessage = "کد ملی نمی‌تواند بیشتر از ۱۰ رقم باشد.")]
    public string NationalCode { get; set; }

    [StringLength(11, ErrorMessage = "شماره تلفن نمی‌تواند بیشتر از ۱۱ رقم باشد.")]
    public string PhoneNumber { get; set; }
}