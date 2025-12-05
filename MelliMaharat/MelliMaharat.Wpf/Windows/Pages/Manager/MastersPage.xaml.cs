namespace MelliMaharat.Wpf.Windows.Pages.Manager;

/// <summary>
/// Interaction logic for MastersPage.xaml
/// </summary>
public partial class MastersPage : Page
{
    List<Models.Master>? Masters { get; set; } = [];
    List<Person> Persons { get; set; } = [];
    Models.Master? Master { get; set; }
    ICollectionView? View { get; set; } = default;
    string Graduation { get; set; } = "Doctorate";
    public MastersPage()
    {
        //new MasterRepo().GetAll().ToList().ForEach(x => Masters?.Add(x));
        //Masters.ForEach(x => Persons.Add(x.PersonInformation));
        //InitializeComponent();
        ////DataContext = Masters;
        //Master = Masters?.First();
        //string firstName = Master!.PersonInformation.FirstName;
        //string fullName = Master!.PersonInformation.FullName;
        //Graduation = Master.Graduation;
        //txtFirstName.InputText = firstName;
        //this.DataContext = Graduation;
        //listbox.DataContext = Persons;

        //View = CollectionViewSource.GetDefaultView(Persons);
        //View.SortDescriptions.Clear();
        //View.SortDescriptions.Add(new SortDescription(nameof(Person.FirstName), ListSortDirection.Ascending));
        //listbox.ItemsSource = View;
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        //await Task.Delay(5000);
        //View!.SortDescriptions.Clear();
        //View.SortDescriptions.Add(new SortDescription(nameof(Person.Age), ListSortDirection.Ascending));
    }
}