namespace RookCP.Logic;
public interface INestProcesses
{
    Task ProcessNestAsync(DeckRegularDict<RookCardInformation> list);
}