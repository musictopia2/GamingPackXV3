﻿namespace BasicGameFrameworkLibrary.MultiplayerClasses.InterfacesForHelpers;
public interface IMoveProcesses<M>
{
    Task MakeMoveAsync(M space);
}