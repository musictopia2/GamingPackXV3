namespace SolitaireBoardGameBlazor.Views;
public partial class SolitaireBoardGameMainView
{
    private static SolitaireBoardGameCollection GetSpaceList()
    {
        SolitaireBoardGameSaveInfo thisSave = aa.Resolver!.Resolve<SolitaireBoardGameSaveInfo>();
        return thisSave.SpaceList;
    }
}