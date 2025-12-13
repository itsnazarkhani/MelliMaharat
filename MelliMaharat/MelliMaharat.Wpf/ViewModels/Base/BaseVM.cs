namespace MelliMaharat.Wpf.ViewModels;

public partial class BaseVM { }

public partial class BaseVM<TModel> : BaseVM where TModel : notnull, new()
{
    public TModel Model
    {
        get => field;
        set
        {
            field = value;
            ValidateProperty(this);
        }
    }
    public BaseVM(TModel model) => Model = model ?? throw new ArgumentNullException(nameof(model));
    public BaseVM() => Model = new();   
}