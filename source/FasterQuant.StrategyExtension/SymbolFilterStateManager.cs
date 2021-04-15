using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FasterQuant.StrategyExtension
{
    internal sealed class SymbolFilterStateManager
    {
        internal Dictionary<int, List<SymbolFilter>> SymbolFilters;

        private static readonly SymbolFilterStateManager instance = new SymbolFilterStateManager();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static SymbolFilterStateManager()
        {
        }

        private SymbolFilterStateManager()
        {
            SymbolFilters = new Dictionary<int, List<SymbolFilter>>();
        }

        public static SymbolFilterStateManager Instance
        {
            get
            {
                return instance;
            }
        }
    }
}

