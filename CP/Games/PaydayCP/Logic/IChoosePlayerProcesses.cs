﻿namespace PaydayCP.Logic;
public interface IChoosePlayerProcesses
{
    Task ProcessChosenPlayerAsync();
    void LoadPlayerList();
}