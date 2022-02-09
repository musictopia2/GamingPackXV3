using BasicGameFrameworkLibrary.StandardImplementations.Settings;
using CommonBasicLibraries.BasicDataSettingsAndProcesses;
using System.IO;
using System.Threading.Tasks;
using ff = CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.FileFunctions.FileFunctions;
using jj = CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.FileHelpers;
namespace BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
public static class NativeGlobalSettingsFileAccess
{

    private static string GetGlobalSettingsPath()
    {
        string output = NativeFileAccessSetUp.GetParentDirectory();
        output = Path.Combine(output, "settings.json");
        return output;
    }

    public static async Task LoadSettingsAsync()
    {
        //ignore js.
        //must be in this class since this is wpf anyways.
        GlobalDataModel output;
        string tempPath = GetGlobalSettingsPath();
        if (ff.FileExists(tempPath) == false)
        {
            output = new();
        }
        else
        {
            output = await jj.RetrieveSavedObjectAsync<GlobalDataModel>(tempPath);
        }


        //if (js.ContainsKey(GlobalDataModel.LocalStorageKey) == false)
        //{
        //    output = new GlobalDataModel(); //just return a new global data model if not there
        //}
        //else
        //{
        //    output = await js.StorageGetItemAsync<GlobalDataModel>(GlobalDataModel.LocalStorageKey);
        //}
        GlobalDataModel.DataContext = output;
    }
    public static async Task SaveSettingsAsync()
    {
        if (GlobalDataModel.DataContext is null)
        {
            throw new CustomBasicException("Must have the data information filled out");
        }
        string tempPath = GetGlobalSettingsPath();
        await jj.SaveObjectAsync(tempPath, GlobalDataModel.DataContext);
    }
}
