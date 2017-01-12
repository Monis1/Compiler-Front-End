using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexical_Analyzer
{
    class SymbolTable2
    {
        public string name;
        public string type;
        public List<SymbolTable3> classatts;

        public SymbolTable2(string n, string t)
        {
            name = n;
            type = t;
        }
    }
}
