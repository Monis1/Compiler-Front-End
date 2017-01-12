using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexical_Analyzer
{
    class SymbolTable3
    {
        public string name, type, AM, TM;
        public List<SymbolTable1> funclocals;
      
        public SymbolTable3(string n,string t,string am,string tm)
        {
            name = n;
            type = t;
            AM = am;
            TM = tm;
        }

       
    }
}
