﻿namespace Phase10CP.SetClasses;
public class SetInfo
{
    public EnumPhase10Sets SetType;
    public bool HasLaid;
    public bool DidSucceed;
    public int HowMany;
    public DeckRegularDict<Phase10CardInformation>? SetCards;
}