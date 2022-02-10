using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicGameFrameworkLibrary;

internal static class TestClass
{

    //this is a sample of what should actually happen.
    //need to be able to support generics for cloning.
    //looks like it won't work.
    //because we can't get the information about what was sent in (since its a runtime thing).
    //this means i can attempt something else.
    //which is doing as solitaire card.
    //since that is actually very common.
    //the basicgameframework knows about solitaire card.


    //public static global::BasicGameFrameworkLibrary.DrawableListsObservable.SavedDiscardPile<D> Clone<D>(this global::BasicGameFrameworkLibrary.DrawableListsObservable.SavedDiscardPile<D> source)
    //    where D: IDeckObject, new()
    //{
    //    global::BasicGameFrameworkLibrary.DrawableListsObservable.SavedDiscardPile<D> output = new()
    //    {
    //        CurrentCard = source.CurrentCard
    //    };
    //    output.PileList = new();
    //    foreach (var item in source.PileList.AsSpan())
    //    {
    //        output.PileList.Add(item);
    //    }
    //    return output;
    //}
}
