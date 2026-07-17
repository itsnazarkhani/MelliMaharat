namespace MelliMaharat.Wpf.Commands;

public class CommandRelay : CommandBase
{
    private readonly Action _execute;
    private readonly Func<bool>? _canExecute;

    public CommandRelay(Action execute) : this(execute, null) { }
    public CommandRelay(Action execute, Func<bool>? canExecute)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public override bool CanExecute(object? parameter = null) => _canExecute is null || _canExecute();
    public override void Execute(object? parameter = null) => _execute();
}

public class CommandRelay<T> : CommandBase
{
    private readonly Action<T> _execute;
    private readonly Func<T, bool>? _canExecute;
    public CommandRelay(Action<T> execute) : this(execute, null) { }
    public CommandRelay(Action<T> execute, Func<T, bool>? canExecute)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public override bool CanExecute(object? parameter) => _canExecute is null || (parameter is T t && _canExecute(t));
    public override void Execute(object? parameter)
    {
        if(parameter is T t)
            _execute(t); 
    }
}