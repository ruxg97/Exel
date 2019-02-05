using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

namespace Exel_2_The_Reload
{
    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }
    public static class WinObjFunctions
    {
        public static DataTable ToDataTable(DataGridView dataGridView, string tableName)
        {
            DataGridView dgv = dataGridView;
            DataTable table = new DataTable(tableName);

            for (int iCol = 0; iCol < dgv.Columns.Count; iCol++)
            {
                table.Columns.Add(dgv.Columns[iCol].Name);
            }
            foreach (DataGridViewRow row in dgv.Rows)
            {

                DataRow datarw = table.NewRow();

                for (int iCol = 0; iCol < dgv.Columns.Count; iCol++)
                {
                    datarw[iCol] = row.Cells[iCol].Value;
                }

                table.Rows.Add(datarw);
            }
            return table;
        }
        public static int CountGridWidth(DataGridView dgv)
        {
            int width = 0;
            foreach (DataGridViewColumn column in dgv.Columns)
                if (column.Visible == true)
                    width += column.Width;
            return width += 80;
        }
        public static int CountGridHeight(DataGridView dgv)
        {
            int height = 0;
            foreach (DataGridViewRow row in dgv.Rows)
                if (row.Visible == true)
                    height += Convert.ToInt32(row.Height * 0.6);
            return height += 500;
        }

        public static Size SetFormSize(DataGridView dgv)
        {
            int width = WinObjFunctions.CountGridWidth(dgv);
            int height = WinObjFunctions.CountGridHeight(dgv);
            return new Size(width, height);
        }
    }
}
