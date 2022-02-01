namespace BuncoDiceGameBlazor.Views;
public class BuncoNewRoundView : BasicCustomButtonView<BuncoNewRoundViewModel>
{
    protected override string DisplayName => "New Round";
    protected override ICustomCommand Command => DataContext!.NewRoundCommand!;
}