namespace CountdownCP.Data;
[UseScoreboard]
public partial class CountdownPlayerItem : SimplePlayer
{//anything needed is here
    public BasicList<SimpleNumber> NumberList { get; set; } = new();
}