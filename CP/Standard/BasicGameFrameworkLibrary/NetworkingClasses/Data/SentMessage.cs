namespace BasicGameFrameworkLibrary.NetworkingClasses.Data;
public class SentMessage
{
    public string Status { get; set; } = "";
    public string Body { get; set; } = "";
    public override string ToString()
    {
        return js.SerializeObject(this); //hopefully this simple (?)
    }
    //public override string ToString()
    //{
    //    return JsonConvert.SerializeObject(this, Formatting.Indented); //i do like indented.   has to be this way because can't use async in this situation
    //}
}