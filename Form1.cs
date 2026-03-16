using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CompilerPhase1
{
    public partial class Scanner : Form
    {
        public Scanner()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != null)
            {
                button1.Enabled = true;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text;
            string keywords = @"\b(read|write|repeat|until|if|elseif|else|then|return|endl)\b";
            string dataTypes = @"\b(int|float|string)\b";
            string identifierPattern = @"\b[a-zA-Z][a-zA-z0-9]*\b";
            string funNames = @"\b[a-zA-Z]\w*(?=\s*\()";
            string declaration = dataTypes + @"\s+" + @"\b[a-zA-Z][a-zA-z0-9]*\b" +
                @"(\s*:=\s*[^,;]+)?(\s*,\s*" + @"\b[a-zA-Z][a-zA-z0-9]*\b" + @"(\s*:=\s*[^,;]+)?)*\s*;";
            string write_stmt = @"\bwrite\b\s+([^;]+|endl)\s*;";
            string function_body = @"\{\s*.*?\breturn\b\s+[^;]+;\s*\}";
            string function_stmt = dataTypes + @"\s+" + funNames+ @"\s*\(\s*\)\s*" + function_body;
            string main_function = dataTypes + @"\s+main\s*\(\s*\)\s*" + function_body;
            string number = @"\b\d+(\.\d+)?\b";
            string comment_stmt = @"\/\*[\s\S]*?\*\/";
            string condition_operators = @"(=|<>|<|>)";
            
            string term = @"^(?:\\d+|[A-Za-z_]\w*|\w+\([^)]*\))$";
            
            string String = "^\".+\"$";
            string Equation = @"^(?:\(*[A-Za-z0-9]+\)*)(?:\s*[\+\-\*/]\s*\(*[A-Za-z0-9]+\)*)+$";
            //"^(?:\\(*[A-Za-z0-9]+\\)*)(?:\\s*[\\+\\-\\*/]\\s*\\(*[A-Za-z0-9]+\\)*)+$";
            string Expression = String + "|" + term + "|" + Equation;
            //"^\".+\"$|^(?:\\\\d+|[A-Za-z_]\\\\w*|\\\\w+\\\\([^)]*\\\\))$|^(?:\\(*[A-Za-z0-9]+\\)*)(?:\\s*[\\+\\-\\*/]\\s*\\(*[A-Za-z0-9]+\\)*)+$";
            string else_if_stmt = "^elseif\\s*\\([^)]*\\)\\s*\\{[^}]*\\}$";
            string else_stmt = "^else[\\s\\S]+end$";
            string Repeat_Statement = "^repeat[\\s\\S]+until\\s*\\([^)]*\\)$";
            string condition = identifierPattern + @"\s*" + condition_operators + @"\s*" + term;
            string boolean_operator = @"\b(&&|\|\|)\b";
                
            string condition_stmt = @"^" + condition + @"(\s*" + boolean_operator + @"\s*" + condition + @")*$";
            //condition + @"(\s*" + boolean_operator + @"\s*" + condition + ")*";
            string arithmeticPattern = @"^[+\-*/]$";
            string functionCallPattern = @"^[A-Za-z][A-Za-z0-9]*\((([A-Za-z][A-Za-z0-9]*)(,\s*[A-Za-z][A-Za-z0-9]*)*)?\)$";
            string assignmentPattern = @"^[A-Za-z][A-Za-z0-9]*\s*:=\s*.+$";
            string returnPattern = @"^return\s+.+;$";
            string statement = "(" + declaration + "|" + write_stmt + "|" + function_stmt + "|" + Repeat_Statement + "|" + assignmentPattern + "|" + returnPattern + "|" + "|" + condition_stmt + "|" + functionCallPattern + "|" + String + "|" + Equation + "|" + Expression+")";
            string if_stmt = @"^if\s+" + condition_stmt + @"\s+then\s+" + statement + "+(" + else_if_stmt + "|" + else_stmt + ")*end$";
            string parameterPattern = @"^(int|float|double|char|string)\s+[A-Za-z][A-Za-z0-9]*$";
            string functionDeclarationPattern = @"^(int|float|double|char|string)\s+[A-Za-z][A-Za-z0-9]*\s*\((\s*(int|float|double|char|string)\s+[A-Za-z][A-Za-z0-9]*(\s*,\s*(int|float|double|char|string)\s+[A-Za-z][A-Za-z0-9]*)*)?\)$";
            string patterns = $"{keywords}|{dataTypes}|{declaration}|{write_stmt}|{function_body}|{function_stmt}|{main_function}|{number}|" +
                $"{comment_stmt}|{condition_operators}|{term}|{identifierPattern}|{statement}|{else_if_stmt}|{else_stmt}|{condition}|{boolean_operator}|" +
                $"{condition_stmt}|{if_stmt}|{String}|{Equation}|{Expression}|{Repeat_Statement}|{arithmeticPattern}|{functionCallPattern}|" +
                $"{assignmentPattern}|{returnPattern}|{parameterPattern}|{functionDeclarationPattern}";
            DataTable data = new DataTable();
            data.Columns.Add("Lexeme");
            data.Columns.Add("Tokens");
            MatchCollection matches = Regex.Matches(input, patterns);
            string type, lexeme;
            foreach (Match match in matches)
            {
                lexeme = match.Value;
                type = "";
                if (Regex.IsMatch(lexeme, keywords))
                {
                    type = "Keyword";
                }
                else if (Regex.IsMatch(lexeme, dataTypes))
                {
                    type = "Data Type";
                }
                //else if (Regex.IsMatch(lexeme, funNames))
                //{
                //    type = "Function Name";
                //}
                else if (Regex.IsMatch(lexeme, declaration))
                {
                    type = "Declaration";
                }
                else if (Regex.IsMatch(lexeme, write_stmt))
                {
                    type = "Write Statement";
                }
                else if (Regex.IsMatch(lexeme, function_stmt))
                {
                    type = "Function Statement";
                }
                else if (Regex.IsMatch(lexeme, main_function))
                {
                    type = "Main Function";
                }
                else if (Regex.IsMatch(lexeme, number))
                {
                    type = "Number";
                }
                else if (Regex.IsMatch(lexeme, comment_stmt))
                {
                    type = "Comment";
                }
                else if (Regex.IsMatch(lexeme, condition_operators))
                {
                    type = "Condition Operator";
                }
                else if (Regex.IsMatch(lexeme, identifierPattern))
                {
                    type = "Identifier";
                }
                else if (Regex.IsMatch(lexeme, term))
                {
                    type = "Term";
                }
                
                else if (Regex.IsMatch(lexeme, else_if_stmt))
                {
                    type = "Else If Statement";
                }
                else if (Regex.IsMatch(lexeme, else_stmt))
                {
                    type = "Else Statement";
                }
                else if (Regex.IsMatch(lexeme, condition))
                {
                    type = "Condition";
                }
                else if (Regex.IsMatch(lexeme, boolean_operator))
                {
                    type = "Boolean Operator";
                }
                else if (Regex.IsMatch(lexeme, condition_stmt))
                {
                    type = "Condition Statement";
                }
                else if (Regex.IsMatch(lexeme, if_stmt))
                {
                    type = "If Statement";
                }
                else if (Regex.IsMatch(lexeme, String))
                {
                    type = "String";
                }
                else if (Regex.IsMatch(lexeme, Equation))
                {
                    type = "Equation";
                }
                else if (Regex.IsMatch(lexeme, Expression))
                {
                    type = "Expression";
                }
                else if (Regex.IsMatch(lexeme, Repeat_Statement))
                {
                    type = "Repeat Statement";
                }
                else if (Regex.IsMatch(lexeme, arithmeticPattern))
                {
                    type = "Arithmetic Operator";
                }
                else if (Regex.IsMatch(lexeme, functionCallPattern))
                {
                    type = "Function Call";
                }
                else if (Regex.IsMatch(lexeme, assignmentPattern))
                {
                    type = "Assignment";
                }
                else if (Regex.IsMatch(lexeme, returnPattern))
                {
                    type = "Return Statement";
                }
                else if (Regex.IsMatch(lexeme, parameterPattern))
                {
                    type = "Parameter";
                }
                else if (Regex.IsMatch(lexeme, functionDeclarationPattern))
                {
                    type = "Function Declaration";
                }
                else if (Regex.IsMatch(lexeme, statement))
                {
                    if (!string.IsNullOrEmpty(lexeme))
                        type = "Statement";
                }
                //else
                //{
                //    type = "Unknown";
                //}
                dataGridView1.DataSource = data;
                data.Rows.Add(lexeme, type);
            }
        }
    }
}