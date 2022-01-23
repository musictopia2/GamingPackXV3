namespace BasicGameFrameworkLibrary.DIContainers;
public static class Helpers
{
    public static IGamePackageResolver? Resolver { get; set; }


    //i can no longer populate the container.  this means lots of rethinking will be required this time.
    //don't have a simple resolver anymore.


    public static void PopulateContainer(IAdvancedDIContainer thisMain) //this is probably the best thing to do.
    {

        if (thisMain.MainContainer is not null)
        {
            return;
        }
        if (Resolver is null)
        {
            throw new CustomBasicException("Never populated the di container");
        }
        thisMain.MainContainer = Resolver;
        if(thisMain.MainContainer is IGamePackageGeneratorDI di)
        {
            thisMain.GeneratorContainer = di;
        }
        else
        {
            throw new CustomBasicException("Never populated the container that implemented the IGamePackageGeneratorDI");
        }
        //if (thisMain.MainContainer != null)
        //    return;
        //if (cons == null)
        //{
        //    throw new BasicBlankException("Never populated the di container in old static.  Rethink");
        //}
        //thisMain.MainContainer = (IGamePackageResolver)cons;
    }
}
