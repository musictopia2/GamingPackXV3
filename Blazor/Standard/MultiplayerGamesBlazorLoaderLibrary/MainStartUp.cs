﻿namespace MultiplayerGamesBlazorLoaderLibrary;
public class MainStartUp : IStartUp
{
    public static bool? IsWasm { get; set; }
    void IStartUp.RegisterCustomClasses(IGamePackageDIContainer container, bool multiplayer, BasicData data)
    {
        if (IsWasm.HasValue == false)
        {
            throw new CustomBasicException("Must figure out if its wasm since autoresume classes are different for wasm");
        }
        if (data.GamePackageMode == EnumGamePackageMode.Debug)
        {
            throw new CustomBasicException("Only production is supported.");
        }
        if (multiplayer)
        {
            if (IsWasm.Value == true)
            {
                container.RegisterType<MultiplayerReleaseAutoResume>();
            }
            else
            {
                container.RegisterType<MultiPlayerReleaseNativeFileAccessAutoResume>();
            }
            container.RegisterType<NetworkStartUp>(); //i think i need this here too (?)
        }
        else
        {
            if (IsWasm.Value == true)
            {
                container.RegisterType<SinglePlayerNoSave>();
            }
            else
            {
                container.RegisterType<SinglePlayerReleaseNativeFileAccessAutoResume>();
            }
        }
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(container);
    }
    void IStartUp.StartVariables(BasicData data)
    {
        if (GlobalDataModel.DataContext == null)
        {
            throw new CustomBasicException("Must have the data filled out in order to get the nick names");
        }
        data.NickName = GlobalDataModel.DataContext.NickName; //looks like needs this.
    }
}