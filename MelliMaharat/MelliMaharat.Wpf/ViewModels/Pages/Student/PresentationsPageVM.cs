namespace MelliMaharat.Wpf.ViewModels.Pages.Student;

public class PresentationsPageVM : BaseVM
{
    #region Constructors
    public PresentationsPageVM(Models.Student student)
    {
        Model = EmptyPresentation; 
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
        set
        {
            Show("This Property Should Not Be Touched!");
            OnPropertyChanged();
        }
    }
    public string LessonName 
    {
        get => Model.Lesson.Name;
        set
        {
            Model.Lesson.Name = value;
            OnPropertyChanged();
            ValidateProperty(Model.Lesson);
        }
    }
    public string DayHold 
    {
        get => Model.DayHold;
        set
        {
            Model.DayHold = value;
            OnPropertyChanged();
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
                OnPropertyChanged();
                return;
            }
            OnPropertyChanged();
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
                OnPropertyChanged();
                return;
            }
            OnPropertyChanged();
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
        if (_repo.Remove(Model) > 0)
            Models.Remove(Model);
        else
            Show("Delete Operation Failed!");
    }
    void Add() => Models.Add(Model);
    void Update()
    {
        if (_repo.Update(Model) > 0)
        {
            var selected = Model;
            var index = Models.IndexOf(selected);

            if (index >= 0)
            {
                Models[index] = null!;
                Models[index] = selected;
            }
        }
        else
            Show("Update Operation Failed!");
    }
    #endregion
}
