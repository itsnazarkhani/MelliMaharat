global using static System.ComponentModel.DataAnnotations.Validator;
namespace DataForge.Wpf.ViewModels;

public partial class BaseVM<TModel> : INotifyDataErrorInfo
{
    protected readonly Dictionary<string, List<string>> _errors = [];
    public bool HasErrors => _errors.Count > 0;

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public IEnumerable GetErrors([CallerMemberName]string? propertyName = "")
    {
        if (IsNullOrEmpty(propertyName))
            return _errors.SelectMany(x => x.Value).ToList();
        else
            return _errors.TryGetValue(propertyName, out var errorMessages) ? errorMessages : [];
    }

    protected void ValidateProperty(TModel? instance = default, [CallerMemberName] string? propertyName = null)
    {
        if (instance is null)
            return;
        if (IsNullOrEmpty(propertyName))
            return;

        _errors.Remove(propertyName);

        var context = new ValidationContext(instance) { MemberName = propertyName };
        var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
        var value = instance.GetType().GetProperty(propertyName)?.GetValue(instance);
        bool isValid = TryValidateProperty(value, context, results);

        if (!isValid)
        {
            _errors[propertyName] = results.Select(x => x.ErrorMessage ?? Empty).ToList();

            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }

    public bool ValidateAllProperties()
    {
        _errors.Clear();
        var context = new ValidationContext(Model);
        var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

        bool isValid = TryValidateObject(Model, context, results, true);

        foreach (var result in results)
        {
            foreach (var memberName in result.MemberNames)
            {
                if (!_errors.ContainsKey(memberName))
                    _errors[memberName] = [];

                _errors[memberName].Add(result.ErrorMessage ?? Empty);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(memberName));
            }
        }
        return isValid;
    }
}