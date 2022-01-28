namespace BasicGameFrameworkLibrary.ChooserClasses;
public interface IListViewPicker
{
    ICustomCommand ItemSelectedCommand { get; }
    BasicList<ListPieceModel> TextList { get; }
}