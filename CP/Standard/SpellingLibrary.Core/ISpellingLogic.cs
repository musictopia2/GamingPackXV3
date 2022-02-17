namespace SpellingLibrary.Core;
public interface ISpellingLogic
{
    Task<BasicList<WordInfo>> GetWordsAsync(EnumDifficulty? difficulty, int? letters);
}