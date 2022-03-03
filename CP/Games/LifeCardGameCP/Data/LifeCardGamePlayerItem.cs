namespace LifeCardGameCP.Data;
[UseScoreboard]
public partial class LifeCardGamePlayerItem : PlayerSingleHand<LifeCardGameCardInformation>
{//anything needed is here
    [ScoreColumn]
    public int Points { get; set; }
    public string LifeString { get; set; } = "";
    [JsonIgnore]
    public LifeStoryHand? LifeStory { get; set; }
}