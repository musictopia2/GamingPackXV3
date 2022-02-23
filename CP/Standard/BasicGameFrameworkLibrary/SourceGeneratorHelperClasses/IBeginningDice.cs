using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicGameFrameworkLibrary.SourceGeneratorHelperClasses;
public interface IBeginningDice<P, S> : ICommonMultiplayer<P, S>
    where P : class, IPlayerItem, new()
    where S : BasicSavedGameClass<P>, new()
{
}