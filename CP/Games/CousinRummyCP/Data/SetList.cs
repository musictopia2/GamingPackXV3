namespace CousinRummyCP.Data;
public class SetList
{
    public string Description { get; set; } = "";
    public BasicList<SetInfo> PhaseSets = new();
}