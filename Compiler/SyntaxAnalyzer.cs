using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lexical_Analyzer
{
    class SyntaxAnalyzer
    {
        Token[] tokens;
        int i, S = -1,index=-1;
        List<SymbolTable1> st1 = new List<SymbolTable1>();
        List<SymbolTable2> classes = new List<SymbolTable2>();
        Stack<int> stack = new Stack<int>();
        bool Infunc, Inclass;
        List<SymbolTable3> atts;
        List<SymbolTable1> InFunction;
        StreamWriter WriteIC = new StreamWriter(@"..\IC.txt");
        public SyntaxAnalyzer(Token[] tokens)
        {
            i = 0;
            this.tokens = tokens;
            if (Start() && tokens[i].class_part == "$")
                Console.WriteLine("Successfully Parsed...");
            else
                Console.WriteLine("Syntax Error at " + tokens[i].line_no);
            
            WriteIC.Close();
        }


        // ICG Functions

        void generate(string s)
        {
            WriteIC.WriteLine(s);
        }

        string Createtemp()
        {
            index++;
            return "t" + index;
           
        }

        string Createlabel()
        {
            index++;
            return "L" + index;
           
        }


        //Semantic Functions------V
        void createScope()
        {
            S++;
            stack.Push(S);
        }

        void destroyScope()
        {
            if (stack.Count != 0)
            {
                stack.Pop();
                if (stack.Count != 0)
                    S = stack.Peek();
            }
            else
                S--;
        }

        string compatibility(string type, string opr)
        {
            if (opr == "++" || opr == "--")
            {
                if (type == "integer")
                    return "integer";
                else if (type == "char")
                    return "char";
            }
            else if (opr == "!")
            {
                if (type == "bool")
                    return "bool";
            }
            return null;
        }

        string compatibility(string T1, string T2, string op)
        {
            if (T1 == "integer" && T2 == "integer" || T2 == "integer" && T1 == "integer")
            {
                if (op == "+")
                    return T1;

                else if (op == "-")
                    return T1;

                else if (op == "/")
                    return T1;
                else if (op == "*")
                    return T1;
                else if (op == "%")
                    return T1;
                else if (op == "&")
                    return T1;
                else if (op == "|")
                    return T1;
                else if (op == "~")
                    return T1;
                else if (op == ">>" || op == "<<")
                    return T1;
                else if (op == "==")
                    return "bool";
                else if (op == "!=")
                    return "bool";
                else if (op == ">=")
                    return "bool";
                else if (op == "<=")
                    return "bool";
                else if (op == "<")
                    return "bool";
                else if (op == ">")
                    return "bool";
                else if (op == "=")
                    return "integer";
                else if (op == "+=")
                    return "integer";
                else if (op == "/=")
                    return "integer";
                else if (op == "*=")
                    return "integer";
                else if (op == "%=")
                    return "integer";


            }


            else if (T1 == "char" && T2 == "char" || T2 == "char" && T1 == "char")
            {
                if (op == "+")
                    return T1;

                else if (op == "-")
                    return T1;

                else if (op == "/")
                    return T1;
                else if (op == "*")
                    return T1;
                else if (op == "%")
                    return T1;
                else if (op == "&")
                    return T1;
                else if (op == "|")
                    return T1;
                else if (op == "~")
                    return T1;
                else if (op == ">>" || op == "<<")
                    return T1;
                else if (op == "==")
                    return "bool";
                else if (op == "!=")
                    return "bool";
                else if (op == ">=")
                    return "bool";
                else if (op == "<=")
                    return "bool";
                else if (op == "<")
                    return "bool";
                else if (op == ">")
                    return "bool";
                else if (op == "=")
                    return "char";
                else if (op == "+=")
                    return "char";
                else if (op == "/=")
                    return "char";
                else if (op == "*=")
                    return "char";
                else if (op == "%=")
                    return "char";
            }


            else if (T1 == "integer" && T2 == "decimal" || T2 == "integer" && T1 == "decimal")
            {
                if (op == "+")
                    return "decimal";

                else if (op == "-")
                    return "decimal";

                else if (op == "*")
                    return "decimal";

                else if (op == "/")
                    return "decimal";
                else if (op == "=")
                    return T1;
                else if (op == "+=")
                    return "decimal";
                else if (op == "/=")
                    return "decimal";
                else if (op == "*=")
                    return "decimal";

            }

            else if (T1 == "word" || T2 == "word")
            {
                if (op == "+")
                    return "word";
                else if (op == "==")
                    return "bool";
                else if (op == "!=")
                    return "bool";
                else if (op == ">=")
                    return "bool";
                else if (op == "<=")
                    return "bool";
                else if (op == "<")
                    return "bool";
                else if (op == ">")
                    return "bool";
                else if (op == "=")
                    return "word";
                else if (op == "+=")
                    return "word";

            }

            else if (T1 == "decimal" && T2 == "decimal" || T2 == "decimal" && T1 == "decimal")
            {
                if (op == "+")
                    return "decimal";

                else if (op == "-")
                    return "decimal";

                else if (op == "*")
                    return "decimal";
                else if (op == "&")
                    return "decimal";
                else if (op == "|")
                    return "decimal";
                else if (op == "~")
                    return "decimal";
                else if (op == "/")
                    return "decimal";
                else if (op == "==")
                    return "bool";
                else if (op == "!=")
                    return "bool";
                else if (op == ">=")
                    return "bool";
                else if (op == "<=")
                    return "bool";
                else if (op == "<")
                    return "bool";
                else if (op == ">")
                    return "bool";
                else if (op == "=")
                    return "decimal";
                else if (op == "+=")
                    return "decimal";
                else if (op == "/=")
                    return "decimal";
                else if (op == "*=")
                    return "decimal";

            }


            else if (T1 == "char" && T2 == "integer" || T2 == "integer" && T1 == "char")
            {
                if (op == "+")
                    return "integer";

                else if (op == "-")
                    return "integer";

                else if (op == "*")
                    return "integer";

                else if (op == "/")
                    return "integer";
                else if (op == "%")
                    return "integer";
                else if (op == "=")
                    return "integer";
                else if (op == "+=")
                    return "integer";
                else if (op == "/=")
                    return "integer";
                else if (op == "*=")
                    return "integer";
                else if (op == "%=")
                    return "integer";
            }

            else if (T1 == "bool" && T2 == "bool" || T2 == "bool" && T1 == "bool")
            {
                if (op == "==")
                    return "bool";
                else if (op == "!=")
                    return "bool";
                else if (op == "&&")
                    return "bool";
                else if (op == "||")
                    return "bool";
                else if (op == "=")
                    return "bool";

            }

            return null;


        }

        string clfieldlookup(string T, string N)
        {
            List<SymbolTable3> C_C_atts = new List<SymbolTable3>();
            int k = 0;
            for (k = 0; k < classes.Count; k++)
            {
                if (T == classes[k].name)
                    break;
            }
            if (k == classes.Count)
                k--;
            C_C_atts = classes[k].classatts;

            for (int a = 0; a < C_C_atts.Count; a++)
            {
                if (C_C_atts[a].name == N && (C_C_atts[a].AM == "public" || C_C_atts[a].AM == "") && (C_C_atts[a].TM == "nonstatic" | C_C_atts[a].TM == ""))
                {
                    return C_C_atts[a].type;
                }
            }
            return null;
        }

        string frflookup(List<SymbolTable3> att, string N)
        {
            for (int j = 0; j < att.Count; j++)
            {
                if (N == att[j].name && att[j].type.Contains("->") == false)
                    return att[j].type;
            }

            return null;
        }

        string Icforfunclookup(List<SymbolTable3> att, string N, string TE)
        {
            for (int k = 0; k < att.Count; k++)
            {
                if (N == att[k].name&&(att[k].TM=="nonstatic"||att[k].TM==""))
                {
                    string PL = att[k].type;
                    string[] sep = new string[1];
                    sep[0] = "->";
                    string[] plrt = PL.Split(sep, StringSplitOptions.None);
                    if (TE == plrt[0])
                        return plrt[1];
                }
            }
            return null;
        }

        string clmethodlookup(string N, string T, string TE)
        {

            string toreturn = null;
            List<SymbolTable3> C_C_atts = new List<SymbolTable3>();
            int k = 0;
            for (k = 0; k < classes.Count; k++)
            {
                if (T == classes[k].name)
                    break;
            }
            if (k == classes.Count)
                k--;

            if(k>=0)
            C_C_atts = classes[k].classatts;

            for (int a = 0; a < C_C_atts.Count; a++)
            {
                if (C_C_atts[a].name == N && (C_C_atts[a].AM == "public" || C_C_atts[a].AM == "") && (C_C_atts[a].TM == "nonstatic" | C_C_atts[a].TM == ""))
                {
                    string PL = C_C_atts[a].type;
                    string[] sep = new string[1];
                    sep[0] = "->";
                    string[] plrt = PL.Split(sep, StringSplitOptions.None);
                    if (TE == plrt[0])
                        toreturn = plrt[1];
                }
            }
            if (TE == "" && toreturn == null)
                return "void";
            return toreturn;
        }

        string Iclookup(List<SymbolTable3> att, string N)
        {
            for (int j = 0; j < att.Count; j++)
            {
                if (N == att[j].name)
                    return att[j].type;

            }
            return null;
        }

        string Ifnflookup(List<SymbolTable3> att, string N)
        {
            for (int j = 0; j < att.Count; j++)
            {
                if (N == att[j].name && att[j].type.Contains("->") == false)
                    return att[j].type;

            }
            return null;
        }

        string Iclookup(List<SymbolTable3> att, string N, string TE)
        {
            for (int i = 0; i < att.Count; i++)
            {
                if (N == att[i].name)
                {
                    string PL = att[i].type;
                    string[] sep = new string[1];
                    sep[0] = "->";
                    string[] plrt = PL.Split(sep, StringSplitOptions.None);
                    if (TE == plrt[0])
                        return plrt[1];
                }
            }
            return null;
        }


        string IcIflookup(List<SymbolTable1> att, string N, int s)
        {
            for (int k = s; k >= 0; k--)
            {


                for (int i = 0; i < att.Count; i++)
                {
                    if (N == att[i].name && att[i].stack.Contains(k))
                    {
                        return att[i].type;
                    }
                }
            }
            return null;
        }



        void Insert(string name, string type)
        {

            SymbolTable1 sti = new SymbolTable1(name, type, null);
            st1.Add(sti);
        }

        void Insert(string name, string type, Stack<int> stack)
        {
            Stack<int> s1 = new Stack<int>(stack);
            SymbolTable1 sti = new SymbolTable1(name, type, s1);
            st1.Add(sti);
        }


        string dlookup(string name, int scope)
        {
            for (int k = scope; k >= 0; k--)
            {


                for (int i = 0; i < st1.Count; i++)
                {
                    if (name == st1[i].name && st1[i].stack.Contains(k))
                    {
                        return st1[i].type;
                    }
                }
            }
            return null;
        }

        string clookup(string N)
        {
            for (int i = 0; i < classes.Count; i++)
            {
                if (N == classes[i].name)
                    return classes[i].type;
            }
            return null;
        }

        void Insertclass(string N)
        {
            SymbolTable2 t = new SymbolTable2(N, "class");
            classes.Add(t);
        }

        bool dlookup(string name, string AL)
        {
            for (int i = 0; i < st1.Count; i++)
            {
                if (name == st1[i].name && st1[i].type == AL)
                {
                    return true;
                }
            }
            return false;
        }

        string dlookup2(string name, string AL)
        {
            for (int i = 0; i < st1.Count; i++)
            {
                if (name == st1[i].name)
                {
                    string PL = st1[i].type;
                    string[] sep = new string[1];
                    sep[0] = "->";
                    string[] plrt = PL.Split(sep, StringSplitOptions.None);
                    if (AL == plrt[0])
                        return plrt[1];
                }
            }
            return null;
        }
        /// <summary>
        /// Sematinc end here
        /// </summary>
        /// <returns></returns>
        /// 


        //Syntax Startss Here
        bool Start()
        {
            createScope();
            if (tokens[i].class_part == "Main" || tokens[i].class_part == "ID")
            {


                if (cls_func())
                {
                    if (tokens[i].class_part == "Main")
                    {
                        generate("Main proc");
                        i++;

                        if (tokens[i].class_part == "(")
                        {
                            i++;

                            if (tokens[i].class_part == ")")
                            {
                                i++;

                                if (Body())
                                {
                                    generate("Main endp");
                                    return true;
                                }
                            }

                        }
                    }
                }



            }
            return false;
        }

        bool cls_func()
        {
            if (tokens[i].class_part == "ID")
            {
                if (class_st())
                {
                    if (cls_func())
                        return true;
                }
            }

            else if (tokens[i].class_part == "Main")
                return true;
            return false;
        }


        bool class_st()
        {
            if (tokens[i].class_part == "ID")
            {
                string N = tokens[i].value_part;
                i++;
                if (clookup(N) == null)                                                     //------------------------------------//
                    Insertclass(N);                                                         //        Semantic                    //
                else                                                                        //                    work             //  
                    Console.WriteLine("Redeclaration of class at " + tokens[i].line_no);      //-------------------------------------//
                if (tokens[i].class_part == "class")
                {
                    i++;
                    if (tokens[i].class_part == "{")
                    {
                        Inclass = true;
                        atts = new List<SymbolTable3>();
                        classes[classes.Count - 1].classatts = atts;
                        i++;
                        if (Bodyc(ref atts))
                        {
                            if (tokens[i].class_part == "}")
                            {
                                Inclass = false;

                                i++;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }


        bool Bodyc(ref List<SymbolTable3> att)
        {
            if (tokens[i].class_part == "Access_Modifier" || tokens[i].class_part == "static" || tokens[i].class_part == "nonstatic" || tokens[i].class_part == "DataType")
            {
                string A = "";
                if (AM(ref A))
                {
                    if (Bodyc1(ref att, A))
                        return true;
                }
            }
            else if (tokens[i].class_part == "enum")
            {
                if (enum_st(ref att))
                {
                    if (Bodyc(ref att))
                        return true;
                }
            }
            else if (tokens[i].class_part == "}")
                return true;
            return false;
        }


        bool Bodyc1(ref List<SymbolTable3> att, string A)
        {
            if (tokens[i].class_part == "static")
            {
                string S = "static";
                i++;
                if (Bodyc2(ref att, A, S))
                    return true;
            }
            else if (tokens[i].class_part == "nonstatic")
            {
                string S = "nonstatic";

                i++;
                if (tokens[i].class_part == "ID")
                {
                    string N = tokens[i].value_part;
                    i++;
                    if (tokens[i].class_part == "(")
                    {
                        createScope();
                        Infunc = true;
                        InFunction = new List<SymbolTable1>();
                      
                           
                        i++;
                        string PL = "", AL = "";
                        if (args(PL, ref AL))
                        {
                            if (AL == "")
                                AL = "void";
                            generate(classes[classes.Count - 1].name + "_" + N + "_" + AL + " proc");
                            if (tokens[i].class_part == ")")
                            {
                                i++;
                                string RT = "";                                                                 //----------------------------------//
                                if (rets(ref RT))                                                              //                                  //
                                {                                                                             //       semantic                   //
                                    if (Iclookup(att, N, AL) == null && Ifnflookup(att, N) == null)//                 work             //
                                    {                                                                       //                                  //
                                        SymbolTable3 t = new SymbolTable3(N, AL + "->" + RT, A, S);        //                                  //                 
                                        att.Add(t);                                                       //                                  //      
                                        att[att.Count - 1].funclocals = InFunction;
                                    }                                                                    //                                  // 
                                    else                                                                //                                  //
                                        Console.WriteLine("Redeclaration of method at " + tokens[i].line_no);//----------------------------------//

                                    if (Body())
                                    {
                                        Infunc = false;
                                        generate(classes[classes.Count - 1].name + "_" + N + "_" + AL + " endp");
                                        generate("");
                                        if (Bodyc(ref att))
                                            return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            else if (tokens[i].class_part == "DataType")
            {
                string T = tokens[i].value_part;
                i++;
                if (tokens[i].class_part == "ID")
                {
                    string N = tokens[i].value_part;
                    i++;
                    if (Iclookup(att, N) == null)                                                      ////////////////////////////////////
                    {                                                                              //////////////////////////////////////////
                        SymbolTable3 t = new SymbolTable3(N, T, A, "nonstatic");                     ////////////////////////////////////
                        att.Add(t);                                                                   //            Semantic   Work    //
                    }                                                                                   /////////////////////////////////
                    else                                                                              ////////////////////////////////////
                        Console.WriteLine("Redeclaration of variable in class at " + tokens[i].line_no);////////////////////////////////////
                    if (ArrayDec_Dec1(T, A, "nonstatic"))
                    {
                        if (Bodyc(ref att))
                        { return true; }
                    }
                }
            }

            return false;
        }

        bool Bodyc2(ref List<SymbolTable3> att, string A, string S)
        {
            if (tokens[i].class_part == "ID")
            {
                string N = tokens[i].value_part;
                i++;
                if (tokens[i].class_part == "(")
                {
                    createScope();
                    Infunc = true;
                    InFunction = new List<SymbolTable1>();
                   
                    i++;
                    string PL = "", AL = "";
                    if (args(PL, ref AL))
                    {
                        AL = "void";
                        generate(classes[classes.Count - 1].name + "_" + N + "_" + PL + " proc");
                        if (tokens[i].class_part == ")")
                        {
                            i++;
                            string RT = "";
                            if (rets(ref RT))
                            {
                                if (Iclookup(att, N, AL) == null && Ifnflookup(att, N) == null)                //       SEmantic          work            //
                                {                                                                //                                //
                                    SymbolTable3 t = new SymbolTable3(N, AL + "->" + RT, A, S);  //                                 //                 
                                    att.Add(t);                                                   //                                  //      
                                    att[att.Count - 1].funclocals = InFunction;
                                }                                                                    //                                   // 
                                else                                                           //                                  //
                                    Console.WriteLine("Redeclaration of method at " + tokens[i].line_no);//------------------------//

                                if (Body())
                                {
                                    Infunc = false;
                                    generate(classes[classes.Count - 1].name + "_" + N + "_" + PL + " endp");
                                    generate("");
                                    if (Bodyc(ref att))
                                    {
                                        return true;
                                    }
                                }
                            }

                        }
                    }
                }
            }

            else if (tokens[i].class_part == "DataType")
            {
                string T = tokens[i].value_part;
                i++;
                if (tokens[i].class_part == "ID")
                {
                    string N = tokens[i].value_part;
                    i++;
                    if (Iclookup(att, N) == null)                                                      ////////////////////////////////////
                    {                                                                              //////////////////////////////////////////
                        SymbolTable3 t = new SymbolTable3(N, T, A, S);                                   ////////////////////////////////////
                        att.Add(t);                                                                   //            Semantic   Work    //
                    }                                                                                   /////////////////////////////////
                    else                                                                              ////////////////////////////////////
                        Console.WriteLine("Redeclaration of variable in class at " + tokens[i].line_no);////////////////////////////////////
                    if (ArrayDec_Dec1(T, A, S))
                    {
                        if (Bodyc(ref att))
                            return true;
                    }
                }
            }

            return false;
        }

        bool ArrayDec_Dec()
        {
            if (tokens[i].class_part == "Access_Modifier" || tokens[i].class_part == "static" || tokens[i].class_part == "DataType")
            {
                string A = "";
                if (AM(ref A))
                {
                    string s = "";
                    if (statics(ref s))
                    {
                        if (tokens[i].class_part == "DataType")
                        {
                            string T = tokens[i].value_part;

                            i++;

                            if (tokens[i].class_part == "ID")
                            {
                                string N = tokens[i].value_part;
                                i++;

                                if (Inclass && Infunc)
                                {
                                    if (IcIflookup(InFunction, N, S) == null)                                                      ///////////////////////////////////
                                    {                                                                                         //
                                        SymbolTable1 temp = new SymbolTable1(N, T, stack);                                    //
                                        InFunction.Add(temp);                                                                //
                                    }                                                                                         //           Semantic
                                    else                                                                                      //                      
                                        Console.WriteLine("Redeclaration of variable " + N + " in method at " + tokens[i].line_no); //                  Work
                                }

                                else
                                {
                                    if (dlookup(N, S) == null)                                 //--------------------------------//
                                        Insert(N, T, stack);                             //                                    //
                                    else                                                    //       Semantic Work              //
                                        Console.WriteLine("Recdalaration of " + N + " at " + tokens[i].line_no);    //-------------------------------------//
                                }
                                if (ArrayDec_Dec1(T, A, s))
                                    return true;

                            }
                        }
                    }

                }

            }


            return false;
        }


        bool ArrayDec_Dec1(string T, string A, string s)
        {
            if (tokens[i].class_part == "[")
            {
                i++;
                string TI = "",NI="";
                if (OE(ref TI,ref NI))
                {
                    if (TI != "integer")
                        Console.WriteLine("Invalid Index at " + tokens[i].line_no);
                    if (tokens[i].class_part == "]")
                    {
                        i++;
                        if (A_INIT(T))
                            if (A_LIST(T, A, s))
                                return true;

                    }
                }

            }

            else if (INIT(T))
            {
                if (LIST(T, A, s))
                    return true;
            }


            return false;
        }

        bool A_INIT(string T)
        {
            if (tokens[i].class_part == "=")
            {
                i++;
                if (tokens[i].class_part == "{")
                {
                    i++;
                    string RT = "",NT="";
                    if (OE(ref RT,ref NT))
                    {
                        if (T != RT)                                                                                 ////SEmantic
                            Console.WriteLine("Invalid Initialization of array at " + tokens[i].line_no);               ////Work
                        if (A_INIT2(T))
                        {
                            if (tokens[i].class_part == "}")
                            {
                                i++;
                                return true;
                            }
                        }
                    }
                }

            }

            else if (tokens[i].class_part == "." || tokens[i].class_part == ",")
                return true;

            return false;
        }

        bool A_INIT2(string T)
        {
            if (tokens[i].class_part == ",")
            {
                i++;
                string RT = "", NT = "" ;
                if (OE(ref RT,ref NT))
                {
                    if (T != RT)                                                                           ////SEmantic
                        Console.WriteLine("Invalid Initialization at " + tokens[i].line_no);               ////Work
                    if (A_INIT2(T))
                        return true;
                }

            }
            else if (tokens[i].class_part == "}")
                return true;

            return false;
        }


        bool A_LIST(string T, string A, string s)
        {
            if (tokens[i].class_part == ".")
            {
                i++;
                return true;
            }
            else if (tokens[i].class_part == ",")
            {
                i++;
                if (tokens[i].class_part == "ID")
                {
                    string N = tokens[i].value_part;
                    i++;
                    if (Inclass == true && Infunc == false)                                                     /////////////////////////////
                    {                                                                                   //////////////////////////////
                        if (Iclookup(atts, N) == null)                                                           /////////////////////////////
                        {                                                                                     //      Semantic work    //
                            SymbolTable3 temp = new SymbolTable3(N, T, A, s);                                     //                        //     
                            atts.Add(temp);                                                                  //                      // 
                            ///////////////////////////////
                        }                                                                                  //////////////////////////////
                        else                                                                               ////////////////////////////
                            Console.WriteLine("Redeclaration of variable " + N + " in class at " + tokens[i].line_no);/////////////////////
                    }
                    else if (Inclass && Infunc)
                    {
                        if (IcIflookup(InFunction, N, S) == null)                                                      ///////////////////////////////////
                        {                                                                                         //
                            SymbolTable1 temp = new SymbolTable1(N, T, stack);                                    //
                            InFunction.Add(temp);                                                                //
                        }                                                                                         //           Semantic
                        else                                                                                      //                      
                            Console.WriteLine("Redeclaration of variable " + N + " in method at " + tokens[i].line_no); //                  Work
                    }
                    else
                    {
                        if (dlookup(N, S) == null)                                                        //--------------------------------//
                            Insert(N, T, stack);                                                          //                                    //
                        else                                                                                //       Semantic Work              //
                            Console.WriteLine("Recdalaration of variable " + N + " at " + tokens[i].line_no);//                                     //
                    }                                                                                          //-------------------------------------//
                    if (tokens[i].class_part == "[")
                    {
                        i++;
                        string RT = "",NT="";
                        if (OE(ref RT,ref NT))
                        {

                            if (tokens[i].class_part == "]")
                            {
                                i++;
                                if (A_INIT(T))
                                    if (A_LIST(T, A, s))
                                        return true;
                            }
                        }
                    }

                }
            }


            return false;
        }


        bool INIT(string T)
        {


            if (tokens[i].class_part == "=")
            {
                i++;
                string RT = "",NT="";
                if (OE(ref RT,ref NT))
                {
                    if (T != RT)
                        Console.WriteLine("Cannot Implicitly convert type " + RT + " to " + T + " at " + tokens[i].line_no);
                    return true;
                }
            }
            else if (tokens[i].class_part == "." || tokens[i].class_part == ",")
            {
                return true;

            }

            return false;
        }

        bool LIST(string T, string A, string s)
        {
            if (tokens[i].class_part == ".")
            {
                i++;
                return true;

            }

            else if (tokens[i].class_part == ",")
            {
                i++;
                if (tokens[i].class_part == "ID")
                {
                    string N = tokens[i].value_part;
                    i++;
                    if (Inclass == true && Infunc == false)                                                     /////////////////////////////
                    {                                                                                   //////////////////////////////
                        if (Iclookup(atts, N) == null)                                                           /////////////////////////////
                        {                                                                                     //      Semantic work    //
                            SymbolTable3 temp = new SymbolTable3(N, T, A, s);                                     //                        //     
                            atts.Add(temp);                                                                  //                      // 
                            ///////////////////////////////
                        }                                                                                  //////////////////////////////
                        else                                                                               ////////////////////////////
                            Console.WriteLine("Redeclaration of variable " + N + " in class at " + tokens[i].line_no);/////////////////////
                    }
                    else if (Inclass && Infunc)
                    {
                        if (IcIflookup(InFunction, N, S) == null)                                                      ///////////////////////////////////
                        {                                                                                         //
                            SymbolTable1 temp = new SymbolTable1(N, T, stack);                                    //
                            InFunction.Add(temp);                                                                //
                        }                                                                                         //           Semantic
                        else                                                                                      //                      
                            Console.WriteLine("Redeclaration of variable " + N + " in method at " + tokens[i].line_no); //                  Work
                    }
                    else
                    {
                        if (dlookup(N, S) == null)                                                        //--------------------------------//
                            Insert(N, T, stack);                                                          //                                    //
                        else                                                                                //       Semantic Work              //
                            Console.WriteLine("Recdalaration of variable " + N + " at " + tokens[i].line_no);//                                     //
                    }                                                                                          //-------------------------------------//
                    if (INIT(T))
                        if (LIST(T, A, s))
                            return true;

                }
            }

            return false;
        }

        bool statics(ref string S)
        {

            if (tokens[i].class_part == "static")
            {
                S = "static";
                i++;
                return true;
            }
            else if (tokens[i].class_part == "DataType")
                return true;
            return false;
        }

        bool ObjCreate()
        {
            if (tokens[i].class_part == "ID")
            {
                i++;
                if (tokens[i].class_part == "ID")
                {
                    i++;
                    if (isOArr())
                    {
                        if (ObjCreate5(null, null))
                            return true;
                    }
                }

            }
            return false;
        }


        bool isOArr()
        {
            if (tokens[i].class_part == "[")
            {
                i++;
                string RT = "",NT="";
                if (OE(ref RT,ref NT))
                {
                    if (tokens[i].class_part == "]")
                    {
                        i++;
                        return true;
                    }
                }
            }

            else if (tokens[i].class_part == "=" || tokens[i].class_part == ".")
                return true;

            return false;

        }
        bool ObjCreate5(string N, string T)
        {
            if (tokens[i].class_part == "=")
            {
                i++;
                if (tokens[i].class_part == "new")
                {
                    i++;
                    if (tokens[i].class_part == "ID")
                    {
                        string N1 = tokens[i].value_part;
                        i++;
                        if (tokens[i].class_part == "(")
                        {
                            i++;
                            string AL = "";
                            if (param(ref AL))
                            {
                                if (tokens[i].class_part == ")")
                                {
                                    i++;
                                    if (tokens[i].class_part == ".")
                                    {
                                        if (N1 != N)
                                            Console.WriteLine("Type Mismatch at " + tokens[i].line_no);
                                        if (clmethodlookup(T, T, AL) == null)
                                            Console.WriteLine("No such constructor defined for class " + T + " at " + tokens[i].line_no);
                                        string fcall = Createtemp();
                                        if (AL != "")
                                        {
                                            string[] noargs = AL.Split(',');
                                            generate(fcall + "=call " + N1 + "_" + AL + "," + noargs.Length);
                                        }
                                        else
                                            generate(fcall + "=call " + N1 + "_" + AL + ",0");
                                        i++;
                                        return true;
                                    }
                                }
                            }

                        }
                    }
                }
            }

            else if (tokens[i].class_part == ".")
            {
                i++;
                return true;
            }
            return false;
        }

        bool ObjCreate4()
        {
            if (tokens[i].class_part == "ID")
            {
                i++;
                if (tokens[i].class_part == ".")
                {
                    i++;
                    if (ObjCreate6())
                        return true;
                }
            }

            else if (tokens[i].class_part == "new")
            {
                i++;
                if (tokens[i].class_part == "ID")
                {
                    string N = tokens[i].value_part;
                    i++;
                    if (tokens[i].class_part == "(")
                    {
                        i++;
                        string RT = "";
                        if (param(ref RT))
                        {
                            string fcall = Createtemp();
                            if (RT != "")
                            {
                                string[] noargs = RT.Split(',');
                                generate(fcall + "=call " + N + "_" + RT + "," + noargs.Length);
                            }
                            else
                                generate(fcall + "=call " + N + "_" + RT + "," + 0);
                            if (tokens[i].class_part == ")")
                            {
                                i++;
                                if (tokens[i].class_part == ".")
                                {
                                    i++;
                                    return true;
                                }
                            }
                        }

                    }
                }
            }

            return false;
        }

        bool param(ref string T)
        {
            string T1 = "";
            if (tokens[i].class_part == "ID" || tokens[i].class_part == "IncDec" || tokens[i].class_part == "(" || tokens[i].class_part == "!" || tokens[i].class_part == "integer" || tokens[i].class_part == "decimal" || tokens[i].class_part == "word" || tokens[i].class_part == "char" || tokens[i].class_part == "bool")
            {
                string RT = "",NT="";
                if (OE(ref RT,ref NT))
                {
                    T1 += RT;
                    generate("param " + NT);
                    if (param1(T1, ref T))
                        return true;

                }
            }
            else if (tokens[i].class_part == ")")
            {
                T = T1;
                return true;
            }

            return false;
        }

        bool param1(string T1, ref string T)
        {
            if (tokens[i].class_part == ",")
            {
                i++;
                string RT = "",NT="";
                if (OE(ref RT,ref NT))
                {
                    T1 += "," + RT;
                    generate("param " + NT);
                    if (param1(T1, ref T))
                        return true;

                }
            }
            else if (tokens[i].class_part == ")")
            {
                T = T1;
                return true;
            }


            return false;
        }

        bool brk()
        {
            if (tokens[i].class_part == "break")
            {
                i++;
                if (tokens[i].class_part == ".")
                {
                    i++;
                    return true;
                }
            }
            return false;
        }

        bool cont()
        {
            if (tokens[i].class_part == "continue")
            {
                i++;
                if (tokens[i].class_part == ".")
                {
                    i++;
                    return true;
                }
            }
            return false;
        }

        bool ObjCreate3()
        {
            if (tokens[i].class_part == ".")
            {
                i++;
                return true;
            }
            else if (tokens[i].class_part == "(")
            {
                i++;
                string RT = "";
                if (param(ref RT))
                {
                    if (tokens[i].class_part == ")")
                    {
                        i++;
                        if (tokens[i].class_part == ".")
                        {
                            i++;
                            return true;
                        }
                    }
                }
            }
            return false;

        }

        bool ObjCreate6()
        {
            if (tokens[i].class_part == "ID" && tokens[i - 1].line_no == tokens[i].line_no)
            {
                i++;
                if (ObjCreate3())
                { return true; }
            }
            else if (tokens[i].class_part == "Access_Modifier" || tokens[i].class_part == "static" || tokens[i].class_part == "nonstatic" || tokens[i].class_part == "DataType" || tokens[i].class_part == "enum" || tokens[i].class_part == "ID" || tokens[i].class_part == "}")
                return true;

            return false;
        }

        bool func()
        {
            if (tokens[i].class_part == "Access_Modifier" || tokens[i].class_part == "static" || tokens[i].class_part == "nonstatic")
            {
                string A = "";
                if (AM(ref A))
                {
                    if (_static())
                    {
                        if (tokens[i].class_part == "ID")
                        {
                            string N = tokens[i].value_part;
                            i++;

                            if (tokens[i].class_part == "(")
                            {
                                createScope();
                                string PL = "", AL = "";
                                i++;
                                if (args(PL, ref AL))
                                {

                                    if (tokens[i].class_part == ")")
                                    {

                                        i++;
                                        string RT = "";
                                        if (rets(ref RT))
                                        {
                                            if (dlookup(N, AL + "->" + RT) == false)                         //------------------------------------//
                                                Insert(N, AL + "->" + RT);                              //                  Semantic         //
                                            else                                                         //                          work    //
                                                Console.WriteLine("Redeclaration of function " + N);     //------------------------------------//     

                                            if (Body())
                                                return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;

        }

        bool rets(ref string RT)
        {
            if (tokens[i].class_part == "returns")
            {
                i++;
                if (rets1(ref RT))
                    return true;
            }
            return false;
        }

        bool rets1(ref string RT)
        {
            if (tokens[i].class_part == "DataType" || tokens[i].class_part == "ID")
            {
                RT = tokens[i].value_part;
                i++;
                if (isArrret())
                    return true;
            }

            else if (tokens[i].class_part == "void")
            {
                RT += "void";
                i++;
                return true;
            }
            return false;


        }

        bool isArrret()
        {
            if (tokens[i].class_part == "[")
            {
                i++;
                if (tokens[i].class_part == "]")
                {
                    i++;
                    return true;
                }
            }
            if (tokens[i].class_part == "{")
                return true;
            return false;
        }

        bool AM(ref string A)
        {
            if (tokens[i].class_part == "Access_Modifier")
            {
                A = tokens[i].value_part;
                i++; return true;
            }
            else if (tokens[i].class_part == "static" || tokens[i].class_part == "nonstatic" || tokens[i].class_part == "DataType")
                return true;
            return false;
        }

        bool _static()
        {
            if (tokens[i].class_part == "static" || tokens[i].class_part == "nonstatic")
            { i++; return true; }
            return false;

        }

        bool args(string PL, ref string AL)
        {
            if (tokens[i].class_part == "DataType" || tokens[i].class_part == "ID")
            {
                string T = tokens[i].value_part;
                PL += T;
                i++;
                if (isArr())
                {
                    if (tokens[i].class_part == "ID")
                    {
                        string N = tokens[i].value_part;
                        i++;
                        if (Inclass && Infunc)
                        {
                            if (IcIflookup(InFunction, N, S) == null)                                                      ///////////////////////////////////
                            {                                                                                         //
                                SymbolTable1 temp = new SymbolTable1(N, T, stack);                                    //
                                InFunction.Add(temp);                                                                //
                            }                                                                                         //           Semantic
                            else                                                                                      //                      
                                Console.WriteLine("Redeclaration of variable " + N + " in method at " + tokens[i].line_no); //                  Work
                        }                                                                                      //
                        //
                        else                                                                 // //  //  // //
                        {                                                                   /////////////////////////////////////////////////////////
                            if (dlookup(N, S) == null)                                 //--------------------------------//
                                Insert(N, T, stack);                             //                                    //
                            else                                                    //       Semantic Work              //
                                Console.WriteLine("Recdalaration of " + N);    //-------------------------------------//
                        }
                        if (args1(PL, ref AL))
                        {
                            return true;
                        }

                    }
                }

            }
            else if (tokens[i].class_part == ")")
            {
                AL = PL;
                return true;
            }

            return false;
        }

        bool isArr()
        {
            if (tokens[i].class_part == "[")
            {
                i++;
                if (tokens[i].class_part == "]")
                {
                    i++;
                    return true;
                }
            }
            else if (tokens[i].class_part == "ID")
                return true;
            return false;
        }

        bool args1(string PL, ref string AL)
        {
            if (tokens[i].class_part == ",")
            {
                i++;
                if (tokens[i].class_part == "DataType" || tokens[i].class_part == "ID")
                {
                    string T = tokens[i].value_part;
                    PL += "," + T;
                    i++;
                    if (isArr())
                    {
                        if (tokens[i].class_part == "ID")
                        {
                            string N = tokens[i].value_part;
                            i++;
                            if (Inclass && Infunc)
                            {
                                if (IcIflookup(InFunction, N, S) == null)                                                      ///////////////////////////////////
                                {                                                                                         //
                                    SymbolTable1 temp = new SymbolTable1(N, T, stack);                                    //
                                    InFunction.Add(temp);                                                                //
                                }                                                                                         //           Semantic
                                else                                                                                      //                      
                                    Console.WriteLine("Redeclaration of variable " + N + " in method at " + tokens[i].line_no); //                  Work
                            }                                                                                      //
                            //
                            else                                                                 // //  //  // //
                            {                                                                   /////////////////////////////////////////////////////////
                                if (dlookup(N, S) == null)                                 //--------------------------------//
                                    Insert(N, T, stack);                             //                                    //
                                else                                                    //       Semantic Work              //
                                    Console.WriteLine("Recdalaration of " + N);    //-------------------------------------//
                            }
                            if (args1(PL, ref AL))
                                return true;
                        }
                    }

                }

            }
            else if (tokens[i].class_part == ")")
            {
                AL = PL;
                return true;
            }
            return false;
        }


        bool ret()
        {
            if (tokens[i].class_part == "returns")
            {
                i++;
                if (ret1())
                {
                    if (tokens[i].class_part == ".")
                    { i++; return true; }
                }
            }

            return false;
        }


        bool ret1()
        {
            string RT = "",NT="";
            if (OE(ref RT,ref NT))
                return true;
            else if (tokens[i].class_part == ".")
                return true;

            return false;
        }

        bool OE(ref string T2,ref string N2)
        {
           
            string T1 = "",N1="";
            if (ANDE(ref T1,ref N1))
                if (OEdash(T1, ref T2,N1,ref N2))
                {
                    return true;



                }


            return false;
        }

        bool OEdash(string T1, ref string T4,string N1,ref string N4)
        {
            string T2 = "",N2="";
            if (tokens[i].class_part == "||")
            {
                string op = tokens[i].value_part;
                i++;
                if (ANDE(ref T2,ref N2))
                {
                    string T3 = compatibility(T1, T2, op);                                                  //---------------------------------------//
                    if (T3 == null)                                                                        ///////////////////////////////////////////
                    {                                                                                      /////////////////////////////////////////
                        if (T1 == null && T2 != null)                                                              ////////////////////////////////////////
                            Console.WriteLine("Type Mismatch between undefined variable/type" + " and " + T2+" at "+tokens[i].line_no); ///////////////////////////////////////////
                        else if (T2 == null && T1 != null)                                                   //////////////////////////////////////////
                            Console.WriteLine("Type Mismatch between " + T1 + " and undefined variable/type" + " at " + tokens[i].line_no);     ///////////////////////////////////////////
                        else if (T2 == null && T1 == null)                                                    /////////////////////////////////////////
                            Console.WriteLine("Type Mismatch between undefined variable/type and undefined variable/type" + " at " + tokens[i].line_no);///////////////////////////////////////
                        else                                                                                 //       SEMANTIC WORK                   //
                            Console.WriteLine("Type Mismatch between " + T1 + " and " + T2 + " at " + tokens[i].line_no);                  //--------------------------------------//
                    }
                    string N3 = Createtemp();
                    generate(N3 + "=" + N1 + op + N2);
                    if (OEdash(T3, ref T4,N3,ref N4))
                        return true;
                }
            }
            else if (tokens[i].class_part == "." || tokens[i].class_part == "," || tokens[i].class_part == "times" || tokens[i].class_part == "}" || tokens[i].class_part == ")" || tokens[i].class_part == "]")
            {
                T4 = T1;
                N4=N1;
                return true;
            }
            return false;
        }

        bool ANDE(ref string T2,ref string N2)
        {
            string T1 = "", N1 = "" ;
            if (BE1(ref T1,ref N1))
                if (ANDEdash(T1, ref T2,N1,ref N2))
                {
                    return true;
                }


            return false;
        }

        bool ANDEdash(string T1, ref string T4,string N1,ref string N4)
        {
            string T2 = "",N2="";
            if (tokens[i].class_part == "&&")
            {
                string op = tokens[i].value_part;
                i++;
                if (BE1(ref T2,ref N2))
                {
                    string T3 = compatibility(T1, T2, op);                                                  //---------------------------------------//
                    if (T3 == null)                                                                        ///////////////////////////////////////////
                    {                                                                                      /////////////////////////////////////////
                        if (T1 == null && T2 != null)                                                              ////////////////////////////////////////
                            Console.WriteLine("Type Mismatch between undefined variable/type" + " and " + T2 + " at " + tokens[i].line_no); ///////////////////////////////////////////
                        else if (T2 == null && T1 != null)                                                   //////////////////////////////////////////
                            Console.WriteLine("Type Mismatch between " + T1 + " and undefined variable/type" + " at " + tokens[i].line_no);     ///////////////////////////////////////////
                        else if (T2 == null && T1 == null)                                                    /////////////////////////////////////////
                            Console.WriteLine("Type Mismatch between undefined variable/type and undefined variable/type" + " at " + tokens[i].line_no);///////////////////////////////////////
                        else                                                                                 //       SEMANTIC WORK                   //
                            Console.WriteLine("Type Mismatch between " + T1 + " and " + T2 + " at " + tokens[i].line_no);                  //--------------------------------------//
                    }
                    string N3 = Createtemp();
                    generate(N3 + "=" + N1 + op + N2);
                    if (ANDEdash(T3, ref T4,N3,ref N4))
                        return true;
                }
            }
            else if (tokens[i].class_part == "||" || tokens[i].class_part == "." || tokens[i].class_part == "," || tokens[i].class_part == "times" || tokens[i].class_part == "}" || tokens[i].class_part == ")" || tokens[i].class_part == "]")
            {
                T4 = T1;
                N4 = N1;
                return true;
            }
            return false;
        }

        bool BE1(ref string T2,ref string N2)
        {
            string T1 = "",N1="";
            if (BE2(ref T1,ref N1))
                if (BE1dash(T1, ref T2,N1,ref N2))
                {
                    return true;
                }


            return false;
        }

        bool BE1dash(string T1, ref string T4,string N1,ref string N4)
        {
            string T2 = "",N2="";
            if (tokens[i].class_part == "|")
            {
                string op = tokens[i].value_part;
                i++;
                if (BE2(ref T2,ref N2))
                {
                    string T3 = compatibility(T1, T2, op);                                                  //---------------------------------------//
                    if (T3 == null)                                                                        ///////////////////////////////////////////
                    {                                                                                      /////////////////////////////////////////
                        if (T1 == null && T2 != null)                                                              ////////////////////////////////////////
                            Console.WriteLine("Type Mismatch between undefined variable/type" + " and " + T2 + " at " + tokens[i].line_no); ///////////////////////////////////////////
                        else if (T2 == null && T1 != null)                                                   //////////////////////////////////////////
                            Console.WriteLine("Type Mismatch between " + T1 + " and undefined variable/type" + " at " + tokens[i].line_no);     ///////////////////////////////////////////
                        else if (T2 == null && T1 == null)                                                    /////////////////////////////////////////
                            Console.WriteLine("Type Mismatch between undefined variable/type and undefined variable/type" + " at " + tokens[i].line_no);///////////////////////////////////////
                        else                                                                                 //       SEMANTIC WORK                   //
                            Console.WriteLine("Type Mismatch between " + T1 + " and " + T2 + " at " + tokens[i].line_no);                  //--------------------------------------//
                    }
                    string N3 = Createtemp();
                    generate(N3 + "=" + N1 + op + N2);
                    if (BE1dash(T3, ref T4,N3,ref N4))
                        return true;
                }
            }
            else if (tokens[i].class_part == "&&" || tokens[i].class_part == "||" || tokens[i].class_part == "." || tokens[i].class_part == "," || tokens[i].class_part == "times" || tokens[i].class_part == "}" || tokens[i].class_part == ")" || tokens[i].class_part == "]")
            {
                T4 = T1;
                N4 = N1;
                return true;
            }
            return false;
        }

        bool BE2(ref string T2,ref string N2)
        {
            string T1 = "",N1="";
            if (BE3(ref T1,ref N1))
                if (BE2dash(T1, ref T2,N1,ref N2))
                {
                    return true;
                }


            return false;
        }

        bool BE2dash(string T1, ref string T4,string N1,ref string N4)
        {
            string T2 = "",N2="";
            if (tokens[i].class_part == "~")
            {
                string op = tokens[i].value_part;
                i++;
                if (BE3(ref T2,ref N2))
                {
                    string T3 = compatibility(T1, T2, op);                                                  //---------------------------------------//
                    if (T3 == null)                                                                        ///////////////////////////////////////////
                    {                                                                                      /////////////////////////////////////////
                        if (T1 == null && T2 != null)                                                              ////////////////////////////////////////
                            Console.WriteLine("Type Mismatch between undefined variable/type" + " and " + T2 + " at " + tokens[i].line_no); ///////////////////////////////////////////
                        else if (T2 == null && T1 != null)                                                   //////////////////////////////////////////
                            Console.WriteLine("Type Mismatch between " + T1 + " and undefined variable/type" + " at " + tokens[i].line_no);     ///////////////////////////////////////////
                        else if (T2 == null && T1 == null)                                                    /////////////////////////////////////////
                            Console.WriteLine("Type Mismatch between undefined variable/type and undefined variable/type" + " at " + tokens[i].line_no);///////////////////////////////////////
                        else                                                                                 //       SEMANTIC WORK                   //
                            Console.WriteLine("Type Mismatch between " + T1 + " and " + T2 + " at " + tokens[i].line_no);                  //--------------------------------------//
                    }
                    string N3 = Createtemp();
                    generate(N3 + "=" + N1 + op + N2);
                    if (BE2dash(T3, ref T4,N3,ref N4))
                        return true;
                }
            }
            else if (tokens[i].class_part == "|" || tokens[i].class_part == "&&" || tokens[i].class_part == "||" || tokens[i].class_part == "." || tokens[i].class_part == "," || tokens[i].class_part == "times" || tokens[i].class_part == "}" || tokens[i].class_part == ")" || tokens[i].class_part == "]")
            {
                T4 = T1;
                N4 = N1;
                return true;
            }
            return false;
        }

        bool BE3(ref string T2,ref string N2)
        {
            string T1 = "",N1="";
            if (RE(ref T1,ref N1))
                if (BE3dash(T1, ref T2,N1,ref N2))
                {
                    return true;
                }


            return false;
        }

        bool BE3dash(string T1, ref string T4,string N1,ref string N4)
        {
            string T2 = "",N2="";
            if (tokens[i].class_part == "&")
            {
                string op = tokens[i].value_part;
                i++;
                if (RE(ref T2,ref N2))
                {
                    string T3 = compatibility(T1, T2, op);                                                  //---------------------------------------//
                    if (T3 == null)                                                                        ///////////////////////////////////////////
                    {                                                                                      /////////////////////////////////////////
                        if (T1 == null && T2 != null)                                                              ////////////////////////////////////////
                            Console.WriteLine("Type Mismatch between undefined variable/type" + " and " + T2 + " at " + tokens[i].line_no); ///////////////////////////////////////////
                        else if (T2 == null && T1 != null)                                                   //////////////////////////////////////////
                            Console.WriteLine("Type Mismatch between " + T1 + " and undefined variable/type" + " at " + tokens[i].line_no);     ///////////////////////////////////////////
                        else if (T2 == null && T1 == null)                                                    /////////////////////////////////////////
                            Console.WriteLine("Type Mismatch between undefined variable/type and undefined variable/type" + " at " + tokens[i].line_no);///////////////////////////////////////
                        else                                                                                 //       SEMANTIC WORK                   //
                            Console.WriteLine("Type Mismatch between " + T1 + " and " + T2 + " at " + tokens[i].line_no);                  //--------------------------------------//
                    }
                    string N3 = Createtemp();
                    generate(N3 + "=" + N1 + op + N2);
                    if (BE3dash(T3, ref T4,N3,ref N4))
                        return true;
                }
            }
            else if (tokens[i].class_part == "~" || tokens[i].class_part == "|" || tokens[i].class_part == "&&" || tokens[i].class_part == "||" || tokens[i].class_part == "." || tokens[i].class_part == "," || tokens[i].class_part == "times" || tokens[i].class_part == "}" || tokens[i].class_part == ")" || tokens[i].class_part == "]")
            {
                T4 = T1;
                N4 = N1;
                return true;
            }
            return false;
        }

        bool RE(ref string T2,ref string N2)
        {
            string T1 = "",N1="";
            if (SE(ref T1,ref N1))
                if (REdash(T1, ref T2,N1,ref N2))
                {
                    return true;
                }


            return false;
        }

        bool REdash(string T1, ref string T4,string N1,ref string N4)
        {
            string T2 = "", N2 = "";
            if (tokens[i].class_part == "ROP")
            {
                string op = tokens[i].value_part;
                i++;
                if (SE(ref T2,ref N2))
                {
                    string T3 = compatibility(T1, T2, op);                                                  //---------------------------------------//
                    if (T3 == null)                                                                        ///////////////////////////////////////////
                    {                                                                                      /////////////////////////////////////////
                        if (T1 == null && T2 != null)                                                              ////////////////////////////////////////
                            Console.WriteLine("Type Mismatch between undefined variable/type" + " and " + T2 + " at " + tokens[i].line_no); ///////////////////////////////////////////
                        else if (T2 == null && T1 != null)                                                   //////////////////////////////////////////
                            Console.WriteLine("Type Mismatch between " + T1 + " and undefined variable/type" + " at " + tokens[i].line_no);     ///////////////////////////////////////////
                        else if (T2 == null && T1 == null)                                                    /////////////////////////////////////////
                            Console.WriteLine("Type Mismatch between undefined variable/type and undefined variable/type" + " at " + tokens[i].line_no);///////////////////////////////////////
                        else                                                                                 //       SEMANTIC WORK                   //
                            Console.WriteLine("Type Mismatch between " + T1 + " and " + T2 + " at " + tokens[i].line_no);                  //--------------------------------------//
                    }
                    string N3 = Createtemp();
                    generate(N3 + "=" + N1 + op + N2);
                    if (REdash(T3, ref T4,N3,ref N4))
                        return true;
                }
            }
            else if (tokens[i].class_part == "&" || tokens[i].class_part == "~" || tokens[i].class_part == "|" || tokens[i].class_part == "&&" || tokens[i].class_part == "||" || tokens[i].class_part == "." || tokens[i].class_part == "," || tokens[i].class_part == "times" || tokens[i].class_part == "}" || tokens[i].class_part == ")" || tokens[i].class_part == "]")
            {
                T4 = T1;
                N4 = N1;
                return true;
            }
            return false;
        }


        bool SE(ref string T2,ref string N2)
        {
            string T1 = "", N1 = "" ;
            if (E(ref T1,ref N1))
                if (SEdash(T1, ref T2,N1,ref N2))
                {
                    return true;
                }


            return false;
        }

        bool SEdash(string T1, ref string T4,string N1,ref string N4)
        {
            string T2 = "",N2="";
            if (tokens[i].class_part == "Shift")
            {
                string op = tokens[i].value_part;
                i++;
                if (E(ref T2,ref N2))
                {
                    string T3 = compatibility(T1, T2, op);                                                  //---------------------------------------//
                    if (T3 == null)                                                                        ///////////////////////////////////////////
                    {                                                                                      /////////////////////////////////////////
                        if (T1 == null && T2 != null)                                                              ////////////////////////////////////////
                            Console.WriteLine("Type Mismatch between undefined variable/type" + " and " + T2 + " at " + tokens[i].line_no); ///////////////////////////////////////////
                        else if (T2 == null && T1 != null)                                                   //////////////////////////////////////////
                            Console.WriteLine("Type Mismatch between " + T1 + " and undefined variable/type" + " at " + tokens[i].line_no);     ///////////////////////////////////////////
                        else if (T2 == null && T1 == null)                                                    /////////////////////////////////////////
                            Console.WriteLine("Type Mismatch between undefined variable/type and undefined variable/type" + " at " + tokens[i].line_no);///////////////////////////////////////
                        else                                                                                 //       SEMANTIC WORK                   //
                            Console.WriteLine("Type Mismatch between " + T1 + " and " + T2 + " at " + tokens[i].line_no);                  //--------------------------------------//
                    }
                    string N3 = Createtemp();
                    generate(N3 +"=" +N1 + op + N2);
                    if (SEdash(T3, ref T4,N3,ref N4))
                        return true;
                }
            }
            else if (tokens[i].class_part == "ROP" || tokens[i].class_part == "&" || tokens[i].class_part == "~" || tokens[i].class_part == "|" || tokens[i].class_part == "&&" || tokens[i].class_part == "||" || tokens[i].class_part == "." || tokens[i].class_part == "," || tokens[i].class_part == "times" || tokens[i].class_part == "}" || tokens[i].class_part == ")" || tokens[i].class_part == "]")
            {
                T4 = T1;
                N4 = N1;
                return true;
            }
            return false;
        }


        bool E(ref string T2,ref string N2)
        {
            string T1 = "",N1="";
            if (T(ref T1,ref N1))
                if (Edash(T1, ref T2,N1,ref N2))
                {
                    return true;
                }


            return false;
        }

        bool Edash(string T1, ref string T4,string N1, ref string N4)
        {
            string T2 = "", N2 = "";
            if (tokens[i].class_part == "PlusMinus")
            {
                string op = tokens[i].value_part;
                i++;
                if (T(ref T2,ref N2))
                {
                    string T3 = compatibility(T1, T2, op);                                                  //---------------------------------------//
                    if (T3 == null)                                                                        ///////////////////////////////////////////
                    {                                                                                      /////////////////////////////////////////
                        if (T1 == null && T2 != null)                                                              ////////////////////////////////////////
                            Console.WriteLine("Type Mismatch between undefined variable/type" + " and " + T2 + " at " + tokens[i].line_no); ///////////////////////////////////////////
                        else if (T2 == null && T1 != null)                                                   //////////////////////////////////////////
                            Console.WriteLine("Type Mismatch between " + T1 + " and undefined variable/type" + " at " + tokens[i].line_no);     ///////////////////////////////////////////
                        else if (T2 == null && T1 == null)                                                    /////////////////////////////////////////
                            Console.WriteLine("Type Mismatch between undefined variable/type and undefined variable/type" + " at " + tokens[i].line_no);///////////////////////////////////////
                        else                                                                                 //       SEMANTIC WORK                   //
                            Console.WriteLine("Type Mismatch between " + T1 + " and " + T2 + " at " + tokens[i].line_no);                  //--------------------------------------//
                    }
                    string N3 = Createtemp();
                    generate(N3 + "=" + N1 + op + N2);
                    if (Edash(T3, ref T4,N3,ref N4))
                        return true;
                }
            }
            else if (tokens[i].class_part == "Shift" || tokens[i].class_part == "ROP" || tokens[i].class_part == "&" || tokens[i].class_part == "~" || tokens[i].class_part == "|" || tokens[i].class_part == "&&" || tokens[i].class_part == "||" || tokens[i].class_part == "." || tokens[i].class_part == "," || tokens[i].class_part == "times" || tokens[i].class_part == "}" || tokens[i].class_part == ")" || tokens[i].class_part == "]")
            {
                T4 = T1;
                N4 = N1;
                return true;
            }
            return false;
        }

        bool T(ref string T2,ref string N2)
        {
            string T1 = "",N1="";
            if (F(ref T1,ref N1))
                if (Tdash(T1, ref T2,N1,ref N2))
                {
                    return true;
                }


            return false;
        }

        bool Tdash(string T1, ref string T4,string N1,ref string N4)
        {
            string T2 = "",N2="";
            if (tokens[i].class_part == "DivMulMod")
            {
                string op = tokens[i].value_part;
                i++;
                if (F(ref T2,ref N2))
                {
                    string T3 = compatibility(T1, T2, op);                                                  //---------------------------------------//
                    if (T3 == null)                                                                        ///////////////////////////////////////////
                    {                                                                                      /////////////////////////////////////////
                        if (T1 == null && T2 != null)                                                              ////////////////////////////////////////
                            Console.WriteLine("Type Mismatch between undefined variable/type" + " and " + T2 + " at " + tokens[i].line_no); ///////////////////////////////////////////
                        else if (T2 == null && T1 != null)                                                   //////////////////////////////////////////
                            Console.WriteLine("Type Mismatch between " + T1 + " and undefined variable/type" + " at " + tokens[i].line_no);     ///////////////////////////////////////////
                        else if (T2 == null && T1 == null)                                                    /////////////////////////////////////////
                            Console.WriteLine("Type Mismatch between undefined variable/type and undefined variable/type" + " at " + tokens[i].line_no);///////////////////////////////////////
                        else                                                                                 //       SEMANTIC WORK                   //
                            Console.WriteLine("Type Mismatch between " + T1 + " and " + T2 + " at " + tokens[i].line_no);                  //--------------------------------------//
                    }
                    string N3 = Createtemp();
                    generate(N3+"="+N1+op+N2);
                    if (Tdash(T3, ref T4,N3,ref N4))
                        return true;
                }
            }
            else if (tokens[i].class_part == "PlusMinus" || tokens[i].class_part == "Shift" || tokens[i].class_part == "ROP" || tokens[i].class_part == "&" || tokens[i].class_part == "~" || tokens[i].class_part == "|" || tokens[i].class_part == "&&" || tokens[i].class_part == "||" || tokens[i].class_part == "." || tokens[i].class_part == "," || tokens[i].class_part == "times" || tokens[i].class_part == "}" || tokens[i].class_part == ")" || tokens[i].class_part == "]")
            {
                T4 = T1;
                N4 = N1;
                return true;
            }
            return false;
        }


        bool F(ref string RT,ref string ICName)
        {
            string T = "";
            if (tokens[i].class_part == "ID")
            {
                string N = tokens[i].value_part;
                
                i++;
                if (Inclass && Infunc)
                {
                    if (tokens[i].class_part != "(")
                    {
                        T = frflookup(atts, N);
                        if (T == null)
                            T = IcIflookup(InFunction, N, S);
                        if (T == null)
                            Console.WriteLine("Undefined variable " + N + " at " + tokens[i].line_no);
                    }
                }
                else
                {
                    T = dlookup(N, S);
                    if (T == null)
                        Console.WriteLine("Undefined variable " + N + " at " + tokens[i].line_no);
                }
                if (F21(N,T, ref RT,ref ICName))
                    return true;
            }
            else if (tokens[i].class_part == "IncDec")
            {
                string op = tokens[i].value_part;
                i++;
                if (tokens[i].class_part == "ID")
                {
                    string N = tokens[i].value_part;
                    ICName += N;
                    i++;
                    if (Inclass && Infunc)
                    {
                        T = frflookup(atts, N);
                        if (T == null)
                            T = IcIflookup(InFunction, N, S);
                        if (T == null)
                            Console.WriteLine("Undefined variable " + N + " at " + tokens[i].line_no);
                    }
                    else
                    {
                        T = "";
                        T = dlookup(N, S);                                       //--------------------------------------------//
                        if (T == null)                                         //               Semantic work                 //
                            Console.WriteLine("Undefined variable " + N + " at " + tokens[i].line_no);
                    }    //--------------------------------------------//
                    if (F20(op, T, ref RT,ref ICName))
                        return true;

                }

            }

            else if (constt(ref RT,ref ICName))
            {
                return true;
            }
            else if (tokens[i].class_part == "!")
            {
                i++;
                T = "";
                string NT = "";   
                if (F(ref T,ref NT))
                {
                    RT = compatibility(T, "!");
                    if (RT == null)
                        Console.WriteLine("Type Mismatch at " + tokens[i].line_no);
                    string not = Createtemp();
                    generate(not+"=!"+NT);
                    generate(ICName+"="+not);
                    return true;
                }

            }

            else if (tokens[i].class_part == "(")
            {
                i++;
                string NT = "";
                if (OE(ref RT,ref NT))
                {
                    ICName=NT;
                    if (tokens[i].class_part == ")")
                    {
                        i++;
                        return true;

                    }

                }

            }
            return false;
        }

        bool F21(string N1,string T, ref string RT,ref string Icname)
        {
            if (tokens[i].class_part == "[")
            {
                i++;
                string ET = "",NT="";
                if (OE(ref ET,ref NT))
                {
                    if (ET != "integer")
                        Console.WriteLine("Invalid Index at " + tokens[i].line_no);
                    string sz = Createtemp();
                    generate(sz + "=" + NT + "*" + "size of " + N1);
                    Icname += N1 + "[" + sz + "]";
                    if (tokens[i].class_part == "]")
                    {
                        i++;
                        if (F22(T, ref RT,ref Icname))
                        {
                            return true;
                        }
                    }
                }
            }

            else if (tokens[i].class_part == "." && tokens[i + 1].class_part == "ID")
            {
                i++;
                Icname += N1 + ".";
                if (tokens[i].class_part == "ID")
                {
                    string N = tokens[i].value_part;
                    Icname += N;
                    if (tokens[i + 1].class_part != "(")
                    {
                        T = clfieldlookup(T, N);
                        if (T == null)
                            Console.WriteLine("Variable " + N + " is undefined or not visible at " + tokens[i].line_no);
                    }
                    i++;

                    if (F12(N, T, ref RT,ref Icname))
                        return true;

                }


            }
            else if (F1(N1, T, ref RT,ref Icname))
                return true;
            return false;
        }

        bool F22(string T, ref string RT,ref string Icname)
        {
            if (tokens[i].class_part == "." && tokens[i + 1].class_part == "ID")
            {
                i++;
                Icname += ".";
                if (tokens[i].class_part == "ID")
                {
                    string N = tokens[i].value_part;
                    Icname += N;
                    if (tokens[i + 1].class_part != "(")
                    {
                        T = clfieldlookup(T, N);
                        if (T == null)
                            Console.WriteLine("Variable " + N + " is undefined or not visible at " + tokens[i].line_no);
                    }
                    i++;
                    if (F12(N, T, ref RT,ref Icname))
                        return true;
                }

            }
            if (F2(null, T, ref RT,ref Icname))
                return true;

            return false;
        }

        bool F20(string op, string T, ref string RT,ref string Icname)
        {
            if (tokens[i].class_part == "[")
            {
                i++;
                string ET = "",NT="";
                if (OE(ref ET,ref NT))
                {
                    if (ET != "integer")
                        Console.WriteLine("Invalid Index at " + tokens[i].line_no);
                    string sz = Createtemp();
                    generate(sz + "=" + NT + "*" + "size of " + Icname);
                    Icname +="[" + sz + "]";
                    if (tokens[i].class_part == "]")
                    {
                        i++;
                        if (F201(op, T, ref RT,ref Icname))
                            return true;
                    }
                }
            }
            else if (tokens[i].class_part == "." || tokens[i + 1].class_part == "ID")
            {
                i++;
                if (tokens[i].class_part == "ID")
                {
                    string N = tokens[i].value_part;
                    Icname +="."+N;
                    T = clfieldlookup(T, N);
                    if (T == null)
                        Console.WriteLine("Variable " + N + " is undefined or not visible at " + tokens[i].line_no);
                    i++;
                    if (F7(op, T, ref RT,ref Icname))
                        return true;
                }
            }
            else if (tokens[i].class_part == "DivMulMod" || tokens[i].class_part == "PlusMinus" || tokens[i].class_part == "Shift" || tokens[i].class_part == "ROP" || tokens[i].class_part == "&" || tokens[i].class_part == "~" || tokens[i].class_part == "|" || tokens[i].class_part == "&&" || tokens[i].class_part == "||" || tokens[i].class_part == "." || tokens[i].class_part == "," || tokens[i].class_part == "times" || tokens[i].class_part == "}" || tokens[i].class_part == ")" || tokens[i].class_part == "]")
            {
                RT = compatibility(T, op);
                if (RT == null)
                    Console.WriteLine("Type Mismatch at " + tokens[i].line_no);
                string id = Createtemp();
                generate(id + "=" + Icname + op);
                generate(Icname + "=" + id);
                return true;
            }
            return false;
        }

        bool F201(string op, string T, ref string RT,ref string Icname)
        {
            if (tokens[i].class_part == "." && tokens[i + 1].class_part == "ID")
            {
                i++;
                if (tokens[i].class_part == "ID")
                {
                    string N = tokens[i].value_part;
                    Icname += "." + N;
                    T = clfieldlookup(T, N);
                    if (T == null)
                        Console.WriteLine("Variable " + N + " is undefined or not visible at " + tokens[i].line_no);
                    i++;
                    if (F7(op, T, ref RT,ref Icname))
                        return true;
                }
            }
            else if (tokens[i].class_part == "DivMulMod" || tokens[i].class_part == "PlusMinus" || tokens[i].class_part == "Shift" || tokens[i].class_part == "ROP" || tokens[i].class_part == "&" || tokens[i].class_part == "~" || tokens[i].class_part == "|" || tokens[i].class_part == "&&" || tokens[i].class_part == "||" || tokens[i].class_part == "." || tokens[i].class_part == "," || tokens[i].class_part == "times" || tokens[i].class_part == "}" || tokens[i].class_part == ")" || tokens[i].class_part == "]")
            {
                RT = compatibility(T, op);
                if (RT == null)
                    Console.WriteLine("Type Mismatch at " + tokens[i].line_no);
                string id = Createtemp();
                generate(id + "=" + Icname + op);
                generate(Icname + "=" + id);
                return true;
            }
            return false;
        }

        bool F12(string N, string T, ref string RT,ref string Icname)
        {

            if (tokens[i].class_part == "[")
            {
                i++;
                string ET = "",NT="";
                if (OE(ref ET,ref  NT))
                {
                    if (ET != "integer")
                        Console.WriteLine("Invalid Index at " + tokens[i].line_no);
                    string sz = Createtemp();
                    generate(sz + "=" + NT + "*" + "size of " + N);
                    Icname +="[" + sz + "]";
                    if (tokens[i].class_part == "]")
                    {
                        i++;
                        if (F2(N, T, ref RT,ref Icname))
                            return true;
                    }
                }
            }
            else if (F1(N, T, ref RT,ref Icname))
                return true;

            return false;
        }

        bool F7(string op, string T, ref string RT,ref string Icname)
        {
            if (tokens[i].class_part == "[")
            {
                i++;
                string ET = "",NT="";
                if (OE(ref ET,ref NT))
                {
                    if (ET != "integer")
                        Console.WriteLine("Type Mismatch at " + tokens[i].line_no);
                    string sz = Createtemp();
                    generate(sz + "=" + NT + "*" + "size of " + Icname);
                    Icname +="[" + sz + "]";
                    if (tokens[i].class_part == "]")
                    {
                        RT = compatibility(T, op);
                        if (RT == null)
                            Console.WriteLine("Type Mismatch at " + tokens[i].line_no);
                        string id = Createtemp();
                        generate(id + "=" + Icname + op);
                        generate(Icname + "=" + id);
                        i++;
                        return true;
                    }
                }
            }

            else if (tokens[i].class_part == "DivMulMod" || tokens[i].class_part == "PlusMinus" || tokens[i].class_part == "Shift" || tokens[i].class_part == "ROP" || tokens[i].class_part == "&" || tokens[i].class_part == "~" || tokens[i].class_part == "|" || tokens[i].class_part == "&&" || tokens[i].class_part == "||" || tokens[i].class_part == "." || tokens[i].class_part == "," || tokens[i].class_part == "times" || tokens[i].class_part == "}" || tokens[i].class_part == ")" || tokens[i].class_part == "]")
            {
                RT = compatibility(T, op);
                if (RT == null)
                    Console.WriteLine("Type Mismatch at " + tokens[i].line_no);
                string id = Createtemp();
                generate(id + "=" + Icname + op);
                generate(Icname + "=" + id);
                return true;
            }

            return false;
        }

        bool F2(string N, string T, ref string RT,ref string Icname)
        {
            if (tokens[i].class_part == "IncDec")
            {
                string op = tokens[i].value_part;

                RT = compatibility(T, op);
                if (RT == null)
                    Console.WriteLine("Type Mismatch at " + tokens[i].line_no);
                string id = Createtemp();
                generate(id + "=" + Icname + tokens[i].value_part);
                generate(Icname+"="+id);
                i++;
                return true;
            }
            else if (tokens[i].class_part == "DivMulMod" || tokens[i].class_part == "PlusMinus" || tokens[i].class_part == "Shift" || tokens[i].class_part == "ROP" || tokens[i].class_part == "&" || tokens[i].class_part == "~" || tokens[i].class_part == "|" || tokens[i].class_part == "&&" || tokens[i].class_part == "||" || tokens[i].class_part == "." || tokens[i].class_part == "," || tokens[i].class_part == "times" || tokens[i].class_part == "}" || tokens[i].class_part == ")" || tokens[i].class_part == "]")
            {
                RT = T;
                return true;
            }

            return false;
        }

        bool F1(string N, string T, ref string RT,ref string Icname)
        {
            if (tokens[i].class_part == "IncDec")
            {
                string op = tokens[i].value_part;
                RT = compatibility(T, op);
                string incdec = Createtemp();
                generate(incdec + "=" + N + tokens[i].value_part);
                generate(N + "=" + incdec);
                Icname += N;
                if (RT == null)
                    Console.WriteLine("Type Mismatch at " + tokens[i].line_no);
                i++;
                return true;
            }
            else if (tokens[i].class_part == "(")
            {
                i++;
                string TE = "";
                if (param(ref TE))
                {
                  
                        RT = clmethodlookup(N, T, TE);                                                                                           //----------------------------------------//
                        if (RT == null)                                                                                                         //            Semantic                    //
                            Console.WriteLine("No such method " + N + "(" + TE + ") or it is invisible at " + tokens[i].line_no);              //----------------work--------------------//
                    
                    if (tokens[i].class_part == ")")
                    {
                        i++;
                        string fcall = Createtemp();
                        if (TE != "")
                        {
                            string[] noargs = TE.Split(',');
                            generate(fcall + "=call " + N + "_" + TE + "," + noargs.Length);
                        }
                        else
                            generate(fcall + "=call " + N + "_" + "void" + "," + 0);
                        Icname += fcall;
                        return true;
                    }
                }
            }
            else if (tokens[i].class_part == "DivMulMod" || tokens[i].class_part == "PlusMinus" || tokens[i].class_part == "Shift" || tokens[i].class_part == "ROP" || tokens[i].class_part == "&" || tokens[i].class_part == "~" || tokens[i].class_part == "|" || tokens[i].class_part == "&&" || tokens[i].class_part == "||" || tokens[i].class_part == "." || tokens[i].class_part == "," || tokens[i].class_part == "times" || tokens[i].class_part == "}" || tokens[i].class_part == ")" || tokens[i].class_part == "]")
            {
                RT = T;
                Icname = N;
                return true;
            }

            return false;
        }
        bool constt(ref string RT,ref string Icname)
        {
            if (tokens[i].class_part == "integer" || tokens[i].class_part == "decimal" || tokens[i].class_part == "char" || tokens[i].class_part == "word")
            {
                RT = tokens[i].class_part;
                Icname = tokens[i].value_part;
                i++;
                return true;
            }
            return false;
        }



        bool enum_st(ref List<SymbolTable3> att)
        {
            if (tokens[i].class_part == "enum")
            {

                i++;

                if (tokens[i].class_part == "ID")
                {
                    string N = tokens[i].value_part;
                    i++;
                    if (Iclookup(att, N) == null)                                                        //--------------------------------//
                    {                                                                                   //                                 //
                        SymbolTable3 temp = new SymbolTable3(N, "enum", "public", "nonstatic");        //                                   //
                        att.Add(temp);                                                                 //                                    //
                        //                                    //
                    }                                                                                 //                                    //
                    //                                    //
                    else                                                                              //       Semantic Work                //
                        Console.WriteLine("Recdalaration of enum " + N + " at " + tokens[i].line_no);    //-------------------------------------//

                    if (tokens[i].class_part == "{")
                    {
                        i++;

                        if (enum_body())
                        {

                            if (tokens[i].class_part == "}")
                            {
                                i++;

                                if (tokens[i].class_part == ".")
                                {
                                    i++;
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        bool enum_body()
        {

            if (tokens[i].class_part == "ID")
            {

                i++;
                if (enum_body1())
                {
                    return true;

                }
            }

            else if (tokens[i].class_part == "}")
            {
                return true;

            }
            return false;
        }

        bool enum_body1()
        {

            if (tokens[i].class_part == ",")
            {
                i++;
                if (tokens[i].class_part == "ID")
                {

                    i++;

                    if (enum_body1())
                    {
                        return true;

                    }
                }
            }

            else if (tokens[i].class_part == "}")
            {
                return true;
            }
            return false;
        }
        bool Body()
        {
            if (tokens[i].class_part == "{")
            {
                if (Infunc != true)
                    createScope();
                i++;
                if (multiple_st())
                {
                    if (tokens[i].class_part == "}")
                    {
                        destroyScope();
                        i++;
                        return true;
                    }

                }
            }
            return false;
        }




        bool multiple_st()
        {
            if (tokens[i].class_part == "Select" || tokens[i].class_part == "Repeat" || tokens[i].class_part == "ID" || tokens[i].class_part == "break" || tokens[i].class_part == "continue" || tokens[i].class_part == "returns" || tokens[i].class_part == "Access_Modifier" || tokens[i].class_part == "static" || tokens[i].class_part == "DataType")
            {
                if (single_st())
                {
                    if (multiple_st())
                        return true;
                }
            }
            else if (tokens[i].class_part == "}" || tokens[i].class_part == "breaks")
                return true;
            return false;
        }




        bool single_st()
        {
            if (brk())
                return true;
            else if (cont())
                return true;

            else if (ret())
                return true;
            else if (tokens[i].class_part == "Repeat")
            {
                string NL1 = Createlabel();
                generate(NL1+":");
                i++;
                if (tokens[i].class_part == "(")
                {
                    i++;
                    if (single_st2(NL1))
                    {
                        return true;

                    }

                }

            }
            else if (tokens[i].class_part == "Select")
            {

                i++;
                if (tokens[i].class_part == "(")
                {
                    i++;
                    if (single_st1())
                        return true;

                }
            }

            else if (ArrayDec_Dec())
                return true;


            else if (tokens[i].class_part == "ID")
            {
                string N = tokens[i].value_part;
                i++;
                if (SS3(N))
                    return true;
            }

            return false;
        }

        bool SS3(string N)
        {
            
           
            if (tokens[i].class_part == "(")
            {
                i++;
                string RT = "";
                if (param(ref RT))
                {
                    if (Inclass && Infunc)
                    {
                        if(Icforfunclookup(atts, N, RT)==null)
                     
                            Console.WriteLine("Undefined Function at "+tokens[i].line_no);
                    }
                    else
                        Console.WriteLine("Undefined Function at " + tokens[i].line_no);
                    string fcall = Createtemp();
                    if (RT != "")
                    {


                        string[] noargs = RT.Split(',');

                        generate(fcall + "=call " + N + "_" + RT + "," + noargs.Length);
                    }
                    else
                        generate(fcall + "=call " + N + "_" + RT + "," + 0);
                    if (tokens[i].class_part == ")")
                    {
                        i++;
                        if (tokens[i].class_part == ".")
                        {
                            i++;
                            return true;
                        }
                    }
                }
            }

            else if (tokens[i].class_part == "ID")
            {
                string N1 = tokens[i].value_part;
                string T = "";
                if (clookup(N) == null)
                    Console.WriteLine("Undefined class at " + tokens[i].line_no);
                else
                    T = N;
                if (Inclass && Infunc)
                { 
                if(IcIflookup(InFunction,N1,S)==null)
                    {
                        SymbolTable1 t = new SymbolTable1(N1, T, stack);
                        InFunction.Add(t);
                }
                else
                    Console.WriteLine("Redeclaration of variable " + N1 + " at " + tokens[i].line_no);
                }
                else
                {
                    if (dlookup(N1, S) == null)
                        Insert(N1, T, stack);
                    else
                        Console.WriteLine("Redeclaration of variable " + N1 + " at " + tokens[i].line_no);
                }
                i++;
                if (isOArr())
                {
                    if (ObjCreate5(N, T))
                        return true;
                }
            }

            else if (tokens[i].class_part == ".")
            {

                string T = "";
                if (Infunc && Inclass)
                {
                    if (tokens[i+1].class_part != "(")
                    {

                        T = frflookup(atts, N);
                        if (T == null)
                            T = IcIflookup(InFunction, N, S);
                        if (T == null)
                            Console.WriteLine("Undefined variable " + N + " at " + tokens[i].line_no);
                    }
                }
                else
                {
                    T = dlookup(N, S);
                    if (T == null)
                        Console.WriteLine("Undefined variable " + N + " at " + tokens[i].line_no);
                }
                i++;
                if (tokens[i].class_part == "ID")
                {
                    string N1 = tokens[i].value_part;
                    string Icname = N + "." + N1;
                    i++;
                    if (tokens[i].class_part != "(")
                    {
                        T = clfieldlookup(T, N1);
                        if (T == null)
                        {
                            Console.WriteLine("Variable "+N1+" of class "+T+" is undefined or invisible at "+tokens[i].line_no);
                        }
                    }
                    if (SS1(N1,T,Icname))
                        return true;
                }
            }

            else if (tokens[i].class_part == "[")
            {

                string T = "";
                if (Infunc && Inclass)
                {
                    if (tokens[i+1].class_part != "(")
                    {

                        T = frflookup(atts, N);
                        if (T == null)
                            T = IcIflookup(InFunction, N, S);
                        if (T == null)
                            Console.WriteLine("Undefined variable " + N + " at " + tokens[i].line_no);
                    }
                }
                else
                {
                    T = dlookup(N, S);
                    if (T == null)
                        Console.WriteLine("Undefined variable " + N + " at " + tokens[i].line_no);
                }
                i++;
                string RT = "",NT="";
                if (OE(ref RT,ref NT))
                {
                    if(RT!="integer")
                        Console.WriteLine("Invalid Index at "+tokens[i].line_no);
                    Console.WriteLine("Invalid Index at " + tokens[i].line_no);
                    string sz = Createtemp();
                    generate(sz + "=" + NT + "*" + "size of " + N);
                   string Icname = N + "[" + sz + "]";

                    if (tokens[i].class_part == "]")
                    {
                        i++;
                        if (SS2(T,Icname))
                            return true;
                    }

                }
            }

            else if (tokens[i].class_part == "IncDec" || tokens[i].class_part == "=" || tokens[i].class_part == "AOP") 
            {
                string T = "";
                if (Infunc && Inclass)
                {
                    if (tokens[i].class_part != "(")
                    {
                       
                        T = frflookup(atts, N);
                        if (T == null)
                            T = IcIflookup(InFunction, N, S);
                        if (T == null)
                            Console.WriteLine("Undefined variable " + N + " at " + tokens[i].line_no);
                    }
                }
                else
                {
                    T = dlookup(N, S);
                    if (T == null)
                        Console.WriteLine("Undefined variable " + N + " at " + tokens[i].line_no);
                }
                if (ASSIGN1(T,N))
                {
                  
                    if (tokens[i].class_part == ".")
                    {
                        i++;
                        return true;
                    }
                }

            }

            return false;
        }

        bool SS1(string N1,string T,string Icname)
        {
           
            if (tokens[i].class_part == "(")
            {
                i++;
                string RT = "";
                if (param(ref RT))
                {
                    T = clmethodlookup(N1, T, RT);
                    if(T==null)
                    { Console.WriteLine("Method " + N1+"("+RT+")"+ " of class " + T + " is undefined or invisible at " + tokens[i].line_no); }
                    string fcall = Createtemp();
                    if (RT != "")
                    {
                        string[] noargs = RT.Split(',');
                        generate(fcall + "=call " + Icname + "_" + RT + "," + noargs.Length);
                    }
                    else
                        generate(fcall + "=call " + Icname + "_" + RT + "," + 0);
                    if (tokens[i].class_part == ")")
                    {
                        i++;
                        if (tokens[i].class_part == ".")
                        {
                            i++;
                            return true;
                        }
                    }
                }
            }
            else if (LO1(ref Icname))
            {

                if (ASSIGN1(T,Icname))
                {
                    if (tokens[i].class_part == ".")
                    {
                        i++;
                        return true;
                    }
                }
            }


            return false;
        }




        bool ASSIGN1(string T,string Icname)
        {
            string opr = "";
            if (tokens[i].class_part == "IncDec")
            {
                string op = tokens[i].value_part;
                if(compatibility(T,op)==null)
                    Console.WriteLine("Type Mismatch at "+tokens[i].line_no);
                string id = Createtemp();
                generate(id + "=" + Icname + tokens[i].value_part);
                generate(Icname + "=" + id);
                i++;
                return true;
            }



            else if (_ASG(ref opr))
            {
                
                string RT = "",NT="";
                if (OE(ref RT,ref NT))
                {
                    if(compatibility(T,RT,opr)==null)
                        Console.WriteLine("Type Mismatch at "+tokens[i].line_no);
                    if (opr.Length == 1)
                    {
                        generate(Icname + "=" + NT);
                    }
                    else
                    {
                        string asg2 = Createtemp();
                        generate(asg2 + "=" + Icname + opr[0] + NT);
                        generate(Icname + "=" + asg2);
                    }
                    return true;
                }
            }

            return false;
        }



        bool _ASG(ref string op)
        {
            if (tokens[i].class_part == "=" || tokens[i].class_part == "AOP")
            {
                op = tokens[i].value_part;
                i++; 
                return true;
            }

            return false;
        }

        bool SS2(string T,string Icname)
        {
           
            if (tokens[i].class_part == ".")
            {
                i++;
                if (tokens[i].class_part == "ID")
                {
                    string N1 = tokens[i].value_part;
                    Icname +="."+ N1;
                    i++;
                    if (tokens[i].class_part != "(")
                    {
                        T = clfieldlookup(T, N1);
                        if(T==null)
                        { Console.WriteLine("Variable " + N1 + " of class " + T + " is undefined or invisible at " + tokens[i].line_no); }
                    }
                    
                    if (SS4(N1,T,Icname))
                        return true;
                }
            }
            else if (ASSIGN1(T,Icname))
            {
                if (tokens[i].class_part == ".")
                {
                    i++;
                    return true;
                }

            }

            return false;
        }

        bool SS4(string N1,string T,string Icname)
        {
           
            if (tokens[i].class_part == "(")
            {
                i++;
                string RT = "";
                if (param(ref RT))
                {
                    T = clmethodlookup(N1, T, RT);
                    if(T==null)
                    { Console.WriteLine("Method " + N1 + "(" + RT + ")" + " of class " + T + " is undefined or invisible at " + tokens[i].line_no); }
                    string fcall = Createtemp();
                    if (RT != "")
                    {
                        string[] noargs = RT.Split(',');
                        generate(fcall + "=call " + Icname + "_" + RT + "," + noargs.Length);
                    }
                    else
                        generate(fcall + "=call " + Icname + "_" + RT + "," + 0);
                    if (tokens[i].class_part == ")")
                    {
                        i++;
                        if (tokens[i].class_part == ".")
                        {
                            i++;
                            return true;
                        }
                    }

                }
            }

            else if (LO1(ref Icname))
            {
                if (ASSIGN1(T,Icname))
                {
                    if (tokens[i].class_part == ".")
                    {
                        i++;
                        return true;
                    }
                }


            }

            return false;
        }

        bool LO1(ref string Icname)
        {
            if (tokens[i].class_part == "[")
            {
                i++;
                string RT = "",NT="";
                if (OE(ref RT,ref NT))
                {
                    if (tokens[i].class_part == "]")
                    {
                        i++;
                        Console.WriteLine("Invalid Index at " + tokens[i].line_no);
                        string sz = Createtemp();
                        generate(sz + "=" + NT + "*" + "size of " + Icname);
                        Icname += "[" + sz + "]";
                        return true;
                    }
                }
            }
            else if (tokens[i].class_part == "=" || tokens[i].class_part == "AOP")
            {
                return true;
            }
            
            return false;
        }

        bool single_st1()
        {
            string RT = "",NT="";
            if (OE(ref RT,ref NT))
            {
                string exit = Createlabel();
                if (RT != "integer")                                                       //SEmantic
                    Console.WriteLine("Type Mismatch at " + tokens[i].line_no);              // work
                if (tokens[i].class_part == ")")
                {
                    i++;
                    if (tokens[i].class_part == "{")
                    {
                        createScope();
                        i++;
                        if (select_body(NT,exit))
                            if (Default())
                            {
                                if (tokens[i].class_part == "}")
                                {
                                    generate(exit + ":");
                                    destroyScope();
                                    i++;
                                    return true;

                                }

                            }

                    }

                }


            }

            else if (tokens[i].class_part == "if")
            {
                i++;
                if (tokens[i].class_part == "(")
                {
                    i++;
                   
                    if (OE(ref RT,ref NT))
                    {
                        if (RT != "bool")                                                       //SEmantic
                            Console.WriteLine("Type Mismatch at " + tokens[i].line_no);              // work
                        if (tokens[i].class_part == ")")
                        {
                            string NL1 = Createlabel();
                            generate("if(" + NT + "==false)jmp " + NL1);
                            i++;
                            if (tokens[i].class_part == ")")
                            {
                                i++;
                                if (Body())
                                    if (elseOelse(NL1))
                                        return true;


                            }

                        }
                    }

                }

            }


            return false;
        }


        bool elseOelse(string NL1)
        {
            if (tokens[i].class_part == "Select")
            {
                i++;
                if (tokens[i].class_part == "(")
                {
                    i++;
                    if (tokens[i].class_part == "else")
                    {
                        i++;
                        if (elseOelse1(NL1))
                            return true;

                    }


                }


            }

            else if (tokens[i].class_part == "}")
            {
                generate(NL1 + ":");
                return true;
            }

            return false;
        }


        bool elseOelse1(string NL1)
        {
            if (tokens[i].class_part == "if")
            {
                
                i++;
                if (tokens[i].class_part == "(")
                {
                    i++;
                    string RT = "", NT = "" ;
                    if (OE(ref RT,ref NT))
                    {
                        if (RT != "bool")                                                       //SEmantic
                            Console.WriteLine("Type Mismatch at " + tokens[i].line_no);              // work
                        if (tokens[i].class_part == ")")
                        {
                            i++;
                            if (tokens[i].class_part == ")")
                            {
                                
                                generate("if(" + NT + "==false)jmp " + NL1);
                                i++;
                                if (Body())
                                {
                                    if (elseOelse(NL1))
                                        return true;
                                }

                            }


                        }

                    }

                }


            }

            else if (tokens[i].class_part == ")")
            {
                string NL2 = Createlabel();
                generate("jmp " + NL2);
                generate(NL1+":");
                i++;
                if (Body())
                {
                    generate(NL2 + ":");
                    return true;
                }
            }


            return false;
        }

        bool select_body(string NT,string exit)
        {
            if (tokens[i].class_part == "Option")
            {
                i++;
                if (tokens[i].class_part == "integer")
                {
                    string T1=Createtemp();
                    generate(T1 + "=" + NT + "==" + tokens[i].value_part);
                    string Next = Createlabel();
                    generate("if(" + T1 + "==false)jmp " + Next);
                    i++;
                    if (tokens[i].class_part == ":")
                    {
                        i++;

                        if (multiple_st())
                        {
                            if (tokens[i].class_part == "breaks")
                            {
                               
                                i++;
                                if (tokens[i].class_part == ".")
                                {
                                    generate("jmp " + exit);
                                    generate(Next + ":");
                                    i++;
                                    if (select_body(NT,exit))
                                        return true;
                                }

                            }
                        }

                    }


                }

            }

            else if (tokens[i].class_part == "DefaultOption" || tokens[i].class_part == "}")
                return true;


            return false;
        }


        bool Bodys()
        {
            if (single_st())
            {
                if (Bodys())
                    return true;
            }
            else if (tokens[i].class_part == "breaks" || tokens[i].class_part == "}")
                return true;

            return false;
        }

        bool Default()
        {
            if (tokens[i].class_part == "DefaultOption")
            {
                i++;
                if (tokens[i].class_part == ":")
                {
                    i++;
                    if (multiple_st())
                        return true;

                }

            }
            else if (tokens[i].class_part == "}")
                return true;

            return false;
        }

        bool single_st2(string NL1)
        {
            string RT = "",NT="";
            if (tokens[i].class_part == "while")
            {
                i++;

                if (OE(ref RT,ref NT))
                {
                    if (RT != "bool")                                                            ///  Semantic
                        Console.WriteLine("Type Mismatch at " + tokens[i].line_no);            //////     work
                    string NL2 = Createlabel();
                    generate("if(" + NT + "==false)jmp " + NL2);
                    if (tokens[i].class_part == ")")
                    {
                        i++;
                        if (Body())
                        {
                            generate("jmp " + NL1);
                            generate(NL2 + ":");
                            return true; }
                    }

                }

            }


            else if (OE(ref RT,ref NT))
            {
                string T1 = Createtemp();
                generate(T1 + "=0");
                string T2 = Createtemp();
                generate(T2 + "=" + T1 + "==" + NT);
                 string NL2 = Createlabel();
                 generate("if(" + T2 + "==true)jmp " + NL2);
                if (RT != "integer")                                                      ///  Semantic
                    Console.WriteLine("Type Mismatch at " + tokens[i].line_no);            //////     work

                if (tokens[i].class_part == "times")
                {
                    i++;

                    if (tokens[i].class_part == ")")
                    {
                        i++;
                        if (Body())
                        {
                            generate(T1 + "=" + T1 + "+1");
                            generate("jmp " + NL1);
                            generate(NL2 + ":");
                            return true; }
                    }
                }

            }

            return false;
        }



    }
}



//Syntax endssss here