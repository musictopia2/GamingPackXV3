namespace ChinazoCP.Data;
public class SavedSet
{
    public DeckRegularDict<ChinazoCard> CardList { get; set; } = new();
    public EnumRummyType WhatSet;
    public bool UseSecond;
    public int FirstNumber;
}