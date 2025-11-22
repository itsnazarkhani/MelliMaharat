namespace DataForge.Wpf.ViewModels;

public partial class BaseVM<TModel> : INotifyDataErrorInfo where TModel : notnull , new()
{
    protected readonly Dictionary<string, List<string>> _errors = [];
    public bool HasErrors => _errors.Count > 0;


    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public IEnumerable GetErrors([CallerMemberName]string? propertyName = "")
    {
        if (string.IsNullOrEmpty(propertyName))
            return _errors.SelectMany(x => x.Value).ToList();

        return _errors.ContainsKey(propertyName)? _errors[propertyName] : [];
    }

    protected void ValidateProperty(TModel? instance = default, [CallerMemberName] string? propertyName = null)
    {
        if (instance is null)
            return;

        if (string.IsNullOrEmpty(propertyName))
            return;

        _errors.Remove(propertyName);

        var context = new ValidationContext(instance) { MemberName = propertyName };
        var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
        var value = instance.GetType().GetProperty(propertyName)?.GetValue(instance);
        bool isValid = Validator.TryValidateProperty(value, context, results);

        if (!isValid)
        {
            _errors[propertyName] = results.Select(x => x.ErrorMessage ?? string.Empty).ToList();

            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }

    public bool ValidateAllProperties()
    {
        _errors.Clear();
        var context = new ValidationContext(Model);
        var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

        bool isValid = Validator.TryValidateObject(Model, context, results, true);

        foreach (var result in results)
        {
            foreach (var memberName in result.MemberNames)
            {
                if (!_errors.ContainsKey(memberName))
                    _errors[memberName] = [];

                _errors[memberName].Add(result.ErrorMessage ?? string.Empty);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(memberName));
            }
        }
        return isValid;
    }
}
