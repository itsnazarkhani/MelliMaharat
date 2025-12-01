using MelliMaharat.Models;

namespace MelliMaharat.Web.ViewModels.Student;

public class SelectionTimesViewModel
{
    public IEnumerable<SelectionTime> Times { get; set; }
    public SelectionTime? ActiveSelection { get; set; }
}
