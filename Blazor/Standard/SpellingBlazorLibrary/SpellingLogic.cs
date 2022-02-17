namespace SpellingBlazorLibrary;
public class SpellingLogic : ISpellingLogic
{
    public static string BaseAddress { get; set; } = "";
    public SpellingLogic()
    {
        js.RequireCustomSerialization = true;
    }
    private BasicList<WordInfo>? _words;
    async Task<BasicList<WordInfo>> ISpellingLogic.GetWordsAsync(EnumDifficulty? difficulty, int? letters)
    {
        if (_words is null)
        {
            if (string.IsNullOrWhiteSpace(BaseAddress) == false)
            {
                using HttpClient http = new();
                http.BaseAddress = new (BaseAddress);
                string text = await http.GetStringAsync("./_content/SpellingBlazorLibrary/spelling.json");
                _words = await js.DeserializeObjectAsync<BasicList<WordInfo>>(text);
            }
            else
            {
                string path = Path.Combine(aa.GetApplicationPath(), "wwwroot", "spelling.json");
                _words = await fs.RetrieveSavedObjectAsync<BasicList<WordInfo>>(path);
            }
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