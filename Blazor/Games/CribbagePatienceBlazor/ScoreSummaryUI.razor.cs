namespace CribbagePatienceBlazor;
public partial class ScoreSummaryUI
{
    [Parameter]
    public BasicList<int> Scores { get; set; } = new();
}