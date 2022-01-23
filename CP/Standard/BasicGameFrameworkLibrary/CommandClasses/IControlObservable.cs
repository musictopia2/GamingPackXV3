namespace BasicGameFrameworkLibrary.CommandClasses;
public interface IControlObservable
{
    bool CanExecute();
    void ReportCanExecuteChange();
    EnumCommandBusyCategory BusyCategory { get; set; }
}