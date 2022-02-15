﻿namespace BasicGameFrameworkLibrary.NetworkingClasses.Extensions;
public static class CommonMessages
{
    public static async Task SendReadyMessageAsync(this IGameNetwork network, string yourName, string hostName)
    {
        await network.SendToParticularPlayerAsync("ready", yourName, hostName);
    }
    public static async Task SendLoadGameMessageAsync<T>(this IGameNetwork network, T payLoad)
    {
        await network.SendAllAsync("loadgame", payLoad);
    }
    public static async Task SendNewGameAsync<T>(this IGameNetwork network, T payLoad)
    {
        await network.SendAllAsync("newgame", payLoad);
    }
    public static async Task SendRestoreGameAsync<T>(this IGameNetwork network, T payLoad)
    {
        //hopefully restoregame works even for the new client.
        await network.SendAllAsync("restoregame", payLoad); //try this way.  hopefully i don't need bypassing  if so, rethink again.
    }
    public static async Task SendMoveAsync<T>(this IGameNetwork network, T payLoad)
    {
        await network.SendAllAsync("move", payLoad);
    }
    public static async Task SendDiscardMessageAsync(this IGameNetwork network, int deck)
    {
        await network.SendAllAsync("discard", deck);
    }
    public static async Task SendEndTurnAsync(this IGameNetwork network)
    {
        await network.SendAllAsync("endturn");
    }
    public static async Task SendDrawAsync(this IGameNetwork network)
    {
        await network.SendAllAsync("drawcard");
    }
    public static async Task SendPlayDominoAsync(this IGameNetwork network, int deck)
    {
        await network.SendAllAsync("playdomino", deck);
    }
    public static async Task SendCustomDeckListAsync<D>(this IGameNetwork network, string status, DeckRegularDict<D> list)
        where D : class, IDeckObject
    {
        var output = list.GetDeckListFromObjectList();
        await network.SendAllAsync(status, output);
    }
}