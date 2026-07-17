using MelliMaharat.Models;

namespace MelliMaharat.Web.ViewModels.Student
{
    public class RegisterSelectionViewModel
    {
        public List<Presentation> AvailablePresentations { get; set; } = new();
        public List<Guid> StudentSelections { get; set; } = new();
        public Guid CurrentTermId { get; set; }
        public Term CurrentTerm { get; set; }
    }
}
