namespace MahJongSolitaireCP.Logic;
public static class MahJongSolitaireStaticFunctions
{
    private static async Task<BasicList<BoardInfo>> GetPreviousListAsync(BasicList<BoardInfo> list)
    {
        string str = await js.SerializeObjectAsync(list);
        return await js.DeserializeObjectAsync<BasicList<BoardInfo>>(str);
    }
    public static async Task SaveMoveAsync(MahJongSolitaireSaveInfo games)
    {
        games.PreviousList = await GetPreviousListAsync(games.BoardList);
    }
}