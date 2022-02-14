namespace BasicGameFrameworkLibrary.MultiplayerClasses.MiscHelpers;
public interface IBeginningColorModel<E>
    where E : IFastEnumColorSimple
{
    SimpleEnumPickerVM<E> ColorChooser { get; } //maybe the second generic is not needed anymore (?)
}