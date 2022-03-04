namespace HuseHeartsCP.Logic;
public static class Extensions
{
    public static int WhoShotMoon(this PlayerCollection<HuseHeartsPlayerItem> thisList)
    {
        var firstList = thisList.Where(Items => Items.HadPoints == true).ToBasicList();
        if (firstList.Count == 1)
        {
            return firstList.Single().Id;
        }
        return 0;
    }
}