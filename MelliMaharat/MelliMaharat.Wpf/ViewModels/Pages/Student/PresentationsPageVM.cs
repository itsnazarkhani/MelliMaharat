namespace MelliMaharat.Wpf.ViewModels.Pages.Student;

public class PresentationsPageVM : BaseVM
{
    #region Constructors
    public PresentationsPageVM(Models.Student student)
    {
        Model = default!; 
        foreach (var item in _repo.GetAll(student))
            Models.Add(item);
    }
    #endregion
    #region Fields
    readonly PresentationRepo _repo = new();
    #endregion
    #region Properties
    public ObservableCollection<Presentation> Models { get; set; } = [];
    public Presentation Model 
    { 
        get => field; 
        set
        {
            field = value;
            field ??= EmptyPresentation;
            ValidateAllProperties(this);
            OnPropertyChanged();
            OnPropertyChanged(nameof(MasterName));
            OnPropertyChanged(nameof(LessonName));
            OnPropertyChanged(nameof(DayHold));
            OnPropertyChanged(nameof(StartTime));
            OnPropertyChanged(nameof(EndTime));
        }
    }
    public string MasterName 
    { 
        get
        {
            var result = Model.Master.User.PersonInformation.FirstName + " " + Model.Master.User.PersonInformation.LastName;
            return IsNullOrEmpty(result.Trim()) || IsNullOrWhiteSpace(result.Trim()) ? Empty : result;
        }
        set => Show("This Property Should Not Be Touched!");
    }
    public string LessonName 
    {
        get => Model.Lesson.Name;
        set
        {
            Model.Lesson.Name = value;
            ValidateProperty(Model.Lesson);
        }
    }
    public string DayHold 
    {
        get => Model.DayHold;
        set
        {
            Model.DayHold = value;
            ValidateProperty(Model);
        }
    }
    public string StartTime 
    {
        get => Model.StartTime == default ? Empty : Model.StartTime.ToString();
        set
        {
            try
            {
                Model.StartTime = TimeOnly.Parse(value);
            }
            catch (Exception ex)
            {
                Model.StartTime = default;
                AddError(ex.Message);
                return;
            }
            ValidateProperty(Model);
        }
    }
    public string EndTime 
    {
        get => Model.EndTime == default ? Empty : Model.EndTime.ToString();
        set
        {
            try
            {
                Model.EndTime = TimeOnly.Parse(value);
            }
            catch (Exception ex)
            {
                Model.EndTime = default;
                AddError(ex.Message);
                return;
            }
            ValidateProperty(Model);
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
    void Add() => Models.Add(Model);
    void Update()
    {
        var (result, message) = _repo.Update(Model);
        if (result <= 0)
            Show(message, "Update Operation Failed!");
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
