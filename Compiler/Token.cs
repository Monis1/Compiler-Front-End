using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexical_Analyzer
{
    class Token
    {
      public  string value_part;
      public  string class_part;
      public int line_no;

        public void display_token()
        { Console.WriteLine("("+value_part+","+class_part+")"); }

    }
}
