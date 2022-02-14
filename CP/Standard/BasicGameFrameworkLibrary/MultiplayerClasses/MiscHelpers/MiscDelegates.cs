﻿namespace BasicGameFrameworkLibrary.MultiplayerClasses.MiscHelpers;
public static class MiscDelegates
{
    public static Func<Task>? ColorsFinishedAsync { get; set; }
    public static Func<BasicList<Type>>? GetMiscObjectsToReplace { get; set; }
    public static Func<Task>? ComputerChooseColorsAsync { get; set; }
    public static Func<Task>? ContinueColorsAsync { get; set; }
    public static Action? FillRestColors { get; set; }
    public static Action? ManuelSetColors { get; set; }
}