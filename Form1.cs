using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompilerPhase1
{
    public partial class Scanner : Form
    {
        public Scanner()
        {
            InitializeComponent();
        }
        public class Token
        {
            public string Value { get; set; }
            public string Type { get; set; }
        }
        public class LexicalAnalyzer
        {
            private List<(string Pattern, string Type)> tokenDefinitions;
            private readonly string patterns;

            public LexicalAnalyzer()
            {

                string keywords = @"\b(read|write|repeat|until|if|elseif|else|then|return|endl)\b";
                string dataTypes = @"\b(int|float|string)\b";
                string identifierPattern = @"\b[a-zA-Z][a-zA-z0-9]*\b";
                string funNames = @"\b[a-zA-Z]\w*(?=\s*\()";
                string declaration = dataTypes + @"\s+" + @"\b[a-zA-Z][a-zA-z0-9]*\b" +
                    @"(\s*:=\s*[^,;]+)?(\s*,\s*" + @"\b[a-zA-Z][a-zA-z0-9]*\b" + @"(\s*:=\s*[^,;]+)?)*\s*;";
                string write_stmt = @"\bwrite\b\s+([^;]+|endl)\s*;";
                string read_stmt = @"\bread\b\s+([^;]+|endl)\s*;";
                string function_body = @"\{\s*.*?\breturn\b\s+[^;]+;\s*\}";
                //string function_stmt = dataTypes + @"\s+" + funNames+ @"\s*\(\s*\)\s*" + function_body;
                //string main_function = dataTypes + @"\s+main\s*\(\s*\)\s*" + function_body;
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
                string statement = "(" + declaration + "|" + write_stmt + "|" + Repeat_Statement + "|" + assignmentPattern + "|" + returnPattern + "|" + "|" + condition_stmt + "|" + functionCallPattern + "|" + String + "|" + Equation + "|" + Expression + ")";
                string if_stmt = @"^if\s+" + condition_stmt + @"\s+then\s+" + statement + "+(" + else_if_stmt + "|" + else_stmt + ")*end$";
                string parameterPattern = @"^(int|float|double|char|string)\s+[A-Za-z][A-Za-z0-9]*$";
                string functionDeclarationPattern = @"^(int|float|double|char|string)\s+[A-Za-z][A-Za-z0-9]*\s*\((\s*(int|float|double|char|string)\s+[A-Za-z][A-Za-z0-9]*(\s*,\s*(int|float|double|char|string)\s+[A-Za-z][A-Za-z0-9]*)*)?\)$";


                tokenDefinitions = new List<(string Pattern, string Type)>
                {
                    (keywords, "Keyword"),
                    (dataTypes, "Data Type"),
                    (declaration, "Declaration"),
                    (write_stmt, "Write Statement"),
                    (number, "Number"),
                    (comment_stmt, "Comment"),
                    (condition_operators, "Condition Operator"),
                    (identifierPattern, "Identifier"),
                    (term, "Term"),
                    (else_if_stmt, "Else If Statement"),
                    (else_stmt, "Else Statement"),
                    (condition, "Condition"),
                    (boolean_operator, "Boolean Operator"),
                    (condition_stmt, "Condition Statement"),
                    (if_stmt, "If Statement"),
                    (String, "String"),
                    (Equation, "Equation"),
                    (read_stmt,"Read Statement"),
                    (Expression, "Expression"),
                    (Repeat_Statement, "Repeat Statement"),
                    (arithmeticPattern, "Arithmetic Operator"),
                    (functionCallPattern, "Function Call"),
                    (assignmentPattern, "Assignment"),
                    (returnPattern, "Return Statement"),
                    (parameterPattern, "Parameter"),
                    (funNames,"Function Name"),
                    (functionDeclarationPattern, "Function Declaration"),
                    (statement, "Statement"),
                    (function_body, "body"),
                };
                patterns = string.Join("|", tokenDefinitions.Select(td => td.Pattern));
            }
            public List<Token> Tokenize(string input)
            {
                string type, lexeme;
                DataTable data = new DataTable();
                data.Columns.Add("Lexeme");
                data.Columns.Add("Tokens");
                MatchCollection matches = Regex.Matches(input, patterns);
                List<Token> allTokens = new List<Token>();

                foreach (Match match in matches)
                {
                    lexeme = match.Value;
                    type = "Unknown";

                    if (string.IsNullOrWhiteSpace(lexeme))
                        continue;

                    foreach (var definition in tokenDefinitions)
                    {
                        if (Regex.IsMatch(lexeme, definition.Pattern))
                        {
                            if (definition.Type == "Statement" && string.IsNullOrEmpty(lexeme))
                                continue;

                            type = definition.Type;
                            break;
                        }
                    }
                    allTokens.Add(new Token { Value = lexeme, Type = type });
                    //data.Rows.Add(lexeme, type);
                }

                return allTokens;
            }
        }


        public class Parser
        {
            private List<Token> tokens;
            private int currentTokenIndex;
            private Token currentToken;

            public Parser(List<Token> tokens)
            {
                this.tokens = tokens;
                currentTokenIndex = 0;
                if (tokens.Count > 0)
                    currentToken = tokens[currentTokenIndex];
            }

            //A method that checks if the current token isn't the end of the code
            // And check if the token matches the argument passed to the method 
            // whether it's a value ex: ;, number (Value is the same var lexeme in prev code)
            // or a Type ex: identifier
            private void VerifyToken(string expected)
            {
                if (currentTokenIndex < tokens.Count && (currentToken.Value == expected || currentToken.Type == expected))
                {
                    currentTokenIndex++;
                    if (currentTokenIndex < tokens.Count)
                        currentToken = tokens[currentTokenIndex];
                    else
                        currentToken = null; // Reached the end of the tokens
                }

                else
                {
                    string found = currentToken != null ? currentToken.Value : "End of File";
                    throw new Exception($"Syntax Error: Expected '{expected}' but found '{found}'");
                }
            }
            public void parseProgram()
            {
                parseStmt();
            }
            private void parseStmts()
            {
                parseStmt();
                if (currentTokenIndex < tokens.Count && currentToken.Value == ";")
                {
                    VerifyToken(";"); //statments in tinyl ends with ; 
                    if (currentTokenIndex < tokens.Count && currentToken.Value != "}" && currentToken.Value
                        != "until") //} end of a function body and "until" is end of repeat statments
                    {
                        parseStmts();
                    }
                }
            }
            private void parseStmt()
            {
                if (currentTokenIndex >= tokens.Count)
                    return;
                if (currentToken.Value == "main")
                {
                    ParseMain_Function();
                }
                else if (currentToken.Value == "Number" || currentToken.Value == "String")
                {
                    ParseDeclaration_Statement();
                }
                else if (currentToken.Value == "if")
                {
                    ParseIf();
                }
                else if (currentToken.Value == "elseif")
                {
                    ParseElseIf();
                }
                else if (currentToken.Value == "else")
                {
                    ParseElse();
                }
                else if (currentToken.Value == "Return Statement")
                {
                    ParseReturn_Statement();
                }
                else if (currentToken.Value == "Read Statment")
                {
                    ParseRead();
                }
                else if (currentToken.Value == "Condition Statement")
                {
                    ParseCondition();
                }
                else if (currentToken.Value == "Comment")
                {
                    ParseComment();
                }
                else if (currentToken.Value == "Write Statement")
                {
                    ParseWrite_Statement();
                }
                else if (currentToken.Value == "Function Name")
                {
                    ParseFunction_Declaration();
                    ParseFunction_Statement();
                }
                else if (currentToken.Value == "Equation")
                {
                    ParseEquation();
                }
            }

            // PERSON 3:IMPLEMENTED RULES

            // 13) Declaration_Statement
            public void ParseDeclaration_Statement()
            {
                // regex 'declaration' captures the whole statement (e.g., "int x := 5;")
                // the parser simply verifies the scanner tagged it correctly.
                VerifyToken("Declaration");
            }

            // 14) Write_Statement
            public void ParseWrite_Statement()
            {
                //  'write_stmt' captures the whole statement.
                VerifyToken("Write Statement");
            }

            // 9) Equation
            public void ParseEquation()
            {
                // Your regex 'Equation' captures the whole math string.
                VerifyToken("Equation");
            }

            // 28) Function_Body
            // Grammar: {[Statement]*return_statment}
            public void ParseFunction_Body()
            {
                VerifyToken("{"); // Note: You may need to add "{" to your scanner's token definitions

                // Loop through statements until we hit a return statement
                while (currentToken != null && currentToken.Type != "Return Statement")
                {
                    VerifyToken("Statement");
                }

                VerifyToken("Return Statement");
                VerifyToken("}");
            }

            // 29) Function_Statement
            public void ParseFunction_Statement()
            {
                VerifyToken("Function Declaration");
                ParseFunction_Body();
            }

            // 30) Main_Function
            public void ParseMain_Function()
            {
                VerifyToken("Data Type");

                // Assuming your scanner picks up "main" as an Identifier or Keyword
                if (currentToken.Value == "main")
                {
                    VerifyToken("main");
                }
                else
                {
                    VerifyToken("Identifier");
                }

                VerifyToken("(");
                VerifyToken(")");
                ParseFunction_Body();
            }


            // Person 2
            public void ParseExpression()
            {
                ParseTerm();
                //VerifyToken("Expression");
                while (currentTokenIndex < tokens.Count && currentToken.Value == "+" ||
                    currentToken.Value == "-")
                {
                    VerifyToken(currentToken.Value);
                    ParseTerm();
                }
            }

            public void ParseTerm()
            {
                ParseFactor();
                while (currentTokenIndex < tokens.Count && currentToken.Value == "*" ||
                    currentToken.Value == "/")
                {
                    VerifyToken(currentToken.Value);
                    ParseFactor();
                }
            }
            public void ParseFactor()
            {
                if (currentToken.Type == "Identifier") VerifyToken("Identifier");
                else if (currentToken.Type == "Number") VerifyToken("Number");
                else if (currentToken.Value == "(")
                {
                    VerifyToken("(");
                    ParseExpression();
                    VerifyToken(")");
                }
                else throw new Exception($"Expected Identifier or Number but found" +
                    $"'{currentToken.Value}'");
            }
            public void ParseElseIf()
            {
                VerifyToken("Else If Statement");
                VerifyToken("(");
                ParseCondition();
                VerifyToken(")");
                VerifyToken("{");
                parseStmt();
                VerifyToken("}");
            }
            public void ParseElse()
            {
                VerifyToken("Else Statement");
                VerifyToken("{");
                parseStmt();
                VerifyToken("}");

            }

            // Person 4
            public void ParseReturn_Statement()
            {
                VerifyToken("Return Statement");
            }

            public void ParseFunction_Declaration()
            {
                VerifyToken("Function Name");
                VerifyToken("(");
                VerifyToken("Parameter");
                VerifyToken(")");
            }
            //Person 1
            public void ParseIf()
            {
                VerifyToken("If Statement");
                VerifyToken("(");
                ParseCondition();
                VerifyToken(")");
                VerifyToken("{");
                parseStmt();
                VerifyToken("}");

            }
            public void ParseComment()
            {
                VerifyToken("Comment");
            }

            public void ParseCondition()
            {
                ParseFactor();
                //VerifyToken("Condition");
            }

            public void ParseConditionStatement()
            {
                VerifyToken("{");
                parseStmt();
                VerifyToken("}");
                //VerifyToken("Condition Statement");
            }

            public void ParseRead()
            {
                VerifyToken("Read Statement");
            }

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
            LexicalAnalyzer myLexer = new LexicalAnalyzer();
            List<Token> data = myLexer.Tokenize(input);
            dataGridView1.DataSource = data;
            Parser parser = new Parser(data);

        }
    }
}
