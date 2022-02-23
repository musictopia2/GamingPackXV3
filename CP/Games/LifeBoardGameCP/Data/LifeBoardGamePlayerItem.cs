namespace LifeBoardGameCP.Data;
public class LifeBoardGamePlayerItem : PlayerBoardGame<EnumColorChoice>
{
    public override bool DidChooseColor => Color.IsNull == false && Color != EnumColorChoice.None;
    public override void Clear()
    {
        Color = EnumColorChoice.None;
    }
    public DeckRegularDict<LifeBaseCard> Hand { get; set; } = new (); //this time, can use the standard hand.
    public EnumStart OptionChosen { get; set; }
    public int Position { get; set; } // where you are at on the board.
    public EnumFinal LastMove { get; set; }
    public int Distance { get; set; }
    public EnumGender Gender { get; set; }
    public BasicList<EnumGender> ChildrenList { get; set; } = new();
    public decimal Loans { get; set; }
    public decimal Salary { get; set; }
    public decimal MoneyEarned { get; set; }
    public int FirstStock { get; set; }
    public int SecondStock { get; set; }
    public bool CarIsInsured { get; set; }
    public bool HouseIsInsured { get; set; }
    public bool DegreeObtained { get; set; }
    public int TilesCollected { get; set; }
    public string HouseName { get; set; } = "";
    public string Career1 { get; set; } = "";
    public string Career2 { get; set; } = "";
    public BasicList<TileInfo> TileList { get; set; } = new();
    public EnumTurnInfo WhatTurn { get; set; }
    public bool Married { get; set; }
}