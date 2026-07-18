namespace MelliMaharat.Models;

public class SelectionFeedback : BaseEntity
{
    [Range(1, 5, ErrorMessage = "امتیاز باید بین ۱ تا ۵ باشد.")]
    public int Rating { get; set; }

    [Required(ErrorMessage = "لطفاً انتخاب مربوطه را مشخص کنید."), ForeignKey(nameof(SelectionId))]
    public Selection Selection { get; set; }

    [Required(ErrorMessage = "شناسه انتخاب الزامی است.")]
    public Guid SelectionId { get; set; }
}
