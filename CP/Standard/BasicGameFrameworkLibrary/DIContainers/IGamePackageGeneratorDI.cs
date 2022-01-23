namespace BasicGameFrameworkLibrary.DIContainers;
public interface IGamePackageGeneratorDI
{
    object LaterGetObject(Type type, bool needsReplacement = false);
    void LaterRegister(Type type, BasicList<Type> assignedFrom, Func<object> action, string tag = "");
}