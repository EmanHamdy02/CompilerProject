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
            string keywords = @"\b(read|write|repeat|until|if|elseif|else|then|return|endl\b";
            string dataTypes = @"\b(int|float|string)\b";
            string funNames = @"\b[a-zA-Z][a-zA-z0-9]*\b";
            string declaration = dataTypes + @"\s+" + @"\b[a-zA-Z][a-zA-z0-9]*\b" +
                @"(\s*:=\s*[^,;]+)?(\s*,\s*" + @"\b[a-zA-Z][a-zA-z0-9]*\b" + @"(\s*:=\s*[^,;]+)?)*\s*;";
            string write_stmt = @"\bwrite\b\s+([^;]+|endl)\s*;";
            string function_body = @"\{\s*.*?\breturn\b\s+[^;]+;\s*\}";
            string function_stmt = dataTypes + @"\s+" + funNames + @"\s*\(\s*\)\s*" + function_body;
            string main_function = dataTypes + @"\s+main\s*\(\s*\)\s*" + function_body;
            string number = @"^\d+(\.\d+)?$";
            string comment_stmt = @"\/\*[\s\S]*?\*\/";
            string condition_operators = @"\b=|<>|<|>\b";
            //write the identifier, term, statement, else_if_stmt, else_stmt regex here
            string identifier = "";
            string term = "";
            string statement = "";
            string else_if_stmt = "";
            string else_stmt = "";
            string condition = identifier + @"\s*" + condition_operators + @"\s*" + term;
            string boolean_operator = @"\b(&&|\|\|)\b";
            string condition_stmt = condition + @"(\s*" + boolean_operator + @"\s*" + condition + ")*";
            string if_stmt = @"^if\s+" + condition_stmt + @"\s+then\s+" + statement + "+(" + else_if_stmt + "|" + else_stmt + ")*end$";
            DataTable data = new DataTable();
            data.Columns.Add("Lexeme");
            data.Columns.Add("Tokens");
            dataGridView1.DataSource = data;
        }
    }
}
