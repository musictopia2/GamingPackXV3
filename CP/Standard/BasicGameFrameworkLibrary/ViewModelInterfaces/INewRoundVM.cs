namespace BasicGameFrameworkLibrary.ViewModelInterfaces;
public interface INewRoundVM : IScreen
{
    bool CanStartNewRound { get; }
    Task StartNewRoundAsync();
}