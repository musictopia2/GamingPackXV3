﻿namespace ChessCP.Data;
public class PieceCP : CheckerChessPieceCP<EnumColorChoice>
{
    public EnumPieceType WhichPiece { get; set; }
}