﻿namespace MahJongSolitaireCP.Data;
[SingletonGame]
public class MahJongSolitaireModGlobal
{
    public int SecondSelected { get; set; }
    public MahjongShuffler TileList = new();
}