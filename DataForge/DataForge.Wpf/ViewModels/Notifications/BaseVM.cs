namespace DataForge.Wpf.ViewModels;

public partial class BaseVM<TModel> : INotifyPropertyChanged
{
    public int MyProperty { get; set; }
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}