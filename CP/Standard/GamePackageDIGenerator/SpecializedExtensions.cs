namespace GamePackageDIGenerator;
internal static class SpecializedExtensions
{
    public static void PopulateReplaceBoardGameColorClasses(this ICodeBlock w)
    {
        if (_player is null || _saved is null || _color is null)
        {
            return;
        }
        w.WriteLine("private static BasicList<Type> ReplaceBoardGameColorClasses()")
        .WriteCodeBlock(w =>
        {
            w.WriteLine("BasicList<Type> output = new()")
            .WriteCodeBlock(w =>
            {
                w.WriteLine(w =>
                {
                    w.Write("typeof(")
                    .PopulateBeginningColorClass(_color, _player, _saved)
                    .Write("),");
                })
                .WriteLine(w =>
                {
                    w.Write("typeof(")
                    .PopulateBeginningColorModel(_color, _player)
                    .Write("),");
                })
                .WriteLine(w =>
                {
                    w.Write("typeof(")
                    .PopulateBeginningChooseColorViewModel(_color, _player)
                    .Write(")");
                });
            }, true)
            .WriteLine("return output;");
        });
    }

    //private static bool _canreplaceBoardProcess = false;
    private static INamedTypeSymbol? _player;
    private static INamedTypeSymbol? _saved;
    private static INamedTypeSymbol? _color;
    

    private static void Reset()
    {
        _player = null;
        _saved = null;
        _color = null;
    }

    public static void PopulateRegisterSpecializedMethod(this ICodeBlock w, INamedTypeSymbol symbol, Compilation compilation)
    {
        Reset();
        FinishDIRegistrationsExtensions.StartMethod();
        //you may have or not have the colors.
        INamedTypeSymbol player = CapturePlayerSymbol(symbol);
        INamedTypeSymbol saved = CaptureSaveSymbol(symbol);
        w.PopulateCommonMethod(player, saved, compilation);
        if (symbol.Name == "IBeginningColors")
        {
            //do color processes.
            INamedTypeSymbol color = CaptureColorSymbol(symbol);
            w.PopulateColorsMethod(color, player, saved, compilation);
        }
        if (symbol.Name == "IBeginningDice")
        {
            INamedTypeSymbol dice = CaptureDiceSymbol(symbol);
            w.PopulateDiceMethod(dice, player, compilation);
        }


    }
    
    private static void PopulateCommonMethod(this ICodeBlock w, INamedTypeSymbol player, INamedTypeSymbol saved, Compilation compilation)
    {
        BasicList<FirstInformation> list = GetCommonList(player, saved, compilation);
        w.WriteLine(w =>
        {
            w.Write("container.RegisterType<").GlobalWrite()
            .Write("BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses.BasicGameLoader<")
            .Write(player.Name)
            .Write(", ")
            .Write(saved.Name)
            .Write(">>();");
        })
        .WriteLine(w =>
        {
            w.Write("container.RegisterType<").GlobalWrite()
            .Write("BasicGameFrameworkLibrary.MiscProcesses.RetrieveSavedPlayers<")
            .Write(player.Name)
            .Write(", ")
            .Write(saved.Name)
            .Write(">>();");
        })
        .WriteLine(w =>
        {
            w.Write("container.RegisterType<").GlobalWrite()
            .Write("BasicGameFrameworkLibrary.ViewModels.MultiplayerOpeningViewModel<")
            .Write(player.Name)
            .Write(">>();");
        });
        w.ProcessFinishDIRegistrations(list);
    }
    private static IWriter PopulateBeginningColorClass(this IWriter w, INamedTypeSymbol color, INamedTypeSymbol player, INamedTypeSymbol saved)
    {
        w.GlobalWrite()
        .Write("BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses.BeginningColorProcessorClass<")
            .Write(color.Name)
            .Write(", ")
            .Write(player.Name)
            .Write(", ")
            .Write(saved.Name)
            .Write(">");
        return w;
    }
    private static IWriter PopulateBeginningChooseColorViewModel(this IWriter w, INamedTypeSymbol color, INamedTypeSymbol player)
    {
        w.GlobalWrite()
        .Write("BasicGameFrameworkLibrary.ViewModels.BeginningChooseColorViewModel<")
           .Write(color.Name)
           .Write(", ")
           .Write(player.Name)
           .Write(">");
        return w;
    }
    private static IWriter PopulateBeginningColorModel(this IWriter w, INamedTypeSymbol color, INamedTypeSymbol player)
    {
        w.GlobalWrite()
           .Write("BasicGameFrameworkLibrary.MultiplayerClasses.MiscHelpers.BeginningColorModel<")
           .Write(color.Name)
           .Write(", ")
           .Write(player.Name)
           .Write(">");
        return w;
    }
    public static void PopulateColorsMethod(this ICodeBlock w, INamedTypeSymbol color, INamedTypeSymbol player, INamedTypeSymbol saved, Compilation compilation)
    {
        _color = color;
        _player = player;
        _saved = saved;
        //i think this needs to figure out how to later register them as well (?)
        BasicList<FirstInformation> list= GetColorList(color, player, saved, compilation);
        w.WriteLine(w =>
        {
            w.Write("container.RegisterType<")
            .PopulateBeginningColorClass(color, player, saved)
            .Write(">();");
        })
       .WriteLine(w =>
       {
           w.Write("container.RegisterType<")
           .PopulateBeginningChooseColorViewModel(color, player)
           .Write(">();");
       })
       .WriteLine(w =>
       {
           w.Write("container.RegisterType<")
           .PopulateBeginningColorModel(color, player)
           .Write(">();");
       });
        w.ProcessFinishDIRegistrations(list);
        w.WriteLine("global::BasicGameFrameworkLibrary.MultiplayerClasses.MiscHelpers.MiscDelegates.GetAutoGeneratedObjectsToReplace = ReplaceBoardGameColorClasses;");
    }
    private static BasicList<FirstInformation> GetColorList(INamedTypeSymbol color, INamedTypeSymbol player, INamedTypeSymbol saved, Compilation compilation)
    {
        BasicList<string> temps = new()
        {
            "BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses.BeginningColorProcessorClass`3",
            "BasicGameFrameworkLibrary.ViewModels.BeginningChooseColorViewModel`2",
            "BasicGameFrameworkLibrary.MultiplayerClasses.MiscHelpers.BeginningColorModel`2"
        };
        Dictionary<string, INamedTypeSymbol> matches = new();
        matches.Add("P", player);
        matches.Add("S", saved);
        matches.Add("E", color);
        BasicList<FirstInformation> output = GetFirstInformation(temps, matches, compilation);
        return output;
    }
    private static BasicList<FirstInformation> GetDiceList(Compilation compilation, INamedTypeSymbol dice, INamedTypeSymbol player)
    {
        BasicList<string> temps = new()
        {
            "BasicGameFrameworkLibrary.Dice.StandardRollProcesses`2"
        };
        INamedTypeSymbol? intSymbol = compilation.GetTypeByMetadataName("System.Int32");
        if (intSymbol is null)
        {
            throw new Exception("Integer was not found");
        }
        Dictionary<string, INamedTypeSymbol> matches = new();
        matches.Add("D", dice);
        matches.Add("P", player);
        matches.Add("Con", intSymbol);
        BasicList<FirstInformation> output = GetFirstInformation(temps, matches, compilation);
        return output;
    }
    private static BasicList<FirstInformation> GetDiceList(Compilation compilation, INamedTypeSymbol player)
    {
        BasicList<string> temps = new()
        {
            "BasicGameFrameworkLibrary.Dice.StandardRollProcesses`2",
            "BasicGameFrameworkLibrary.Dice.SimpleDice"
        };
        //simpledice this time.
        INamedTypeSymbol? diceSymbol = compilation.GetTypeByMetadataName("BasicGameFrameworkLibrary.Dice.SimpleDice");
        if (diceSymbol is null)
        {
            throw new Exception("There was no simple dice found");
        }
        INamedTypeSymbol? intSymbol = compilation.GetTypeByMetadataName("System.Int32");
        if (intSymbol is null)
        {
            throw new Exception("Integer was not found");
        }
        Dictionary<string, INamedTypeSymbol> matches = new();
        matches.Add("D", diceSymbol);
        matches.Add("P", player);
        matches.Add("Con", intSymbol); //this is used to match up so the generic names would correspond to the real symbols instead.
        BasicList<FirstInformation> output = GetFirstInformation(temps, matches, compilation);
        return output;
    }
    private static BasicList<FirstInformation> GetFirstInformation(BasicList<string> symbols, Dictionary<string, INamedTypeSymbol> matches, Compilation compilation)
    {
        BasicList<FirstInformation> output = new();
        foreach (var firsts in symbols)
        {
            INamedTypeSymbol? symbol = compilation.GetTypeByMetadataName(firsts);
            if (symbol is null)
            {
                throw new Exception($"Unable to get symbol for type {symbol}");
            }
            FirstInformation item = new();
            //not sure if i need a delegate or not (?)
            item.MainClass = symbol;
            item.GenericSymbols = matches;
            var temps = item.MainClass!.AllInterfaces.ToBasicList();
            foreach (var temp in temps)
            {
                if (temp.Name != "IHandle" && temp.Name != "IHandleAsync" && temp.Name != "IEquatable" && temp.Name != "IComparable" && temp.Name != "IDisposable")
                {
                    item.Assignments.Add(temp);
                } //cannot do anything with ihandle or ihandleasync since event aggravation handles that anyways.
            }
            if (item.MainClass!.Constructors.Count() > 0)
            {
                var tests = item.MainClass!.Constructors.OrderByDescending(x => x.Parameters.Count()).FirstOrDefault();
                var nexts = item.MainClass!.Constructors.OrderByDescending(x => x.Parameters.Count()).FirstOrDefault().Parameters.ToBasicList();
                foreach (var a in nexts)
                {
                    var lasts = a.Type;
                    item.Constructors.Add((INamedTypeSymbol)lasts);
                }
            }
            
            output.Add(item);
        }
        return output;
    }
    private static BasicList<FirstInformation> GetCommonList(INamedTypeSymbol player, INamedTypeSymbol saved, Compilation compilation)
    {
        BasicList<string> temps = new()
        {
            "BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses.BasicGameLoader`2",
            "BasicGameFrameworkLibrary.ViewModels.MultiplayerOpeningViewModel`1",
            "BasicGameFrameworkLibrary.MiscProcesses.RetrieveSavedPlayers`2"
        };
        Dictionary<string, INamedTypeSymbol> matches = new();
        matches.Add("P", player);
        matches.Add("S", saved);
        
        BasicList<FirstInformation> output = GetFirstInformation(temps, matches, compilation);
        return output;
    }

    private static void PopulateDiceMethod(this ICodeBlock w, INamedTypeSymbol dice, INamedTypeSymbol player, Compilation compilation)
    {
        var list = GetDiceList(compilation, dice, player);
        //first register the stuff needed.
        w.WriteLine(w =>
        {
            w.Write("container.RegisterType<").GlobalWrite()
            .Write("BasicGameFrameworkLibrary.Dice.StandardRollProcesses<")
            .Write(dice.Name)
            .Write(", ")
            .Write(player.Name)
            .Write(">>();");
        })
        .WriteLine(w =>
        {
            w.Write("container.RegisterSingleton<").GlobalWrite()
            .Write("BasicGameFrameworkLibrary.Dice.IGenerateDice<int>, ")
            .Write(dice.Name)
            .Write(">();");
        });
        w.ProcessFinishDIRegistrations(list);
    }
    public static void PopulateStandardDiceMethod(this ICodeBlock w, Compilation compilation, INamedTypeSymbol symbol)
    {
        FinishDIRegistrationsExtensions.StartMethod();
        var player = CapturePlayerSymbol(symbol);
        w.WriteLine(w =>
        {
            w.Write("container.RegisterType<global::BasicGameFrameworkLibrary.Dice.StandardRollProcesses<global::BasicGameFrameworkLibrary.Dice.SimpleDice, ")
            .Write(player.Name)
            .Write(">>();");
        })
        .WriteLine("container.RegisterSingleton<global::BasicGameFrameworkLibrary.Dice.IGenerateDice<int>, global::BasicGameFrameworkLibrary.Dice.SimpleDice>();");
        var list = GetDiceList(compilation, player);
        w.ProcessFinishDIRegistrations(list);
    }
    private static INamedTypeSymbol CapturePlayerSymbol(INamedTypeSymbol symbol)
    {
        foreach (var item in symbol.TypeArguments)
        {
            if (item.Implements("IPlayerItem"))
            {
                return (INamedTypeSymbol) item;
            }
        }
        throw new Exception("No player found");
    }
    private static INamedTypeSymbol CaptureSaveSymbol(INamedTypeSymbol symbol)
    {
        foreach (var item in symbol.TypeArguments)
        {
            var temps = (INamedTypeSymbol)item;
            if (temps.InheritsFrom("BasicSavedGameClass"))
            {
                return temps;
            }
        }
        throw new Exception("No basic saved game class inherited from found");
    }
    private static INamedTypeSymbol CaptureColorSymbol(INamedTypeSymbol symbol)
    {
        foreach (var item in symbol.TypeArguments)
        {
            //since one source generator cannot access information from another source generator, then means that I can first attempt to do iequatable.
            //if that works, great.  if that does not work, then rethinking will be required (could be required to make this implement another interface just to make this work).  not sure yet.
            if (item.Implements("IEquatable"))
            {
                return (INamedTypeSymbol)item;
            }
        }
        //return null;
        throw new Exception("No IEquatable Found Which Represents Color");
    }
    private static INamedTypeSymbol CaptureDiceSymbol(INamedTypeSymbol symbol)
    {
        foreach (var item in symbol.TypeArguments)
        {
            if (item.Implements("IStandardDice"))
            {
                return (INamedTypeSymbol)item;
            }
        }
        throw new Exception("No dice found");
    }
}