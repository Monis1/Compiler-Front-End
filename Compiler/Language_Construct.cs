using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexical_Analyzer
{
    abstract class Language_Construct
    {
      
       public string[] vpscps;
        
        public Language_Construct()
       {
           
           vpscps  = null;
       }

        abstract public void Load();

        abstract public string check(string word, int line_no);
    

        
    }
}
