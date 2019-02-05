using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Exel_2_The_Reload
{
    class TableFormatter
    {
        public void CellColumnIncrease(object obj, int columnPosition)
        {
            Cell cell = obj as Cell;

            //change name
            if (cell.columnIndex >= columnPosition)
            {
                cell.columnIndex++;
                cell.RecreateName();
            }
            //chande formula

            string pattern = @"(C(?<column>\d+))";

            Regex regex = new Regex(pattern);
            foreach (Match match in regex.Matches(cell.Formulate))
            {
                int columnIndex = Int32.Parse(match.Groups["column"].Value);
                if (columnIndex >= columnPosition)
                {
                    cell.Formulate = cell.Formulate.Replace("C" + (columnIndex).ToString(), "C" + (columnIndex + 1).ToString());
                }
            }
        }

        public void CellColumnDecrease(object obj, int columnPosition)
        {
            Cell cell = obj as Cell;

            //change name
            if (cell.columnIndex > columnPosition)
            {
                cell.columnIndex--;
                cell.RecreateName();
            }
            //chande formula

            string pattern = @"(R(?<row>\d+))(C(?<column>\d+))";

            Regex regex = new Regex(pattern);
            foreach (Match match in regex.Matches(cell.Formulate))
            {
                int columnIndex = Int32.Parse(match.Groups["column"].Value);
                if (columnIndex > columnPosition)
                    cell.Formulate = cell.Formulate.Replace("C" + (columnIndex).ToString(), "C" + (columnIndex - 1).ToString());
                if (columnIndex == columnPosition)
                    cell.Formulate = cell.Formulate.Replace("R" + match.Groups["row"].Value.ToString().ToString() + "C" + (columnIndex), "0");

            }
        }

        public void CellRowIncrease(object obj, int rowPosition)
        {
            Cell cell = obj as Cell;

            //change name
            if (cell.rowIndex >= rowPosition)
            {
                cell.rowIndex++;
                cell.RecreateName();
            }
            //chande formula

            string pattern = @"(R(?<row>\d+))";

            Regex regex = new Regex(pattern);
            foreach (Match match in regex.Matches(cell.Formulate))
            {
                int rowIndex = Int32.Parse(match.Groups["row"].Value);
                if (rowIndex >= rowPosition)
                    cell.Formulate = cell.Formulate.Replace("R" + (rowIndex).ToString(), "R" + (rowIndex + 1).ToString());
            }
        }

        public void CellRowDecrease(object obj, int rowPosition)
        {
            Cell cell = obj as Cell;

            //change name
            if (cell.rowIndex > rowPosition)
            {
                cell.rowIndex--;
                cell.RecreateName();
            }
            //chande formula

            string pattern = @"(R(?<row>\d+))(C(?<column>\d+))";

            Regex regex = new Regex(pattern);
            foreach (Match match in regex.Matches(cell.Formulate))
            {
                int rowIndex = Int32.Parse(match.Groups["row"].Value);
                if (rowIndex > rowPosition)
                    cell.Formulate = cell.Formulate.Replace("R" + (rowIndex).ToString(), "R" + (rowIndex - 1).ToString());
                if (rowIndex == rowPosition)
                    cell.Formulate = cell.Formulate.Replace("R" + (rowIndex) + "C" + match.Groups["column"].Value.ToString(), "0");

            }
        }

        public Cell RefreshCell(object obj)
        {
            var cell = obj as Cell;
            //Model model = new Model(cell.Formulate);
            cell.Formulate = (VarList.Get(cell.Name)).ToOut();
            //cell.value = ((model.Expr()).Calculate()).ToString();
            cell.value = (VarList.Get(cell.Name)).Calculate().ToString();
            return cell;
        }
        public Cell RecreateCell(object obj)
        {
            RefreshCell(obj);
            var cell = obj as Cell;
            cell.Formulate = cell.value;
            return cell;
        }

        public Cell BindFormula(object obj, string formula)
        {
            var cell = obj as Cell;
            cell.Formulate = formula;
            return cell;
        }
        public string GetValue(object obj)
        {
            var cell = obj as Cell;
            return cell.value;
        }

        public string GetFormula(object obj)
        {
            var cell = obj as Cell;
            return cell.Formulate;
        }
    }
}