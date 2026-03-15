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
using static System.Windows.Forms.LinkLabel;

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
        private void button1_clicked(object sender, EventArgs e)
        {
            //string input = textBox1.Text;
            //string keywords = @"\b(read|write|repeat|until|if|elseif|else|then|return|endl)\b";
            //string dataTypes = @"\b(int|float|string)\b";
            //string funNames = @"\b[a-zA-Z][a-zA-z0-9]*\b";
            //string declaration = dataTypes + @"\s+" + @"\b[a-zA-Z][a-zA-z0-9]*\b" +
            //    @"(\s*:=\s*[^,;]+)?(\s*,\s*" + @"\b[a-zA-Z][a-zA-z0-9]*\b" + @"(\s*:=\s*[^,;]+)?)*\s*;";
            //string write_stmt = @"\bwrite\b\s+([^;]+|endl)\s*;";
            //string function_body = @"\{\s*.*?\breturn\b\s+[^;]+;\s*\}";
            //string function_stmt = dataTypes + @"\s+" + funNames + @"\s*\(\s*\)\s*" + function_body;
            //string main_function = dataTypes + @"\s+main\s*\(\s*\)\s*" + function_body;
            //string number = @"^\d+(\.\d+)?$";
            //string comment_stmt = @"\/\*[\s\S]*?\*\/";
            //string condition_operators = @"\b=|<>|<|>\b";
            ////write the identifier, term, statement, else_if_stmt, else_stmt regex here
            //string identifier = @"\b[A-Za-z][A-Za-z0-9]*\b";




            //string term = "";
            //string statement = "";
            //string else_if_stmt = "";
            //string else_stmt = "";





            //string condition = identifier + @"\s*" + condition_operators + @"\s*" + term;
            //string boolean_operator = @"\b(&&|\|\|)\b";
            //string condition_stmt = condition + @"(\s*" + boolean_operator + @"\s*" + condition + ")*";
            //string if_stmt = @"^if\s+" + condition_stmt + @"\s+then\s+" + statement + "+(" + else_if_stmt + "|" + else_stmt + ")*end$";
            //string arithmetic = @"[+\-*/]";
            //// Statements
            //string functionCallPattern = @"^[A-Za-z][A-Za-z0-9]*\((([A-Za-z][A-Za-z0-9]*)(,\s*[A-Za-z][A-Za-z0-9]*)*)?\)$";
            //string assignmentPattern = @"^[A-Za-z][A-Za-z0-9]*\s*:=\s*.+$";
            //string returnPattern = @"^return\s+.+;$";
            //string parameterPattern = @"^(int|float|double|char|string)\s+[A-Za-z][A-Za-z0-9]*$";
            //string functionDeclarationPattern =
            //    @"^(int|float|double|char|string)\s+[A-Za-z][A-Za-z0-9]*\s*\((\s*(int|float|double|char|string)\s+[A-Za-z][A-Za-z0-9]*(\s*,\s*(int|float|double|char|string)\s+[A-Za-z][A-Za-z0-9]*)*)?\)$";


            //string[] lines = Regex.Split(input, @"\r?\n");
            //DataTable data = new DataTable();
            //data.Columns.Add("Lexeme");
            //data.Columns.Add("Tokens");
            //foreach (string line in lines)
            //{
            //    string trimmed = line.Trim();

            //    if (string.IsNullOrEmpty(trimmed))
            //        continue;

            //    // ===== Statement Detection =====
            //    string statementType = "Unknown Statement";

            //    if (Regex.IsMatch(trimmed, functionCallPattern))
            //        statementType = "Function Call";

            //    else if (Regex.IsMatch(trimmed, assignmentPattern))
            //        statementType = "Assignment Statement";

            //    else if (Regex.IsMatch(trimmed, returnPattern))
            //        statementType = "Return Statement";

            //    else if (Regex.IsMatch(trimmed, parameterPattern))
            //        statementType = "Parameter";

            //    else if (Regex.IsMatch(trimmed, functionDeclarationPattern))
            //        statementType = "Function Declaration";
            //    data.Rows.Add(trimmed, statementType);
            //    //dataGridView1.Rows.Add(trimmed, statementType);

            //    string patterns = $"{keywords}|{dataTypes}|{funNames}|{declaration}|{write_stmt}|{function_body}|{function_stmt}|{main_function}|{number}|" +
            //    $"{comment_stmt}|{condition_operators}|{term}|{identifier}|{statement}|{else_if_stmt}|{else_stmt}|{condition}|{boolean_operator}|" +
            //    $"{condition_stmt}|{if_stmt}";


            //    MatchCollection matches = Regex.Matches(trimmed, patterns);
            //    string type, lexeme;
            //    foreach (Match match in matches)
            //    {
            //        lexeme = match.Value;
            //        type = "";
            //        if (Regex.IsMatch(lexeme, keywords))
            //        {
            //            type = "Keyword";
            //        }
            //        else if (Regex.IsMatch(lexeme, dataTypes))
            //        {
            //            type = "Data Type";
            //        }
            //        else if (Regex.IsMatch(lexeme, funNames))
            //        {
            //            type = "Function Name";
            //        }
            //        else if (Regex.IsMatch(lexeme, declaration))
            //        {
            //            type = "Declaration";
            //        }
            //        else if (Regex.IsMatch(lexeme, write_stmt))
            //        {
            //            type = "Write Statement";
            //        }
            //        else if (Regex.IsMatch(lexeme, function_stmt))
            //        {
            //            type = "Function Statement";
            //        }
            //        else if (Regex.IsMatch(lexeme, main_function))
            //        {
            //            type = "Main Function";
            //        }
            //        else if (Regex.IsMatch(lexeme, number))
            //        {
            //            type = "Number";
            //        }
            //        else if (Regex.IsMatch(lexeme, comment_stmt))
            //        {
            //            type = "Comment";
            //        }
            //        else if (Regex.IsMatch(lexeme, condition_operators))
            //        {
            //            type = "Condition Operator";
            //        }
            //        else if (Regex.IsMatch(lexeme, identifier))
            //        {
            //            type = "Identifier";
            //        }
            //        else if (Regex.IsMatch(lexeme, term))
            //        {
            //            type = "Term";
            //        }
            //        else if (Regex.IsMatch(lexeme, statement))
            //        {
            //            type = "Statement";
            //        }
            //        else if (Regex.IsMatch(lexeme, else_if_stmt))
            //        {
            //            type = "Else If Statement";
            //        }
            //        else if (Regex.IsMatch(lexeme, else_stmt))
            //        {
            //            type = "Else Statement";
            //        }
            //        else if (Regex.IsMatch(lexeme, condition))
            //        {
            //            type = "Condition";
            //        }
            //        else if (Regex.IsMatch(lexeme, boolean_operator))
            //        {
            //            type = "Boolean Operator";
            //        }
            //        else if (Regex.IsMatch(lexeme, condition_stmt))
            //        {
            //            type = "Condition Statement";
            //        }
            //        else if (Regex.IsMatch(lexeme, if_stmt))
            //        {
            //            type = "If Statement";
            //        }
            //        else
            //        {
            //            type = "Unknown";
            //        }

            //        data.Rows.Add(lexeme, type);
            //    }
            //}
            //dataGridView1.DataSource = data;

        }

        private void button1_Click(object sender, EventArgs e)
        {

            dataGridView1.Rows.Clear();

            string[] lines = Regex.Split(textBox1.Text, @"\r?\n");

            string arithmeticPattern = @"^[+\-*/]$";
            string identifierPattern = @"^[A-Za-z][A-Za-z0-9]*$";

            string functionCallPattern = @"^[A-Za-z][A-Za-z0-9]*\((([A-Za-z][A-Za-z0-9]*)(,\s*[A-Za-z][A-Za-z0-9]*)*)?\)$";
            string assignmentPattern = @"^[A-Za-z][A-Za-z0-9]*\s*:=\s*.+$";

            string returnPattern = @"^return\s+.+;$";

            string parameterPattern = @"^(int|float|double|char|string)\s+[A-Za-z][A-Za-z0-9]*$";

            string functionDeclarationPattern = @"^(int|float|double|char|string)\s+[A-Za-z][A-Za-z0-9]*\s*\((\s*(int|float|double|char|string)\s+[A-Za-z][A-Za-z0-9]*(\s*,\s*(int|float|double|char|string)\s+[A-Za-z][A-Za-z0-9]*)*)?\)$";

            foreach (string line in lines)
            {
                string processed = Regex.Replace(line, @"\s*([+\-*/])\s*", "$1");
                string trimmed = processed.Trim();

                if (string.IsNullOrEmpty(trimmed))
                    continue;

                string type = "Unknown";

                if (Regex.IsMatch(trimmed, arithmeticPattern))
                    type = "Arithmetic Operator";

                else if (Regex.IsMatch(trimmed, identifierPattern))
                    type = "Identifier";

                else if (Regex.IsMatch(trimmed, functionCallPattern))
                    type = "Function Call";

                else if (Regex.IsMatch(trimmed, assignmentPattern))
                    type = "Assignment Statement";

                else if (Regex.IsMatch(trimmed, returnPattern))
                    type = "Return Statement";

                else if (Regex.IsMatch(trimmed, parameterPattern))
                    type = "Parameter";

                else if (Regex.IsMatch(trimmed, functionDeclarationPattern))
                    type = "Function Declaration";

                dataGridView1.Rows.Add(trimmed, type);
            }
        }
    }
}