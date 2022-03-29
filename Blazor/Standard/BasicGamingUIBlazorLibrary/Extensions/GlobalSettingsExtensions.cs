namespace BasicGamingUIBlazorLibrary.Extensions;
public static class GlobalSettingsExtensions
{
    public static async Task LoadGlobalDataAsync(this IJSRuntime js)
    {
        GlobalDataModel output;
        if (js.ContainsKey(GlobalDataModel.LocalStorageKey) == false)
        {
            output = new GlobalDataModel(); //just return a new global data model if not there
        }
        else
        {
            output = await js.StorageGetItemAsync<GlobalDataModel>(GlobalDataModel.LocalStorageKey);
        }
        GlobalDataModel.DataContext = output;
    }
    public static async Task SaveGlobalDataAsync(this IJSRuntime js)
    {
        if (GlobalDataModel.DataContext == null)
        {
            throw new CustomBasicException("There is no global data.  Should have called the LoadGlobalDataAsync then populated it first");
        }
        if (string.IsNullOrWhiteSpace(GlobalDataModel.DataContext.NickName))
        {
            throw new CustomBasicException("Should have populated the nick name first");
        }
        await js.StorageSetItemAsync(GlobalDataModel.LocalStorageKey, GlobalDataModel.DataContext);
    }
}