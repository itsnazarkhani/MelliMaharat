using MelliMaharat.Models;

namespace MelliMaharat.Web.ViewModels.Student
{
    public class CommitPresentationSelectionViewModel
    {
        public Guid StudentId { get; set; }

        public Guid TermId { get; set; }

        public List<Guid> PresentationIds { get; set; } = new();

        public List<Presentation>? Presentations { get; set; }
    }
}
