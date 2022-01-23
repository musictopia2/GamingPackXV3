namespace BasicGameFrameworkLibrary.CommandClasses;
public interface IAsyncCommand
{
    bool CanExecute(object? parameter);
    Task ExecuteAsync(object? parameter);
}