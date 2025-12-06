namespace MelliMaharat.Wpf.ViewModels;

public partial class BaseVM : INotifyDataErrorInfo
{
    protected readonly Dictionary<string, List<string>> _errors = [];
    public bool HasErrors => _errors.Count > 0;

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
    protected void OnErrorsChanged(string propertyName) =>
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));

    public IEnumerable GetErrors([CallerMemberName] string? propertyName = "")
    {
        if (IsNullOrEmpty(propertyName))
            return _errors.SelectMany(x => x.Value).ToList();
        else
            return _errors.TryGetValue(propertyName, out var errorMessages) ? errorMessages : [];
    }

    /// <summary>
    /// Non-Generic Property Validator
    /// </summary>
    /// <param name="instance">the model or object you want to validate</param>
    /// <param name="propertyName"></param>
    /// <example>in setter: ValidateProperty(new PersonsObservableCollection().First(), new PersonsObservableCollection().First().Username)</example>
    protected virtual void ValidateProperty(object? instance = default, [CallerMemberName] string? propertyName = null)
    {
        if (instance is null)
            return;
        if (IsNullOrEmpty(propertyName))
            return;

        _errors.Remove(propertyName);
        OnErrorsChanged(propertyName);

        var context = new ValidationContext(instance) { MemberName = propertyName };
        var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
        var value = instance.GetType().GetProperty(propertyName)?.GetValue(instance);
        bool isValid = TryValidateProperty(value, context, results);

        if (!isValid)
        {
            _errors[propertyName] = [.. results.Select(x => x.ErrorMessage ?? Empty)];
            OnErrorsChanged(propertyName);
        }
    }
    /// <summary>
    /// Non-Generic Validator for All Properties of inserting object
    /// </summary>
    /// <param name="obj">the object you want to validate all properties of it</param>
    /// <returns>is there any errors or not</returns>
    public virtual bool ValidateAllProperties(object obj)
    {
        foreach (var propertyName in _errors.Keys.ToList())
        {
            _errors.Remove(propertyName);
            OnErrorsChanged(propertyName);
        }

        var context = new ValidationContext(obj);
        var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

        bool isValid = TryValidateObject(obj, context, results, true);

        foreach (var result in results)
        {
            foreach (var memberName in result.MemberNames)
            {
                if (!_errors.ContainsKey(memberName))
                    _errors[memberName] = [];

                _errors[memberName].Add(result.ErrorMessage ?? Empty);
                OnErrorsChanged(memberName);
            }
        }
        return isValid;
    }
}

public partial class BaseVM<TModel> : BaseVM
{
    protected void ValidateProperty(TModel? instance = default, [CallerMemberName] string? propertyName = null)
    {
        if (instance is null)
            return;
        if (IsNullOrEmpty(propertyName))
            return;

        _errors.Remove(propertyName);
        OnErrorsChanged(propertyName);

        var context = new ValidationContext(instance) { MemberName = propertyName };
        var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
        var value = instance.GetType().GetProperty(propertyName)?.GetValue(instance);
        bool isValid = default;
        try
        {
            isValid = TryValidateProperty(value, context, results);
        }
        catch (Exception ex)
        {
            _errors[propertyName] = [ex.Message];
            OnErrorsChanged(propertyName);
            isValid = true;
        }
        finally
        {
            if (!isValid)
            {
                _errors[propertyName] = [.. results.Select(x => x.ErrorMessage ?? Empty)];
                OnErrorsChanged(propertyName);
            }
        }
    }

    public bool ValidateAllProperties()
    {
        foreach (var propertyName in _errors.Keys.ToList())
        {
            _errors.Remove(propertyName);
            OnErrorsChanged(propertyName);
        }

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
                OnErrorsChanged(memberName);
            }
        }
        return isValid;
    }
}