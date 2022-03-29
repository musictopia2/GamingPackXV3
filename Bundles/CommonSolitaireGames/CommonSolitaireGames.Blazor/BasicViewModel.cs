namespace CommonSolitaireGames.Blazor;
public class BasicViewModel : LoaderViewModel
{
    public BasicViewModel(IStartUp starts) : base(starts)
    {
    }
    public override string Title => "CommonSolitaireGames";
    protected override void GenerateGameList()
    {
        GameList = new () { "MahJong Solitaire", "Clock Solitaire", "Cribbage Patience", "Eagle Wings Solitaire", "Beleagured Castle", "Heap Solitaire", "Triangle Solitaire", "Vegas Solitaire"};
    }
    protected override Type GetGameType()
    {
        if (GameName == "MahJong Solitaire")
        {
            return typeof(MahJongSolitaireBlazor.Index);
        }
        if (GameName == "Clock Solitaire")
        {
            return typeof(ClockSolitaireBlazor.Index);
        }
        if (GameName == "Cribbage Patience")
        {
            return typeof(CribbagePatienceBlazor.Index);
        }
        if (GameName == "Eagle Wings Solitaire")
        {
            return typeof(EagleWingsSolitaireBlazor.Index);
        }
        if (GameName == "Beleagured Castle")
        {
            return typeof(BeleaguredCastleBlazor.Index);
        }
        if (GameName == "Heap Solitaire")
        {
            return typeof(HeapSolitaireBlazor.Index);
        }
        if (GameName == "Triangle Solitaire")
        {
            return typeof(TriangleSolitaireBlazor.Index);
        }
        if (GameName == "Vegas Solitaire")
        {
            return typeof(VegasSolitaireBlazor.Index);
        }
        throw new CustomBasicException("Game Not Found");
    }
    protected override IGameBootstrapper ChooseGame()
    {
        if (GameName == "MahJong Solitaire")
        {
            return new MahJongSolitaireBlazor.Bootstrapper(Starts, Mode);
        }
        if (GameName == "Clock Solitaire")
        {
            return new ClockSolitaireBlazor.Bootstrapper(Starts, Mode);
        }
        if (GameName == "Cribbage Patience")
        {
            return new CribbagePatienceBlazor.Bootstrapper(Starts, Mode);
        }
        if (GameName == "Eagle Wings Solitaire")
        {
            return new EagleWingsSolitaireBlazor.Bootstrapper(Starts, Mode);
        }
        if (GameName == "Beleagured Castle")
        {
            return new BeleaguredCastleBlazor.Bootstrapper(Starts, Mode);
        }
        if (GameName == "Heap Solitaire")
        {
            return new HeapSolitaireBlazor.Bootstrapper(Starts, Mode);
        }
        if (GameName == "Triangle Solitaire")
        {
            return new TriangleSolitaireBlazor.Bootstrapper(Starts, Mode);
        }
        if (GameName == "Vegas Solitaire")
        {
            return new VegasSolitaireBlazor.Bootstrapper(Starts, Mode);
        }
        throw new CustomBasicException("Game Not Found");
    }
}