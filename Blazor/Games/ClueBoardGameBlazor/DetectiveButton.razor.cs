using CommonBasicLibraries.BasicDataSettingsAndProcesses;

namespace ClueBoardGameBlazor;
public partial class DetectiveButton : GraphicsCommand
{
    [CascadingParameter]
    public EnumDetectiveCategory DetectiveCategory { get; set; }
    [Parameter]
    public RectangleF Bounds { get; set; }
    [Parameter]
    [EditorRequired]
    public ClueBoardGameMainViewModel? DataContext { get; set; }
    private DetectiveInfo? _detective;
    protected override void OnInitialized()
    { 
        if (CommandParameter != null)
        {
            _detective = (DetectiveInfo)CommandParameter;
        }
        base.OnInitialized();
    }
    protected override void OnParametersSet()
    {
        if (DetectiveCategory == EnumDetectiveCategory.Notebook)
        {
            CommandObject = DataContext!.FillInClueCommand!;
        }
        else if (_detective != null)
        {
            CommandObject = PredictCommand();
        }
        base.OnParametersSet();
    }
    private ICustomCommand PredictCommand()
    {
        return _detective!.Category switch
        {
            EnumCardType.IsRoom => DataContext!.CurrentRoomClickCommand!,
            EnumCardType.IsWeapon => DataContext!.CurrentWeaponClickCommand!,
            EnumCardType.IsCharacter => DataContext!.CurrentCharacterClickCommand!,
            _ => throw new CustomBasicException("No predict command found")
        };
    }
    private bool WasSelected()
    {

        return _detective!.Category switch
        {
            EnumCardType.IsRoom => _detective.Name == DataContext!.VMData.CurrentRoomName,
            EnumCardType.IsWeapon => _detective.Name == DataContext!.VMData.CurrentWeaponName,
            EnumCardType.IsCharacter => _detective.Name == DataContext!.VMData.CurrentCharacterName,
            _ => false,
        };
    }
    private string FillColor()
    {
        if (_detective == null || DataContext == null)
        {
            return cc.Transparent;
        }
        if (DetectiveCategory == EnumDetectiveCategory.Notebook)
        {
            if (_detective.IsChecked)
            {
                return cc.LimeGreen;
            }
            return cc.Aqua;
        }
        if (WasSelected())
        {
            return cc.LimeGreen;
        }
        return cc.Aqua;
    }
}