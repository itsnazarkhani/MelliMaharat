using System.Collections;

namespace DataForge.Wpf.ViewModels.Validations;

public partial class AuthenticateWindowVM : INotifyDataErrorInfo
{
    private Dictionary<string, List<string>> _errors;
    public bool HasErrors => _errors.Any();

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public IEnumerable GetErrors(string? propertyName)
    {
        throw new NotImplementedException();
    }
}