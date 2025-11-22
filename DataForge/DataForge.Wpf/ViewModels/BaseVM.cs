namespace DataForge.Wpf.ViewModels;

public partial class BaseVM<TModel>
{
    public TModel Model { get; }
    public BaseVM(TModel model)
    {
        Model = model ?? throw new ArgumentNullException(nameof(model));
    }
    public BaseVM()
    {
        Model = new();
    }
}
