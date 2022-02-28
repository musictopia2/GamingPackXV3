namespace OldMaidCP.Logic;
public interface IOtherPlayerProcess
{
    Task SelectCardAsync(int deck);
    void SortOtherCards();
}