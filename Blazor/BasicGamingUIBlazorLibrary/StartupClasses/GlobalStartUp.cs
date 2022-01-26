﻿namespace BasicGamingUIBlazorLibrary.StartupClasses;
public static class GlobalStartUp
{
    public static Action? StartBootStrap { get; set; }
    public static IJSRuntime? JsRuntime { get; set; }

    private readonly static BasicList<string> _keys = new();
    public static BasicList<string> KeysToSave => _keys;
}