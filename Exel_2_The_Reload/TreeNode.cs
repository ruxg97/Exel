using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exel_2_The_Reload
{
    interface ITreeNode
    {
        int Calculate();
        void Visit(string root);

        string ToOut();
    }

    class BlockNode : ITreeNode
    {
        //Token token;
        private List<ITreeNode> expressions;
        public BlockNode()
        {
            this.expressions = new List<ITreeNode>();
        }
        public string ToOut()
        {
            string result = "";
            foreach (ITreeNode expr in expressions)
            {
                result += (expr.ToOut() + ";");
            }
            return result;
        }
        public int Calculate()
        {
            foreach (ITreeNode expr in expressions)
            {
                expr.Calculate();
            }
            return 1;
        }

        public void Add(ITreeNode node)
        {
            this.expressions.Add(node);
        }
        public void Visit(string root)
        {
            foreach (ITreeNode expr in expressions)
            {
                //expr.Visit();
            }
        }
    }
    class IfNode : ITreeNode
    {
        Token token;
        ITreeNode condition;
        ITreeNode block;

        public IfNode(Token token, ITreeNode condition, ITreeNode block)
        {
            this.token = token;
            this.condition = condition;
            this.block = block;
        }

        public string ToOut()
        {
            return token.value + condition.ToOut() + block.ToOut();
        }
        public int Calculate()
        {
            if (condition.Calculate() != 0)
                return block.Calculate();
            return 0;
        }
        public void Visit(string root)
        {
            condition.Visit(root);
            block.Visit(root);
        }
    }
    class LoopNode : ITreeNode
    {
        Token token;
        ITreeNode condition;
        ITreeNode block;
        private int circlesCount;

        public LoopNode(Token token, ITreeNode condition, ITreeNode block)
        {
            this.token = token;
            this.condition = condition;
            this.block = block;
            circlesCount = 0;
        }

        public string ToOut()
        {
            return token.value + condition.ToOut() + block.ToOut();
        }
        public int Calculate()
        {
            while (condition.Calculate() != 0)
            {
                circlesCount++;
                    block.Calculate();
                if (circlesCount > 10000)
                    throw new StackOverflowException();
            }
            return 0;
        }
        public void Visit(string root)
        {
            condition.Visit(root);
            block.Visit(root);
        }
    }
    //class LogicNode : ITreeNode
    //{
    //    Token token;
    //    ITreeNode left;
    //    ITreeNode right;
    //    public LogicNode(Token token, ITreeNode left, ITreeNode right)
    //    {
    //        this.token = token;
    //        this.left = left;
    //        this.right = right;
    //    }
    //    public string ToOut()
    //    {
    //        return "(" + left.ToOut() + token.value + right.ToOut() + ")";
    //    }
    //    public int Calculate()
    //    {
    //        switch (token.type)
    //        {
    //            case Types.type.LARGER:
    //                {
    //                    if (left.Calculate() > right.Calculate())
    //                        return 1;
    //                    break;
    //                }
    //            case Types.type.LARGEREQUAL:
    //                {
    //                    if (left.Calculate() >= right.Calculate())
    //                        return 1;
    //                    break;
    //                }
    //            case Types.type.SMALLER:
    //                {
    //                    if (left.Calculate() < right.Calculate())
    //                        return 1;
    //                    break;
    //                }
    //            case Types.type.SMALLEREQUAL:
    //                {
    //                    if (left.Calculate() <= right.Calculate())
    //                        return 1;
    //                    break;
    //                }
    //            case Types.type.ISEQUAL:
    //                {
    //                    if (left.Calculate() == right.Calculate())
    //                        return 1;
    //                    break;
    //                }
    //            case Types.type.NOTEQUAL:
    //                {
    //                    if (left.Calculate() != right.Calculate())
    //                        return 1;
    //                    break;
    //                }
    //            case Types.type.AND:
    //                {
    //                    if ((left.Calculate()!=0)&&( right.Calculate()!=0))
    //                        return 1;
    //                    break;
    //                }
    //            case Types.type.OR:
    //                {
    //                    if ((left.Calculate() != 0) || (right.Calculate() != 0))
    //                        return 1;
    //                    break;
    //                }
    //        }
    //        return 0;
    //    }
    //    public void Visit(string root)
    //    {
    //        left.Visit(root);
    //        right.Visit(root);
    //    }
    //}
    class AtomNode : ITreeNode
    {
        Token token;

        public AtomNode(Token token)
        {
            this.token = token;
        }
        public int Calculate()
        {
            if (token.type == Types.type.INTEGER)
                return Int32.Parse(token.value);
            if (VarList.Get(token.value) == null)
                return 0;
            return VarList.Get(token.value).Calculate();
        }
        public void Visit(string root)
        {
            if (token.type == Types.type.VAR)
            {
                if (token.value == root)
                    throw new RecursionException(root);
                VarList.Get(token.value).Visit(root);
            }
        }
        public string ToOut()
        {
            if (token.type == Types.type.VAR)
            {
                return token.value;
            }
            if (token.type == Types.type.INTEGER)
                return token.value;
            return null;
        }

    }
    class UnarNode : ITreeNode
    {
        public ITreeNode son;

        public Token token;
        public UnarNode(Token token, ITreeNode son)
        {
            this.son = son;
            this.token = token;
        }
        public string ToOut()
        {
            if ((token.type == Types.type.INC))
                return son.ToOut() + "++";
            if ((token.type == Types.type.DEC))
                return son.ToOut() + "--";
            if ((token.type == Types.type.NOT))
                return "not (" + son.ToOut() + ")";
            return token.value + son.ToOut();
        }

        public int Calculate()
        {
            switch (token.type)
            {
                case Types.type.PLUS:
                    {
                        return son.Calculate();
                    }
                case Types.type.MINUS:
                    {
                        return -son.Calculate();
                    }
                case Types.type.NOT:
                    {
                        if (son.Calculate() == 0)
                            return 1;
                        return 0;
                    }
                case Types.type.INC:
                    {
                        if (son is AtomNode)
                        {
                            int num = VarList.Get(token.value).Calculate();
                            ITreeNode node = VarList.Get(son.ToOut());
                            VarList.Add(son.ToOut(), 
                                new BinarNode(new Token(Types.type.PLUS,"+"),node,new AtomNode(new Token(Types.type.INTEGER,"1"))));
                            return num;
                        }
                        throw new Exception();
                    }
                case Types.type.DEC:
                    {
                        if (son is AtomNode)
                        {
                            int num = VarList.Get(token.value).Calculate();
                            ITreeNode node = VarList.Get(son.ToOut());
                            VarList.Add(son.ToOut(),
                                new BinarNode(new Token(Types.type.MINUS, "-"), node, new AtomNode(new Token(Types.type.INTEGER, "1"))));
                            return num;
                        }
                        throw new Exception();
                    }
            }
            throw new Exception();
        }
        public void Visit(string root)
        {
            this.son.Visit(root);
        }
    }
    class BinarNode : ITreeNode
    {
        public ITreeNode left;
        public ITreeNode right;
        public Token token;

        public BinarNode(Token token, ITreeNode left, ITreeNode right)
        {
            this.left = left;
            this.right = right;
            this.token = token;
        }
        public string ToOut()
        {
            if ((token.type == Types.type.MAX) || (token.type == Types.type.MIN))
                return token.value + "(" + left.ToOut() + "," + right.ToOut() + ")";

            return "(" + left.ToOut()+")" + token.value + "("+right.ToOut() + ")";
        }

        public int Calculate()
        {
            switch (token.type)
            {
                case Types.type.MULT:
                    return left.Calculate() * right.Calculate();
                case Types.type.DIV:
                    {
                        int a = right.Calculate();
                        if (a == 0)
                        {
                            a++;
                            throw new NullDivisionException();
                        }
                        return left.Calculate() / a;
                    }
                case Types.type.MOD:
                    {
                        int a = right.Calculate();
                        if (a == 0)
                        {
                            a++;
                            throw new NullDivisionException();
                        }
                        return left.Calculate() % a;
                    }
                case Types.type.EQUAL:
                    {
                        VarList.Add(left.ToOut(), right);
                        return VarList.Get(token.value).Calculate();
                    }
                case Types.type.MAX:
                    return Math.Max(left.Calculate(), right.Calculate());
                case Types.type.MIN:
                    return Math.Min(left.Calculate(), right.Calculate());
                case Types.type.POWER:
                    return (int)Math.Pow(left.Calculate(), right.Calculate());
                case Types.type.PLUS:
                    return left.Calculate() + right.Calculate();

                case Types.type.MINUS:
                    return left.Calculate() - right.Calculate();

                case Types.type.LARGER:
                    {
                        if (left.Calculate() > right.Calculate())
                            return 1;
                        break;
                    }
                case Types.type.LARGEREQUAL:
                    {
                        if (left.Calculate() >= right.Calculate())
                            return 1;
                        break;
                    }
                case Types.type.SMALLER:
                    {
                        if (left.Calculate() < right.Calculate())
                            return 1;
                        break;
                    }
                case Types.type.SMALLEREQUAL:
                    {
                        if (left.Calculate() <= right.Calculate())
                            return 1;
                        break;
                    }
                case Types.type.ISEQUAL:
                    {
                        if (left.Calculate() == right.Calculate())
                            return 1;
                        break;
                    }
                case Types.type.NOTEQUAL:
                    {
                        if (left.Calculate() != right.Calculate())
                            return 1;
                        break;
                    }
                case Types.type.AND:
                    {
                        if ((left.Calculate() != 0) && (right.Calculate() != 0))
                            return 1;
                        break;
                    }
                case Types.type.OR:
                    {
                        if ((left.Calculate() != 0) || (right.Calculate() != 0))
                            return 1;
                        break;
                    }
            }
            return 0;
        }

        public void Visit(string root)
        {
            left.Visit(root);
            right.Visit(root);
        }
    }
}