using System.Security.Cryptography;

namespace MelliMaharat.Wpf.ViewModels.Pages.Master;

public class StudentsPageVM : BaseVM
{
    #region Constructors
    public StudentsPageVM(Models.Master master)
    {
        Model = EmptyStudent;
        foreach (var item in _repo.GetAll(master))
            Models.Add(item);
    }
    #endregion
    #region Fields
    readonly StudentRepo _repo = new();
    #endregion
    #region Properties
    public ObservableCollection<Models.Student> Models { get; set; } = [];
    public Models.Student Model
    {
        get => field;
        set
        {
            field = value;
            field ??= EmptyStudent;
            OnPropertyChanged();
            OnPropertyChanged(nameof(FirstName));
            OnPropertyChanged(nameof(LastName));
            OnPropertyChanged(nameof(BirthDate));
            OnPropertyChanged(nameof(NationalCode));
            OnPropertyChanged(nameof(PhoneNumber));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(Username));
        }
    }
    public string FirstName
    {
        get => Model.User.PersonInformation.FirstName;
        set
        {
            Model.User.PersonInformation.FirstName = value;
            OnPropertyChanged();
            ValidateProperty(Model.User.PersonInformation);
        }
    }
    public string LastName
    {
        get => Model.User.PersonInformation.LastName;
        set
        {
            Model.User.PersonInformation.LastName = value;
            OnPropertyChanged();
            ValidateProperty(Model.User.PersonInformation);
        }
    }
    public string BirthDate
    {
        get => Model.User.PersonInformation.BirthDate == default ? Empty : Model.User.PersonInformation.BirthDate.ToString();
        set
        {
            Model.User.PersonInformation.BirthDate = DateOnly.Parse(value);
            OnPropertyChanged();
            ValidateProperty(Model.User.PersonInformation);
        }
    }
    public string NationalCode
    {
        get => Model.User.PersonInformation.NationalCode;
        set
        {
            Model.User.PersonInformation.NationalCode = value;
            OnPropertyChanged();
            ValidateProperty(Model.User.PersonInformation);
        }
    }
    public string PhoneNumber
    {
        get => Model.User.PersonInformation.PhoneNumber;
        set
        {
            Model.User.PersonInformation.PhoneNumber = value;
            OnPropertyChanged();
            ValidateProperty(Model.User.PersonInformation);
        }
    }
    public string Email
    {
        get => Model.User.Email;
        set
        {
            Model.User.Email = value;
            OnPropertyChanged();
            ValidateProperty(Model.User);
        }
    }
    public string Username
    {
        get => Model.User.Username;
        set
        {
            Model.User.Username = value;
            OnPropertyChanged();
            ValidateProperty(Model.User);
        }
    }
    #endregion
    #region Commands
    public CommandRelay DeleteCommand => field ??= new(Delete);
    public CommandRelay UpdateCommand => field ??= new(Update);
    public CommandRelay AddCommand => field ??= new(Add);

    void Delete() => Models.Remove(Model);
    void Add() => Models.Add(Model);
    void Update()
    {
        var selected = Model;
        var index = Models.IndexOf(selected);

        if (index >= 0)
        {
            Models[index] = null!;
            Models[index] = selected;
        }
    }
    #endregion
}
