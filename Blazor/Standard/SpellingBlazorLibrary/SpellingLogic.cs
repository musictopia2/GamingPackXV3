namespace SpellingBlazorLibrary;
public class SpellingLogic : ISpellingLogic
{
    private readonly HttpClient _http;
    public SpellingLogic(HttpClient http)
    {
        js.RequireCustomSerialization = true;
        _http = http;
    }
    private BasicList<WordInfo>? _words;
    async Task<BasicList<WordInfo>> ISpellingLogic.GetWordsAsync(EnumDifficulty? difficulty, int? letters)
    {
        if (_words is null)
        {
            string text = await _http.GetStringAsync("./_content/SpellingBlazorLibrary/spelling.json");
            _words = await js.DeserializeObjectAsync<BasicList<WordInfo>>(text);
        }
        if (difficulty.HasValue == false && letters.HasValue == false)
        {
            return _words.ToBasicList();
        }
        if (difficulty.HasValue == false)
        {
            return _words.Where(x => x.Letters == letters!.Value).ToBasicList();
        }
        return _words.Where(x => x.Letters == letters!.Value && x.Difficulty == difficulty).ToBasicList();
    }
}