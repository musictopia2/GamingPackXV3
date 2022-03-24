namespace SavannahCP.Data;
public enum EnumSelectType
{
    None,
    FromHand,
    FromStock,
    FromDiscard1,
    FromDiscard2, //this is iffy.  if this is from another player, then needs to think about how to handle (since its not just 2 players anymore).
    FromPile
}