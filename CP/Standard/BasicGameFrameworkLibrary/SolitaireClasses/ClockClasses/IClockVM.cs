namespace BasicGameFrameworkLibrary.SolitaireClasses.ClockClasses;
public interface IClockVM
{
    Task ClockClickedAsync(int index); //will be 0 based.
}