namespace BasicGameFrameworkLibrary.CommonInterfaces;
public interface ISuitObject<E> where E : IFastEnumSimple
{
    E GetSuit { get; }
}