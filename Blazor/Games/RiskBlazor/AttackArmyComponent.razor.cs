namespace RiskBlazor;
public partial class AttackArmyComponent
{
    [Parameter]
    public int HowManyArmies { get; set; }
    [Parameter]
    public string Color { get; set; } = "";
}