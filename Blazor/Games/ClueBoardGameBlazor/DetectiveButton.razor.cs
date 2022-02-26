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
            CommandObject = DataContext!.MakePredictionCommand!;
        }
        base.OnParametersSet();
    }
    private string PredictionMethod()
    {
        return _detective!.Category switch
        {
            EnumCardType.IsRoom => nameof(ClueBoardGameMainViewModel.CurrentRoomClick),
            EnumCardType.IsWeapon => nameof(ClueBoardGameMainViewModel.CurrentWeaponClick),
            EnumCardType.IsCharacter => nameof(ClueBoardGameMainViewModel.CurrentCharacterClick),
            _ => "",
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