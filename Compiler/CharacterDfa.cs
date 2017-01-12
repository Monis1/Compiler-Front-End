using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexical_Analyzer
{
    class CharacterDfa
    {
        public  string dfa(string s,int line_no)
        {

            int istate = 0, fstate = 4;
            try
            {
                for (int i = 0; i < s.Length; i++)
                {
                    istate = check(istate, s[i]);
                }
            }
            catch { return null; }
            if (istate == fstate)
                return "(" + s + "," + "char" + "," + line_no + ")";
            else
                return null;

        }

         int check(int state, char ch)
        {

            int[,] am = { { 5, 5, 5, 5, 1 }, { 5, 3, 3, 2, 5 }, { 3, 5, 3, 3, 3 }, { 5, 5, 5, 5, 4 }, { 5, 5, 5, 5, 5 }, { 5, 5, 5, 5, 5 } };

            if (ch == '\"')
                return am[state, 0];
            else if ((ch >= 0 && ch <= 255) && ch != '\\' && ch != '\'' && ch != '\"' && ch != 'a' && ch != 'b' && ch != 'n' && ch != 'f' && ch != 'r' && ch != 't'&&ch!='v'&&ch!='0')
                return am[state, 1];
            else if (ch == 'a' || ch == 'b' || ch == 'n' || ch == 'f' || ch == 'r' || ch == 't'||ch=='0'||ch=='v')
                return am[state, 2];
            else if (ch == '\\')
                return am[state, 3];
            else if (ch == '\'')
                return am[state, 4];
            else
                return -1;


        }
    }
}
