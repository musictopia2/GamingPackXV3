﻿namespace BasicGameFrameworkLibrary.MultiplayerClasses.GameContainers;
public interface ISaveContainer<P, S>
    where P : class, IPlayerItem, new()
    where S : BasicSavedGameClass<P>
{
    S SaveRoot { get; set; }
}