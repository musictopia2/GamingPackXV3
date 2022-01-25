namespace BasicGameFrameworkLibrary.ViewModelInterfaces;
public interface IBlankGameVM : IMainScreen
{
    CommandContainer CommandContainer { get; set; }
}