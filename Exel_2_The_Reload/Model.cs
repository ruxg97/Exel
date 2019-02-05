using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exel_2_The_Reload
{
    class Model
    {
        /*
        block : expr((SEMICOM) expr)*
        expr   : term((PLUS | MINUS) term)*
        term : factor((MUL | DIV) factor)*
        factor : INTEGER | LPAREN expr RPAREN 
        */
        Token _currentToken;
        Lexer lexer;
        public Model(string expression)
        {
            if (expression == "" || expression == null)
                expression = "0";
            this.lexer = new Lexer(expression);
            _currentToken = lexer.GetNextToken();
            if (_currentToken.type == Types.type.CURLLBRACKET)
                _currentToken = lexer.GetNextToken();
        }

        ITreeNode Base()
        {
            if (_currentToken.type == Types.type.IF)
            {
                Token token = _currentToken;
                _currentToken = lexer.GetNextToken();
                if (_currentToken.type == Types.type.LBRACKET)
                {
                    _currentToken = lexer.GetNextToken();
                    ITreeNode condition = Expr();

                    if ((_currentToken.type == Types.type.RBRACKET))
                    {
                        _currentToken = lexer.GetNextToken();
                    }
                    else
                    {
                        throw new BadSyntaxException(lexer.GetPosition(), _currentToken.value);
                    }
                    if (_currentToken.type == Types.type.CURLLBRACKET)
                    {
                        _currentToken = lexer.GetNextToken();
                        return new IfNode(token, condition, Block());
                    }
                    return new IfNode(token, condition, Expr());
                }
                else
                    throw new BadSyntaxException(lexer.GetPosition(), _currentToken.value);
            }
            if (_currentToken.type == Types.type.WHILE)
            {
                Token token = _currentToken;
                _currentToken = lexer.GetNextToken();
                if (_currentToken.type == Types.type.LBRACKET)
                {
                    _currentToken = lexer.GetNextToken();
                    ITreeNode condition = Expr();

                    if ((_currentToken.type == Types.type.RBRACKET))
                    {
                        _currentToken = lexer.GetNextToken();
                    }
                    else
                    {
                        throw new BadSyntaxException(lexer.GetPosition(), _currentToken.value);
                    }
                    if (_currentToken.type == Types.type.CURLLBRACKET)
                    {
                        _currentToken = lexer.GetNextToken();
                        return new LoopNode(token, condition, Block());
                    }
                    return new LoopNode(token, condition, Expr());
                }
                else
                    throw new BadSyntaxException(lexer.GetPosition(), _currentToken.value);
            }

            if ((_currentToken.type == Types.type.MINUS) || (_currentToken.type == Types.type.PLUS))
            {
                Token token = _currentToken;
                _currentToken = lexer.GetNextToken();
                ITreeNode node = new UnarNode(token, factor());
                return node;
            }
            if ((_currentToken.type == Types.type.MAX) || (_currentToken.type == Types.type.MIN))
            {
                Token token = _currentToken;
                _currentToken = lexer.GetNextToken();

                if (_currentToken.type != Types.type.LBRACKET)
                    throw new BadSyntaxException(lexer.GetPosition(), token.value);

                _currentToken = lexer.GetNextToken();
                ITreeNode _leftNode = Expr();

                if (_currentToken.type == Types.type.COMMA)
                {
                    _currentToken = lexer.GetNextToken();
                    ITreeNode p = new BinarNode(token, _leftNode, Expr());
                    _currentToken = lexer.GetNextToken();
                    return p;
                }
                throw new BadSyntaxException(lexer.GetPosition(), _currentToken.value);
            }
            if (_currentToken.type == Types.type.INTEGER)
            {
                ITreeNode p = new AtomNode(_currentToken);
                _currentToken = lexer.GetNextToken();
                return p;
            }
            if (_currentToken.type == Types.type.VAR)
            {
                Token token = _currentToken;
                ITreeNode p = new AtomNode(_currentToken);
                _currentToken = lexer.GetNextToken();
                if (_currentToken.type == Types.type.EQUAL)
                {
                    token = _currentToken;
                    _currentToken = lexer.GetNextToken();
                    ITreeNode formula = Expr();
                    p = new BinarNode(token, p, formula);
                }
                if ((_currentToken.type == Types.type.INC) || (_currentToken.type == Types.type.DEC))
                {
                    token = _currentToken;
                    _currentToken = lexer.GetNextToken();
                    p = new UnarNode(token, p);
                }
                return p;
            }
            if (_currentToken.type == Types.type.NOT)
            {
                Token token = _currentToken;
                _currentToken = lexer.GetNextToken();
                ITreeNode node = new UnarNode(token, factor());
                return node;
            }
            if (_currentToken.type == Types.type.LBRACKET)
            {
                _currentToken = lexer.GetNextToken();
                ITreeNode p = Expr();
                if ((_currentToken.type == Types.type.RBRACKET))
                {
                    _currentToken = lexer.GetNextToken();
                }
                else
                {
                    throw new BadSyntaxException(lexer.GetPosition(), _currentToken.value);
                }
                return p;
            }
            if (_currentToken.type == Types.type.CURLLBRACKET)
            {
                _currentToken = lexer.GetNextToken();
                return Block();
            }
            if (_currentToken.type == Types.type.COMMENT)
            {
                _currentToken = lexer.GetNextToken();
                return Base();
            }

            throw new BadSyntaxException(lexer.GetPosition(), _currentToken.value);
        }
        ITreeNode factor()
        {
            //factor : INTEGER | LPAREN expr RPAREN
            ITreeNode p = Base();
            while (_currentToken.type == Types.type.POWER)
            {
                Token token = _currentToken;
                _currentToken = lexer.GetNextToken();
                p = new BinarNode(token, p, factor());
            }
            return p;
        }
        ITreeNode term()
        {
            //term : factor ((MUL | DIV) factor)*
            ITreeNode node = factor();
            while ((_currentToken.type == Types.type.MULT)
                || (_currentToken.type == Types.type.DIV) || (_currentToken.type == Types.type.MOD)
                || (_currentToken.type == Types.type.LARGER) || (_currentToken.type == Types.type.SMALLEREQUAL)
                || (_currentToken.type == Types.type.SMALLER) || (_currentToken.type == Types.type.LARGEREQUAL)
                || (_currentToken.type == Types.type.NOTEQUAL) || (_currentToken.type == Types.type.ISEQUAL))
            {
                Token token = _currentToken;
                _currentToken = lexer.GetNextToken();
                node = new BinarNode(token, node, factor());
            }
            return node;
        }
        public ITreeNode Expr()
        {
            ITreeNode node = term();

            while ((_currentToken.type == Types.type.PLUS)
                || (_currentToken.type == Types.type.MINUS)
                || (_currentToken.type == Types.type.OR)
                || (_currentToken.type == Types.type.AND))
            {
                Token token = _currentToken;
                _currentToken = lexer.GetNextToken();
                node = new BinarNode(token, node, term());
            }
            if ((_currentToken.type == Types.type.SEMICOMA)
                ||(_currentToken.type == Types.type.COMMA)
                || (_currentToken.type == Types.type.END))
            {
                return node;
            }
            if (_currentToken.type == Types.type.CURLRBRACKET)
            {
                return node;
            }
            if (_currentToken.type == Types.type.RBRACKET)
            {
                return node;
            }
            throw new BadSyntaxException(lexer.GetPosition(), _currentToken.value);
        }
        public ITreeNode Block()
        {
            BlockNode node = new BlockNode();
            while (_currentToken.type != Types.type.CURLRBRACKET)
            {
                node.Add(Expr());
                _currentToken = lexer.GetNextToken();
            }
            return node;
        }
    }
}
