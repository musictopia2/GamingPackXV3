namespace SkuckCardGameBlazor.Views;
public partial class SkuckChoosePlayView
{
    private ICustomCommand PlayCommand => DataContext!.FirstPlayCommand!;
}