namespace BasicGameFrameworkLibrary.ViewModelInterfaces;
public interface INewGameVM : IScreen
{
    bool CanStartNewGame();
    Task StartNewGameAsync();
}