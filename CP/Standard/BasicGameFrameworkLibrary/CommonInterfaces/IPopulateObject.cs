namespace BasicGameFrameworkLibrary.CommonInterfaces;
public interface IPopulateObject<T> where T : IConvertible //could be iffy
{
    void Populate(T chosen);
}