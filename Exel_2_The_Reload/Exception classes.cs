using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exel_2_The_Reload
{
    public class RecursionException : Exception
    {
        public string var;
        public RecursionException(string var)
        {
            this.var = var;
        }
    }
    public class UnknownWordException : Exception
    {
        public string word;
        public int position;
        public UnknownWordException(int pos,string word)
        {
            this.position = pos;
            this.word = word;
        }
    }
    public class BadSyntaxException : Exception
    {
        public int position;
        public string token;
        public BadSyntaxException(int pos, string token)
        {
            this.token = token;
            this.position = pos;
        }
    }
   public class  UnknownSymbolException: Exception
    {
        public int position;
        public char _char;
        public UnknownSymbolException(int pos, char _char)
        {
            this._char = _char;
            this.position = pos;
        }
    }
    public class NullDivisionException: Exception
    {

    }
}
