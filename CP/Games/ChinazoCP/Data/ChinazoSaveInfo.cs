namespace ChinazoCP.Data;
[SingletonGame]
public class ChinazoSaveInfo : BasicSavedCardClass<ChinazoPlayerItem, ChinazoCard>, IMappable, ISaveInfo
{
    public BasicList<SavedSet> SetList { get; set; } = new();
    public int Round { get; set; }
    public bool HadChinazo { get; set; }
}