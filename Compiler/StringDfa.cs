using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexical_Analyzer
{
    class StringDfa
    {
        public string dfa(string s,int line_no)
        {

            int istate = 0, fstate = 2;
            try
            {
                for (int i = 0; i < s.Length; i++)
                {
                    istate = check(istate, s[i]);
                }
            }
            catch { return null; }
            if (istate == fstate)
                return "(" + s + "," + "word" + "," + line_no + ")";
            else
                return null;

        }
       

        public static int check(int state, char ch)
        {

            int[,] am = { { 4, 4, 4, 4, 1 }, { 4, 1, 1, 3, 2 }, { 4, 4, 4, 4, 4 }, { 1, 4, 1, 1, 1 }, { 4, 4, 4, 4, 4 } };

            if (ch == '\'')
                return am[state, 0];
            else if ((ch >= 0 && ch <= 255) && ch != '\\' && ch != '\'' && ch != '\"' && ch != 'a' && ch != 'b' && ch != 'n' && ch != 'f' && ch != 'r' && ch != 't'&&ch!='v'&&ch!='0')
                return am[state, 1];
            else if (ch == 'a' || ch == 'b' || ch == 'n' || ch == 'f' || ch == 'r' || ch == 't'||ch=='0'||ch=='v')
                return am[state, 2];
            else if (ch == '\\')
                return am[state, 3];
            else if (ch == '\"')
                return am[state, 4];
            else
                return -1;


        }

    }
}
