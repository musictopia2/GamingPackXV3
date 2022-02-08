using System;
using System.Collections.Generic;
using System.Text;

namespace GamePackageSaveInfoGenerator;

internal class CompleteInformation
{
    public ResultsModel Result { get; set; } = new();
    //has to figure out automatically what to ignore.
    public BasicList<IPropertySymbol> PropertiesToIgnore { get; set; } = new();
}
