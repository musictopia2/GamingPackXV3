﻿namespace BasicGameFrameworkLibrary.ScoreBoardClassesCP;
public interface IScoreBoard
{
    string TextToDisplay(ScoreColumnModel column, bool useAbbreviationForTrueFalse);
}