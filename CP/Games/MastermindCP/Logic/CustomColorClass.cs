using BasicGameFrameworkLibrary.ChooserClasses;
using CommonBasicLibraries.CollectionClasses;
using MastermindCP.Data;
namespace MastermindCP.Logic
{
    public class CustomColorClass : IEnumListClass<EnumColorPossibilities>
    {
        private readonly GlobalClass _global;
        public CustomColorClass(GlobalClass global)
        {
            _global = global;
        }
        BasicList<EnumColorPossibilities> IEnumListClass<EnumColorPossibilities>.GetEnumList()
        {
            return _global.ColorList;
        }
    }
}