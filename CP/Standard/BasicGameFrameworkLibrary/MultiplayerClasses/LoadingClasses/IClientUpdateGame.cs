﻿namespace BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
public interface IClientUpdateGame
{
    Task UpdateGameAsync(string payload);
}