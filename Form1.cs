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
        private void button1_clicked (object sender, EventArgs e)
        {
            string input = textBox1.Text;
            string keywords = @"\b(| read | write | repeat |
            until | if | elseif | else | then | return | endl\b";
            string dataTypes = @"\b(int | float | string )\b";
            string funNames = @"\b[a-zA-Z][a-zA-z0-9]*\b";
            string declaration = dataTypes + @"\s+" + @"\b[a-zA-Z][a-zA-z0-9]*\b" +
                @"(\s*:=\s*[^,;]+)?(\s*,\s*" + @"\b[a-zA-Z][a-zA-z0-9]*\b" + @"(\s*:=\s*[^,;]+)?)*\s*;";
            string write_stmt = @"\bwrite\b\s+([^;]+|endl)\s*;";
            string function_body = @"\{\s*.*?\breturn\b\s+[^;]+;\s*\}";
            string function_stmt = dataTypes + @"\s+" + funNames + @"\s*\(\s*\)\s*" + function_body;
            string main_function = dataTypes + @"\s+main\s*\(\s*\)\s*" + function_body;
            string patterns= $"{keywords}|{dataTypes}|{funNames}|" +
                $"{declaration}|{write_stmt}|{function_stmt}|{main_function}";
            MatchCollection matches = Regex.Matches(input, patterns); 
            foreach (Match match in matches) {
                string lexeme = match.Value;
                string type = "";
                if(Regex.IsMatch(lexeme, keywords))
                {
                    type = "Keyword";
                }
                else if (Regex.IsMatch(lexeme, dataTypes))
                {
                    type = "Data Type";
                }
                else if (Regex.IsMatch(lexeme, funNames))
                {
                    type = "Function Name";
                }
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
                DataTable data = new DataTable();
            data.Columns.Add("Lexeme");
            data.Columns.Add("Tokens");

            dataGridView1.DataSource = data;
        }
    }
}
