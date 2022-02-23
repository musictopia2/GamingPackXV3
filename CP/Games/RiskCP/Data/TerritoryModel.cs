﻿namespace RiskCP.Data;
public class TerritoryModel
{
    public string Name { get; set; } = "";
    public int Owns { get; set; }
    public string Color { get; set; } = cc.Transparent.ToWebColor();
    public int Armies { get; set; }
    public BasicList<int> Neighbors { get; set; } = new();
    public int Id { get; set; }
}