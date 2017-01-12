using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;

namespace Lexical_Analyzer
{

    class Program
    {
      static string temp;
      static int line_no = 1;
      static string token_set;
      static  char input;
      public static Token[] SyntaxIn;
      static   StreamReader sr = new StreamReader(@"..\Code_Editor.txt");
      static bool lang_part = false;
      
        static void Main(string[] args)
        {
           
            keywords keywords1 = new keywords();
            keywords1.Load();
            Operators operators = new Operators();
            operators.Load();
            CharacterDfa Char = new CharacterDfa();
            StringDfa STring = new StringDfa();
            input = (char)sr.Read();
           
            while(true)
            {
                
                temp = null;
                if ((input >= 'A' && input <= 'Z') || (input >= 'a' && input <= 'z') || input == '_')      //Keyword or Identifier
                {
                    lang_part = true;
                    while(word_breakers(input)==true)
                    {
                        if (sr.EndOfStream)
                        {
                            temp += input;
                            break;
                        }
                        temp += input;
                        input = (char)sr.Read();
                       
                    } 

                  string temp_set = keywords1.check(temp, line_no);
                    if(temp_set!=null)
                    { 
                    token_set += temp_set+"\n";
                  
                    }
                    else
                    { temp_set = check_identifier(temp);
                    if (temp_set != null)
                    {
                        token_set += temp_set + "\n";
                     
                    }
                    else
                    { token_set += "(" + temp + "," + "Invalid Lexeme" + "," + line_no + ")" + "\n";  }
                    }

                    

                }


                 

                 if (input == '\"')           //String constant
                {
                    lang_part = true;
                    temp += input;
                  while(true)
                  {
                      input = (char)sr.Read();
                    
                      
                      if(input=='\\'&&sr.Peek()=='\"')
                      {
                          temp += input;
                          input = (char)sr.Read();
                          temp += input;
                        
                      }
                      else if (input == '\"')
                      {
                          temp += input;
                          break;
                      }
                      else
                      {
                          temp += input;

                      }

                      if (input=='\r')
                          break;

                    
                 }
                 
                    string temp1 = STring.dfa(temp, line_no);
                    if (temp1 != null)
                    { token_set += temp1 + "\n";  }
                    else
                    { 
                        token_set += "(" + temp + "," + "Invalid Lexeme" + "," + line_no + ")" + "\n";
                        continue;
                    
                    }


                }
              
                 if(input>='0'&&input<='9')                   //Integer or decimal constant
                {
                    string iftemp=null;
                    lang_part = true;
                    while (word_breakers(input) == true)
                    {
                        if (sr.EndOfStream)
                            break;
                       
                        iftemp += input;
                        input = (char)sr.Read();
                        if(input=='.'&&(sr.Peek()>='0'&&sr.Peek()<='9'))
                        { 
                         iftemp += '.';
                         input = (char)sr.Read();
                        while (word_breakers(input) == true)
                        {
                            if (sr.EndOfStream)
                            {
                                iftemp += input;
                                break;
                            }
                            iftemp += input;
                            input = (char)sr.Read();
                            if(input=='+'||input=='-')
                            { iftemp += input;
                            input = (char)sr.Read();
                            continue;
                            }
                        } 

                        break;
                        }
                    }
                    bool point = false;
                   if(iftemp[iftemp.Length-1]=='.')
                   {
                      iftemp=iftemp.Remove(iftemp.Length - 1, 1);
                      point = true;
                   }


                   if (iftemp.Contains('.'))
                   {
                       string float1 = check_decimal(iftemp);
                       if (float1 != null)
                       {
                           token_set += float1 + "\n";

                       }
                       else
                       {

                           token_set += "(" + iftemp + "," + "Invalid Lexeme" + "," + line_no + ")" + "\n";
                       }
                   }
                   else
                   {
                       string int1 = check_integer(iftemp);
                       if (int1 != null)
                       {
                           token_set += int1 + "\n";
                           if (point == true)
                               token_set += "(" + '.' + "," + "," + line_no + ")" + "\n";

                       }
                       else
                       {

                           token_set += "(" + iftemp + "," + "Invalid Lexeme" + "," + line_no + ")" + "\n";
                       }

                   }
                }

                if (input == '{' || input == '}' || input == '(' || input == ')' || input == '[' || input == ']' || input == ',' || input == ':' || input == '.')    //Punctuators
                {
                    lang_part = true;
                    token_set +="(" + input + ",,"  + line_no + ")"+"\n";
                }


                if(input=='+'||input=='-'||input=='*'||input=='%'||input=='/'||input=='!'||input=='<'||input=='>'||input=='='||input=='|'||input=='&')            //operators
                {
                    lang_part = true;
                    string optemp=null;
                   if(input=='=')
                   {
                    if ((input == '=' && sr.Peek() == '='))
                        {
                            optemp += input;
                            input = (char)sr.Read();
                            optemp += input;
                            token_set += operators.check(optemp, line_no) + "\n";
                        }
                    else
                    {
                        optemp += input;
                        token_set += operators.check(optemp, line_no) + "\n";
                    }
                   }

                    else if(input=='+'||input=='-'||input=='/'||input=='*'||input=='%')
                    {
                    if(input=='+'||input=='-')
                    {
                        if((input=='+'&&sr.Peek()=='+')||(input=='+'&&sr.Peek()=='='))
                        { optemp += input;
                        input = (char)sr.Read();
                        optemp += input;
                        token_set += operators.check(optemp, line_no) + "\n";
                        }
                        else if ((input == '-' && sr.Peek() == '-') || (input == '-' && sr.Peek() == '='))
                        {
                            optemp += input;
                            input = (char)sr.Read();
                            optemp += input;
                            token_set += operators.check(optemp, line_no) + "\n";
                        }
                        else
                        { optemp += input;
                        token_set += operators.check(optemp, line_no) + "\n";
                        }

                    }
                    else if(input=='*'||input=='/'||input=='%')
                    {
                      if (input == '*' && sr.Peek() == '=')
                        {
                            optemp += input;
                            input = (char)sr.Read();
                            optemp += input;
                            token_set += operators.check(optemp, line_no) + "\n";
                        }
                      else if (input == '/' && sr.Peek() == '=')
                      {
                          optemp += input;
                          input = (char)sr.Read();
                          optemp += input;
                          token_set += operators.check(optemp, line_no) + "\n";
                      }
                      else if (input == '%' && sr.Peek() == '=')
                      {
                          optemp += input;
                          input = (char)sr.Read();
                          optemp += input;
                          token_set += operators.check(optemp, line_no) + "\n";
                      }
                         else
                        { optemp += input;
                        token_set += operators.check(optemp, line_no) + "\n";
                        }
                    }
                    
                    }

                   else  if(input=='&'||input=='|')
                    {
                        if ((input == '&' && sr.Peek() == '&') )
                        {
                            optemp += input;
                            input = (char)sr.Read();
                            optemp += input;
                            token_set += operators.check(optemp, line_no) + "\n";
                        }
                        else if ((input == '|' && sr.Peek() == '|'))
                        {
                            optemp += input;
                            input = (char)sr.Read();
                            optemp += input;
                            token_set += operators.check(optemp, line_no) + "\n";
                        }
                        else
                        { optemp += input;
                        token_set += operators.check(optemp, line_no) + "\n";
                        }


                    }

                   else  if(input=='<'||input=='>'||input=='!')
                    {
                   if(input=='<')
                   {
                       if(sr.Peek()=='='||sr.Peek()=='<')
                       {
                           optemp += input;
                           input = (char)sr.Read();
                           optemp += input;
                           token_set += operators.check(optemp, line_no) + "\n";
                       }
                       else
                       {
                           optemp += input;
                           token_set += operators.check(optemp, line_no) + "\n";
                       }

                   }

                   else  if (input == '>')
                   {
                       if (sr.Peek() == '=' || sr.Peek() == '>')
                       {
                           optemp += input;
                           input = (char)sr.Read();
                           optemp += input;
                           token_set += operators.check(optemp, line_no) + "\n";
                       }
                       else
                       {
                           optemp += input;
                           token_set += operators.check(optemp, line_no) + "\n";
                       }

                   }
                   else if(input=='!')
                   {
                       if (sr.Peek() == '=')
                       {
                           optemp += input;
                           input = (char)sr.Read();
                           optemp += input;
                           token_set += operators.check(optemp, line_no) + "\n";
                       }
                       else
                       {
                           optemp += input;
                           token_set += operators.check(optemp, line_no) + "\n";
                       }
                   }

                   
                   }

                }
             

                 if(input=='\'')                  //Char Constant
                {
                    lang_part = true;
                    temp += input;
                    input = (char)sr.Read();
                    if (input != '\\')
                    { temp += input;
                    input = (char)sr.Read();
                    temp += input;
                    
                    }
                    else
                    {
                        temp += input;
                        input = (char)sr.Read();
                        temp += input;
                        input = (char)sr.Read();
                        temp += input;
                       input = (char)sr.Read();
                    }
                string temp1=Char.dfa(temp, line_no);
                 if (temp1 != null)
                 {
                     token_set += temp1 + "\n";
              
                 }
                 else
                 { token_set += "(" + temp + "," + "Invalid Lexeme" + "," + line_no + ")" + "\n";}

                }


               
             
                 if (input=='\r')                  //Line Change
                {
                    lang_part = true;
                    line_no++;
                    
                }

               
                if(input==';')                        //Single Line Comment
            {
                lang_part = true;
                while(input!='\r')
                { input = (char)sr.Read();
                if (sr.EndOfStream)
                    break;
                if (input == '\r')
                    line_no++;
                }
            }
            
                if(input=='\\'&&sr.Peek()=='\\')           //Multi Line Comment
           {
               lang_part = true;
               while(input!='/'&&sr.Peek()!='/')
               {
                   input = (char)sr.Read();
                   if (sr.EndOfStream)
                       break;
                   if (input == '\r')
                       line_no++;
               }
                    if(!sr.EndOfStream)
                    { input = (char)sr.Read();
                    input = (char)sr.Read();
                    }
           }

                if(input.ToString().CompareTo(" ")==0||input=='\n')
                {
                    lang_part = true;
                }

                if(lang_part==false)               //any other error
                {
                    string error = null;
                    while (word_breakers(input) == true)
                    {
                        if (sr.EndOfStream)
                        {
                            error += input;
                            break;
                        }
                        error+= input;
                        input = (char)sr.Read();

                    } 
                    
                    token_set += "(" + error + "," + "Invalid Lexeme" + "," + line_no + ")" + "\n";
                    if (sr.EndOfStream)
                        break;
                    else
                        continue;
                }
                lang_part = false;
                if (sr.Peek() == -1)
                    break;
                input = (char)sr.Read();
               
            
            }

            token_set += "($,," + line_no + ")" + "\n";

           
               string[] lines = token_set.Split('\n');
               File.WriteAllLines(@"../output.txt", lines);
               SyntaxIn = new Token[lines.Length - 1];
               for (int i = 0; i < lines.Length-1; i++)
               {
                   string[] cvl=lines[i].Split(',');
                  cvl[0]=cvl[0].Remove(0, 1);
                  
                  if (cvl[1] == "")
                      cvl[1] = cvl[0];

                  if (lines[i].IndexOf(',') == 1)
                  { cvl[0] = cvl[1] = ",";
                  cvl[2] = cvl[3].Remove(cvl[3].Length - 1, 1);
                  }
                   else
                      cvl[2] = cvl[2].Remove(cvl[2].Length - 1, 1);
                  SyntaxIn[i] = new Token();
                  SyntaxIn[i].class_part = cvl[1];
                  SyntaxIn[i].value_part = cvl[0];
                  SyntaxIn[i].line_no = int.Parse(cvl[2]);

               }

               SyntaxAnalyzer S1 = new SyntaxAnalyzer(SyntaxIn);


        }

        public static bool word_breakers(char ch)
        {
            if ((ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch<= 'z') || ch == '_'||(ch>='0'&&ch<='9'))
            {return true;}
           
            else if(ch.ToString().CompareTo(" ") == 0)
            {
               
                return false;
            }

            else if (ch=='\n'|| ch=='\r')
            {
               
                return false;
            }
            char[] punctuators = { '{', '}', '(', ')', '[', ']', ',', ':', '.', ';', '*', '/', '\\', '\'', '\"', '+', '-', '!', '%', '<', '>', '=', '&', '|' };
            for (int i = 0; i < punctuators.Length; i++)
            {
                if (ch == punctuators[i])
                {
                    
                    return false;
                   
                }
            }
            return true;
        }

        public static string check_integer(string word)
        {
            string RULE = @"^[\+-]?\d+$";
            if (Regex.IsMatch(word, RULE))
            {

                return "(" + word + "," + "integer" + "," + line_no + ")";

            }
            return null;
        }
       


        public static string check_decimal(string word)
        {
            string RULE = @"^[+-]?\d*\.\d+([eE][-+]?[0-9]+)?$";
            if (Regex.IsMatch(word, RULE))
            {

                return "(" + word + "," + "decimal" + "," + line_no + ")";

            }
            return null;
        }
       

        public static string check_identifier(string word)
        {
            string RULE = @"^([A-Z]|[a-z]|_)([A-Z]|[a-z]|_|[0-9])*$";
              if(Regex.IsMatch(word,RULE))
              {

                  return "(" + word + "," + "ID"+ "," + line_no + ")";
                 
        }
              return null;
        }
    }
}
