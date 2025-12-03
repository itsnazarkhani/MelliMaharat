using System.ComponentModel.DataAnnotations;

namespace MelliMaharat.Web.ViewModels.Master
{
    public class CreateSessionViewModel
    {
        public Guid PresentationId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime SessionDate { get; set; }
    }
}
