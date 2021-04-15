using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FasterQuant.StrategyExtension
{
    public class SymbolFilter
    {
        public long Date;
        public List<string> Symbols;

        public SymbolFilter(long date, List<string> symbols)
        {
            Date = date;
            Symbols = symbols;
        }
    }
}
