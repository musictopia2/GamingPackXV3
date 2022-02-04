namespace BasicGameFrameworkLibrary.SolitaireClasses.TriangleClasses;
public abstract class TriangleObservable
{
    public BasicList<SolitaireCard> CardList = new();
    private readonly ITriangleVM _thisMod;
    private bool _inPlay = false;
    private float _totalWidth;
    private float _cardWidth;
    private float _cardHeight;
    public PlainCommand? CardCommand { get; set; }
    private bool CanClickCard(SolitaireCard card)
    {
        if (card.IsEnabled == false || card.Visible == false)
        {
            return false;
        }
        return _inPlay;
    }
    private async Task PrivateCardClickAsync(SolitaireCard card)
    {
        await _thisMod.CardClickedAsync(card);
    }
    public int TotalColumns { get; } //hopefully this works.
    public TriangleObservable(ITriangleVM thisMod, CommandContainer command, IGamePackageResolver resolver, int maxColumnsRows)
    {
        _thisMod = thisMod;
        TotalColumns = maxColumnsRows;
        //MethodInfo method = this.GetPrivateMethod(nameof(PrivateCardClickAsync));
        //MethodInfo fun = this.GetPrivateMethod(nameof(CanClickCard));
        //CardCommand = new PlainCommand(this, method, fun, null!, command);
        LoadBoard();
    }
    private void LoadBoard()
    {
        CardList = new();
        SolitaireCard tempCard = new();
        _cardWidth = tempCard.DefaultSize.Width;
        _cardHeight = tempCard.DefaultSize.Height;
        _totalWidth = TotalColumns * _cardWidth;
        int nums = HowManyCards;
        nums.Times(x =>
        {
            tempCard = new();
            tempCard.IsEnabled = false;
            CardList.Add(tempCard);
        });
        PositionCards();
    }
    protected void ClearBoard()
    {
        PositionCards();
        CardList.ForEach(thisCard =>
        {
            thisCard.Visible = true;
            thisCard.IsEnabled = false;
        });
        _inPlay = true;
    }
    protected void RecalculateEnables()
    {
        CardList.ForEach(thisCard => thisCard.IsEnabled = false); //must be proven true.
        int firsts = CardList.Count - TotalColumns + 1;
        int x;
        for (x = firsts; x <= CardList.Count; x++)
        {
            var thisCard = CardList[x - 1];
            thisCard.IsEnabled = thisCard.Visible;
        }
        int firstNum = firsts;
        int y;
        int secondNumber;
        int finalNumber;
        SolitaireCard firstCard;
        SolitaireCard secondCard;
        SolitaireCard finalCard;
        for (x = TotalColumns - 1; x >= 1; x += -1)
        {
            var loopTo1 = firstNum + x - 1;
            for (y = firstNum; y <= loopTo1; y++)
            {
                secondNumber = y + 1;
                finalNumber = y - x;
                firstCard = CardList[y - 1]; // because its 0 based
                secondCard = CardList[secondNumber - 1];
                finalCard = CardList[finalNumber - 1];
                if (firstCard.Visible == false & secondCard.Visible == false & finalCard.Visible == true)
                {
                    finalCard.IsEnabled = true;
                }
            }
            firstNum -= x;
        }
    }
    private int HowManyCards
    {
        get
        {
            int y = 0;
            TotalColumns.Times(x => y += x);
            return y;
        }
    }
    private void PositionCards()
    {
        float firstTop = 0;
        float firstLeft = (_totalWidth - _cardWidth) / 2;
        float divHeight = _cardHeight / 2;
        float divWidth = _cardWidth / 2;
        int y = 0;
        float newLeft;
        float newTop;
        SolitaireCard thisCard;
        int oldy = 0;
        int q = 0;
        TotalColumns.Times(x =>
        {
            y += x;
            oldy = y - oldy;
            newLeft = firstLeft;
            newTop = firstTop;
            oldy.Times(z =>
            {
                q++;
                thisCard = CardList[q - 1];
                thisCard.Location = new(newLeft, newTop);
                newLeft += _cardWidth;
            });
            firstTop += divHeight;
            firstLeft -= divWidth;
            oldy = y;
        });
    }
    public SavedTriangle GetSavedTriangles()
    {
        SavedTriangle output = new();
        output.InPlay = _inPlay;
        output.CardList = CardList.ToBasicList();
        return output;
    }
    public virtual void LoadSavedTriangles(SavedTriangle thisT)
    {
        _inPlay = thisT.InPlay;
        CardList.ReplaceRange(thisT.CardList);
    }
}