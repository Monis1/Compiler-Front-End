using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lexical_Analyzer
{
    class Operators:Language_Construct
    {
        public Token[] lexemes;
        public Operators()
        {
            
            vpscps = File.ReadAllLines(@"..\operators.txt");
            lexemes = new Token[vpscps.Length];
        
        }

        public override void Load()
        {


            for (int i = 0; i < vpscps.Length; i++)
            {
                string[] vpcp = vpscps[i].Split(' ');
                lexemes[i] = new Token();
                lexemes[i].value_part = vpcp[0];
                lexemes[i].class_part = vpcp[1];
            }

        }

        public override string check(string word, int line_no)
        {

            for (int i = 0; i < lexemes.Length; i++)
            {
                if (word == lexemes[i].value_part)
                { return "(" + lexemes[i].value_part + "," + lexemes[i].class_part + "," + line_no + ")"; }
            }
            return null;
        }
    

    }
}
