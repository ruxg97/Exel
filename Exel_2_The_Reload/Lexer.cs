using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Exel_2_The_Reload
{
    static class Types
    {
        public enum type
        {
            PLUS, MINUS, MULT, DIV, MIN, MAX, POWER, MOD, EQUAL, INC, DEC,
            LBRACKET, RBRACKET, CURLLBRACKET, CURLRBRACKET, SEMICOMA, END, COMMA,
            INTEGER, VAR, COMMENT,
            WHILE, IF,
            LARGEREQUAL, LARGER, SMALLER, SMALLEREQUAL, ISEQUAL, NOTEQUAL,
            OR, NOT, AND
        }
    }
    class Lexer
    {
        private char eof = (char)26;
        string text;
        int pos;
        char _currentChar;

        public Lexer(string text)
        {
            this.text = text;
            this.pos = 0;
            this._currentChar = text[0];
        }

        void Advance()
        {
            ++pos;
            if (pos > text.Length - 1)
                _currentChar = eof;
            else
                _currentChar = text[pos];
        }

        private string GetComment()
        {
            Advance();
            string result = "";
            while (_currentChar != '@')
            {
                result += _currentChar;
                Advance();
            }
            Advance();
            return result;
        }


        string GetWord()
        {
            string result = "";
            while (Char.IsLetterOrDigit(_currentChar))
            {
                result += _currentChar;
                Advance();
            }
            return result;
        }

        string GetInteger()
        {
            string result = "";
            while (_currentChar != eof && Char.IsDigit(_currentChar))
            {
                result += _currentChar;
                Advance();
            }
            return result;
        }

        private Token GetWordToken()
        {
            string word = GetWord();
            string _varPattern = @"^(R(?<row>\d+))(C(?<column>\d+))$";

            Regex regex = new Regex(_varPattern);
            if (regex.Match(word.ToUpper()).Success)
            {
                return new Token(Types.type.VAR, word.ToUpper());
            }
            if (word.ToLower() == "max")
            {
                return new Token(Types.type.MAX, word);
            }
            if (word.ToLower() == "min")
            {
                return new Token(Types.type.MIN, word);
            }
            if (word.ToLower() == "mod")
            {
                return new Token(Types.type.MOD, "mod");
            }
            if (word.ToLower() == "div")
            {
                return new Token(Types.type.DIV, "div");
            }

            if (word.ToLower() == "not")
            {
                return new Token(Types.type.NOT, "not");
            }
            if (word.ToLower() == "and")
            {
                return new Token(Types.type.AND, "and");
            }
            if (word.ToLower() == "or")
            {
                return new Token(Types.type.OR, "or");
            }
            if (word.ToLower() == "if")
            {
                return new Token(Types.type.IF, "if");
            }
            if (word.ToLower() == "while")
            {
                return new Token(Types.type.WHILE, "while");
            }
            throw new UnknownWordException(pos, word);
        }
        public int GetPosition()
        {
            return pos - 1;
        }
        public Token GetNextToken()
        {
            while (_currentChar != eof)

            {
                if (Char.IsWhiteSpace(_currentChar))
                {
                    Advance();
                    continue;
                }
                if (Char.IsDigit(_currentChar))
                    return new Token(Types.type.INTEGER, GetInteger());

                if (_currentChar == '+')
                {
                    Advance();
                    if (_currentChar == '+')
                    {
                        Advance();
                        return new Token(Types.type.INC, "++");
                    }
                    return new Token(Types.type.PLUS, "+");
                }
                if (Char.IsLetter(_currentChar))
                {
                    return GetWordToken();
                }
                if (_currentChar == '^')
                {
                    Advance();
                    return new Token(Types.type.POWER, "^");
                }
                if (_currentChar == '{')
                {
                    Advance();
                    return new Token(Types.type.CURLLBRACKET, "{");
                }
                if (_currentChar == '}')
                {
                    Advance();
                    return new Token(Types.type.CURLRBRACKET, "}");
                }
                if (_currentChar == '>')
                {
                    Advance();
                    if (_currentChar == '=')
                    {
                        Advance();
                        return new Token(Types.type.LARGEREQUAL, ">=");
                    }
                    return new Token(Types.type.LARGER, ">");
                }
                if (_currentChar == '!')
                {
                    Advance();
                    if (_currentChar == '=')
                    {
                        Advance();
                        return new Token(Types.type.NOTEQUAL, "!=");
                    }
                    throw new BadSyntaxException(this.GetPosition(), "!");
                }
                if (_currentChar == '=')
                {
                    Advance();
                    if (_currentChar == '=')
                    {
                        Advance();
                        return new Token(Types.type.ISEQUAL, "==");
                    }
                    return new Token(Types.type.EQUAL, "=");
                }

                if (_currentChar == '<')
                {
                    Advance();
                    if (_currentChar == '=')
                    {
                        Advance();
                        return new Token(Types.type.SMALLEREQUAL, "<=");
                    }
                    if (_currentChar == '>')
                    {
                        Advance();
                        return new Token(Types.type.NOTEQUAL, "<>");
                    }
                    return new Token(Types.type.SMALLER, "<");
                }

                if (_currentChar == '%')
                {
                    Advance();
                    return new Token(Types.type.MOD, "%");
                }
                if (_currentChar == '-')
                {
                    Advance();
                    if (_currentChar == '-')
                    {
                        Advance();
                        return new Token(Types.type.DEC, "--");
                    }
                    return new Token(Types.type.MINUS, "-");
                }
                if (_currentChar == ',')
                {
                    Advance();
                    return new Token(Types.type.COMMA, ",");
                }
                if (_currentChar == ';')
                {
                    Advance();
                    return new Token(Types.type.SEMICOMA, ";");
                }
                if (_currentChar == '*')
                {
                    Advance();
                    return new Token(Types.type.MULT, "*");
                }
                if (_currentChar == '@')
                {
                    return new Token(Types.type.COMMENT, GetComment());
                }
                if (_currentChar == '/')
                {
                    Advance();
                    return new Token(Types.type.DIV, "/");
                }
                if (_currentChar == '(')
                {
                    Advance();
                    return new Token(Types.type.LBRACKET, "(");
                }
                if (_currentChar == ')')
                {
                    Advance();
                    return new Token(Types.type.RBRACKET, ")");
                }
                throw new UnknownSymbolException(pos, _currentChar);
            }
            return new Token(Types.type.END, Char.ToString(eof));
        }

    }
}
