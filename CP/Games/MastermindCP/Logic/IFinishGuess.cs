namespace MastermindCP.Logic;
public interface IFinishGuess
{
    Task FinishGuessAsync(int howManyCorrect, GameBoardViewModel board);
}