using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexical_Analyzer
{
    class SymbolTable1
    {
       public string name, type;
       public Stack<int> stack;

        public SymbolTable1(string name,string type,Stack<int> stack)
        {
            this.name = name;
            this.type = type;
            this.stack= stack;
        }
    }
}
