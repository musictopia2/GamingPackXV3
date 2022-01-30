namespace BasicGameFrameworkLibrary.CommonInterfaces;
public interface IColorObject<E> where E : IFastEnumColorSimple
{
    E GetColor { get; }
}