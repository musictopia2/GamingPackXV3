namespace BasicGameFrameworkLibrary.BasicGameDataClasses;
public class BasicData
{
    public bool MultiPlayer { get; set; }
    public string NickName { get; set; } = "";
    private bool _gameDataLoading;
    public bool GameDataLoading
    {
        get { return _gameDataLoading; }
        set
        {
            _gameDataLoading = value;
            ChangeState?.Invoke();
        }
    }
    public bool Client { get; set; }
    public EnumGamePackageMode GamePackageMode { get; set; } = EnumGamePackageMode.None; //default to none.  will require showing what it is.
    public Action? ChangeState { get; set; }
    public Action? DoFullScreen { get; set; }
}