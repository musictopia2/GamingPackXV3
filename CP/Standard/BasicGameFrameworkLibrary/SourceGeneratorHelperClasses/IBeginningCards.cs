using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicGameFrameworkLibrary.SourceGeneratorHelperClasses;
public interface IBeginningCards<D, P, S> : ICommonMultiplayer<P, S>
    where D : IDeckObject, new()
    where P : class, IPlayerItem, new()
    where S : BasicSavedCardClass<P, D>, new()
   
{
}
