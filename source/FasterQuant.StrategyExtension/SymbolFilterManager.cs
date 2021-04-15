using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FasterQuant.StrategyExtension
{
    public static class SymbolFilterManager
    {
        public static void Initialize(int strategyId)
        {
            var sfsm = SymbolFilterStateManager.Instance;
            sfsm.SymbolFilters[strategyId] = new List<SymbolFilter>();
        }

        public static void AddSymbolFilter(int strategyId, SymbolFilter symbolFilter)
        {
            var sfsm = SymbolFilterStateManager.Instance;
            sfsm.SymbolFilters[strategyId].Add(symbolFilter);
        }

        public static List<SymbolFilter> GetSymbolFilters(int strategyId)
        {
            var sfsm = SymbolFilterStateManager.Instance;
            return sfsm.SymbolFilters[strategyId];
        }

    }
}
