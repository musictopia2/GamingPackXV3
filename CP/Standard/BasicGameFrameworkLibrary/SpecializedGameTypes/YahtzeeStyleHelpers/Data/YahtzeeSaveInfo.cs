namespace BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Data;
public class YahtzeeSaveInfo<D> : BasicSavedDiceClass<D, YahtzeePlayerItem<D>>
where D : SimpleDice, new()
{
    public int Begins { get; set; }
    private int _round;
    public int Round
    {
        get { return _round; }
        set
        {
            if (SetProperty(ref _round, value))
            {
                if (_model != null)
                {
                    _model.Round = value;
                }
            }
        }
    }
    private YahtzeeVMData<D>? _model; //this is needed so it can hook up.
    internal void LoadMod(YahtzeeVMData<D> model)
    {
        _model = model;
        _model.Round = Round;
    }
}