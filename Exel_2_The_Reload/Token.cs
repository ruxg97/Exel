using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exel_2_The_Reload
{
    class Token
    {
        public Types.type type;
        public string value;
        Token() { }
        public Token(Types.type type, string value)
        {
            this.type = type;
            this.value = value;
        }
    }
}
