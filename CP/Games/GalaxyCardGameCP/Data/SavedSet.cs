﻿namespace GalaxyCardGameCP.Data;
public class SavedSet
{
    public DeckRegularDict<GalaxyCardGameCardInformation> CardList { get; set; } = new();
    public EnumWhatSets WhatSet;
}