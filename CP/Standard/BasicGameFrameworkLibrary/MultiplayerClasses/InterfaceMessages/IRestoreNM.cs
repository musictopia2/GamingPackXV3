﻿namespace BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages;
public interface IRestoreNM
{
    Task RestoreMessageAsync(string payLoad);
}