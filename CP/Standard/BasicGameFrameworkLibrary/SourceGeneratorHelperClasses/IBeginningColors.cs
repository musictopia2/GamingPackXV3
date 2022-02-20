using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicGameFrameworkLibrary.SourceGeneratorHelperClasses;
public interface IBeginningColors<E, P, S> : ICommonMultiplayer<P, S>
    where E: IFastEnumColorSimple
    where P : class, IPlayerBoardGame<E>, new()
    where S : BasicSavedGameClass<P>, new()
{
}
public interface ITest<T>
{

}