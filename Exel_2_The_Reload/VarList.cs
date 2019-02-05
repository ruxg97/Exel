using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exel_2_The_Reload
{
    class VarList
    {
        static private Dictionary<string, ITreeNode> map = new Dictionary<string, ITreeNode>();
        public static void Add(string name, ITreeNode node)
        {
            if (map.ContainsKey(name))
            {
                VarList.ContainsRecursion(node, name);

                map[name] = node;
            }
            else
            {
                VarList.ContainsRecursion(node, name);
                map.Add(name, node);
            }
        }
        public static void Add(string name, string formula)
        {
            if (formula == null)
                formula = "0";
            Model model = new Model(formula);
            if (map.ContainsKey(name))
            {
                ITreeNode node = model.Expr();
                VarList.ContainsRecursion(node, name);

                map[name] = node;
            }
            else
            {
                ITreeNode node = model.Expr();
                VarList.ContainsRecursion(node, name);
                map.Add(name, node);
            }

        }
        public static ITreeNode Get(string name)
        {
            if (map.ContainsKey(name))
                return map[name];
            else
            {
                return new AtomNode(new Token(Types.type.INTEGER, "0"));
            }
        }
        private static void ContainsRecursion(ITreeNode node, string root)
        {
            node.Visit(root);
        }
    }
}
