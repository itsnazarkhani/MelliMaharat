namespace MelliMaharat.Wpf.ViewModels.Pages.Manager;

public class SelectionsPageVM : BaseVM
{
    #region Constructors
    public SelectionsPageVM()
    {
        Model = EmptySelection;
        foreach (var item in _repo.GetAll())
            Models.Add(item);
    }
    #endregion
    #region Fields
    readonly SelectionRepo _repo = new();
    #endregion
    #region Properties
    public ObservableCollection<Selection> Models { get; set; } = [];
    public Selection Model
    { 
        get => field; 
        set
        {
            field = value;
            field ??= EmptySelection;
            ValidateAllProperties(this);
            OnPropertyChanged();
            OnPropertyChanged(nameof(Student));
            OnPropertyChanged(nameof(Master));
            OnPropertyChanged(nameof(Score));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Year));
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(NationalCode));
        }
    }
    public string Id // Presentation Id
    {
        get => Model.Presentation.Id == default ? Empty : Model.Presentation.Id.ToString(); 
        set
        {
            try
            {
                Model.Presentation.Id = Guid.Parse(value);
            }
            catch (Exception ex)
            {
                Show(ex.Message, "Dont Touch This!");
                return;
            }
            ValidateProperty(Model.Presentation);
        }
    }
    public string NationalCode // Student National Code
    { 
        get => Model.Student.User.PersonInformation.NationalCode; 
        set
        {
            Model.Student.User.PersonInformation.NationalCode = value;

            if (IsNullOrEmpty(value.Trim()) || IsNullOrWhiteSpace(value.Trim()))
            {
                AddError("This Field Is Required!");
                return;
            }
            ValidateProperty(Model.Student.User.PersonInformation);
        }
    }
    public string Student
    { 
        get
        {
            var result = Model.Student.User.PersonInformation.FirstName + " " + Model.Student.User.PersonInformation.LastName; 
            return IsNullOrEmpty(result.Trim()) || IsNullOrWhiteSpace(result.Trim()) ? Empty : result;
        }
        set => Show("This Field Is Read-Only!");
    }
    public string Master
    { 
        get 
        {
            var result = Model.Presentation.Master.User.PersonInformation.FirstName + " " + Model.Presentation.Master.User.PersonInformation.LastName; 
            return IsNullOrWhiteSpace(result.Trim()) || IsNullOrEmpty(result.Trim()) ? Empty : result;
        }
        set => Show("This Field Is Read-Only!");
    }
    public string Score 
    { 
        get => Model.Score == default ? Empty : Model.Score.ToString(); 
        set
        {
            try
            {
                Model.Score = decimal.Parse(value);
            }
            catch (Exception ex)
            {
                Model.Score = default;
                AddError(ex.Message);
                return;
            }
            ValidateProperty(Model);
        }
    }
    public string Name // Lesson Name
    { 
        get => Model.Presentation.Lesson.Name; 
        set
        {
            Model.Presentation.Lesson.Name = value;
            ValidateProperty(Model.Presentation.Lesson);
        }
    }
    public string Year // Education Year
    { 
        get => Model.Term.Year == default ? Empty : Model.Term.Year.ToString(); 
        set
        {
            try
            {
                Model.Term.Year = int.Parse(value);
            }
            catch (Exception ex)
            {
                Model.Term.Year = default;
                AddError(ex.Message);
                return;
            }
            ValidateProperty(Model.Term);
        }
    }
    #endregion
    #region Commands
    public CommandRelay DeleteCommand => field ??= new(Delete);
    public CommandRelay UpdateCommand => field ??= new(Update);
    public CommandRelay AddCommand => field ??= new(Add);

    void Delete()
    {
        var (result, message) = _repo.Remove(Model);
        if (result <= 0)
            Show(message, "Delete Operation Failed!");
        else
            Models.Remove(Model);
    }
    void Add()
    {
        var (result, message) = _repo.Add(Model, NationalCode, Guid.Parse(Id));
        
        if (result == 0)
            Show(message, "An Unknown Error Happend!");
        else if (result <= 0)
            Show(message);
        else
            Models.Add(Model);
    }
    void Update()
    {
        var (result, message) = _repo.Update(Model);
     
        if (result <= 0)
            Show(message);
        else
        {
            var selected = Model;
            var index = Models.IndexOf(selected);

            if (index >= 0)
            {
                Models[index] = null!;
                Models[index] = selected;
            }
        }
    }
    #endregion
}
