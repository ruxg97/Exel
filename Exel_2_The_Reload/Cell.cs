using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Exel_2_The_Reload
{
    class Cell
    {
        private string formula = "0";
        private string cellName;
        public bool IsComment = false;
        public string value;
        public int rowIndex;
        public int columnIndex;
        public Cell(string formula, string name)
        {
            this.cellName = name;

            string pattern = @"\b(R(?<row>\d+))\B(C(?<column>\d+))";

            Regex regex = new Regex(pattern);

            this.rowIndex = Int32.Parse(regex.Match(name).Groups["row"].Value);
            this.columnIndex = Int32.Parse(regex.Match(name).Groups["column"].Value);

            this.Formulate = formula;
            if (Formulate == "")
                this.Formulate = "0";
            this.value = Formulate;

        }

        public string Formulate
        {
            get
            {
                return formula;
            }
            set
            {
                if ((value.Length > 0)&&( value[0] == '@'))
                {
                    this.IsComment = true;
                    VarList.Add(this.cellName, "0");
                    this.formula = value;
                    return;
                }
                this.IsComment = false;
                VarList.Add(this.cellName, value);
                this.formula = value;
            }
        }

        public string Name
        {
            get
            {
                return this.cellName;
            }
            set
            {
                VarList.Add(value, VarList.Get(this.cellName));
                this.cellName = value;
            }
        }

        public void RecreateName()
        {
            this.Name = "R" + rowIndex.ToString() + "C" + columnIndex.ToString();
        }
    }
}