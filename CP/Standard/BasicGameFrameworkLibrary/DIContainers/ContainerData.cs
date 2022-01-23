﻿namespace BasicGameFrameworkLibrary.DIContainers;
public class ContainerData
{
    //may need to be public so it can work with source generators in various projects.
    public Type? TypeIn { get; set; }
    public Type? TypeOut { get; set; }
    public BasicList<Type> AssignedFrom { get; set; } = new();
    public bool CanAssignFrom(Type type)
    {
        return AssignedFrom.Any(xx => xx == type);
    }
    public object? ThisObject { get; set; }
    public bool IsSingle { get; set; }
    public string Tag { get; set; } = "";
    public Func<object>? GetNewObject { get; set; } //i think this is still needed. 
}