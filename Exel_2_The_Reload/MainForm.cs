using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Reflection;

namespace Exel_2_The_Reload
{
    public partial class MainForm : Form
    {
        TableFormatter formatter = new TableFormatter();
        private string path = "";

        public MainForm(string[] args)
        {
            InitializeComponent();

            if (args.Count() == 1)
                path = args[0];
            //path = @"C:\Users\Oleja\Desktop\qqq.xml";
            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic |
        BindingFlags.Instance | BindingFlags.SetProperty, null,
        DataGrid, new object[] { true });
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (path != "")
                LoadDocument(path);
            ClientSize = new Size(ClientSize.Width, WinObjFunctions.CountGridHeight(DataGrid));
            DataGrid.CellEndEdit += this.CellEditEndEvent;
            DataGrid.CellClick += this.CellClickedEvent;
            DataGrid.SelectionChanged += this.CellSelectedEvent;
            FormulaBox.KeyDown += this.FormulaBoxKeyPressedEvent;
            DataGrid.SelectionChanged += new EventHandler(this.FormulaBoxEditFinished);
            //FormulaBox.LostFocus += this.FormulaBoxLostFocusEvent;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosingEvent);
        }

        //REFRESH CONTENT FUNCTIONS
        private void RefreshDataGrid()
        {
            foreach (dynamic row in DataGrid.Rows)
                foreach (var elem in row.Cells)
                {
                    try
                    {
                        if (((Cell)elem.Tag).IsComment)
                        {
                            elem.Value = ((Cell)elem.Tag).Formulate;
                            continue;
                        }
                        formatter.RefreshCell(elem.Tag);
                        elem.Value = formatter.GetValue(elem.Tag);
                    }
                    catch (NullDivisionException ev)
                    {
                        MessageBox.Show("Null division was found in your formula.\r\nTry to fix your mistakes :)", "Error");
                    }
                }
        }
        private void CellSelectedEvent(object sender, EventArgs e)
        {
            if (DataGrid.SelectedCells.Count == 1)
            {
                if (DataGrid.SelectedCells[0].Tag == null)
                    return;
                var cell = DataGrid.SelectedCells[0].Tag as Cell;
                FormulaBox.Text = cell.Formulate;
            }
        }
        private void FormulaBoxKeyPressedEvent(object sender, KeyEventArgs e)
        {
            if (((e.KeyCode == Keys.Enter)) && (DataGrid.SelectedCells.Count == 1))
            {
                //DataGrid.Focus();
                try
                {
                    FormulaBox.Select(0, FormulaBox.Text.Length);
                    {
                        FormulaBox.SelectionColor = Color.Black;
                    }
                    FormulaBox.SelectionLength = 0;
                    ((Cell)DataGrid.SelectedCells[0].Tag).Formulate = FormulaBox.Text;
                    DataGrid.Focus();
                }
                catch (RecursionException ev)
                {
                    MessageBox.Show("There are one or more circular references where a formula of " + ev.var +
                        " cell refers to it`s own cell either directly or indirectly.\r\nTry to fix your mistakes :)", "Error");
                }
                catch (UnknownWordException ev)
                {
                    string _newFormula = FormulaBox.Text;
                    ev.position -= ev.word.Length;
                    FormulaBox.Text = _newFormula.Substring(0, ev.position);
                    RichTextBoxExtensions.AppendText(FormulaBox, ev.word, Color.Red);
                    RichTextBoxExtensions.AppendText(FormulaBox, _newFormula.Substring(ev.position + ev.word.Length), Color.Black);

                    MessageBox.Show("Unknown operator or variable " + ev.word +
                        " was found in your formula.\r\nTry to fix your mistakes :)", "Error");
                }
                catch (BadSyntaxException ev)
                {
                    string _newFormula = FormulaBox.Text;
                    ev.position -= (ev.token.Length - 1);
                    FormulaBox.Text = _newFormula.Substring(0, ev.position);
                    RichTextBoxExtensions.AppendText(FormulaBox, ev.token, Color.Red);
                    RichTextBoxExtensions.AppendText(FormulaBox, _newFormula.Substring(ev.position + ev.token.Length), Color.Black);

                    MessageBox.Show("Bad syntax at position " + ev.position.ToString() +
                        " was found in your formula.\r\nTry to fix your mistakes :)", "Error");
                }
                catch (UnknownSymbolException ev)
                {
                    string _newFormula = FormulaBox.Text;
                    FormulaBox.Text = _newFormula.Substring(0, ev.position);
                    RichTextBoxExtensions.AppendText(FormulaBox, ev._char.ToString(), Color.Red);
                    RichTextBoxExtensions.AppendText(FormulaBox, _newFormula.Substring(ev.position + 1), Color.Black);

                    MessageBox.Show("Unknown symbol at position " + ev.position.ToString() +
                        " was found in your formula.\r\nTry to fix your mistakes :)", "Error");
                }
                catch (NullDivisionException ev)
                {
                    MessageBox.Show("Null division was found in your formula.\r\nTry to fix your mistakes :)", "Error");
                }
                catch
                {
                    MessageBox.Show("I don`t even know what happened. \r\nUrgently call my creator!", "Error");
                }
                RefreshDataGrid();
            }
        }
        private void FormulaBoxEditFinished(object sender, EventArgs e)
        {
            if ((DataGrid.SelectedCells.Count == 1) && (DataGrid.SelectedCells[0].Tag != null))
            {
                string backUpFormula = ((Cell)DataGrid.SelectedCells[0].Tag).Formulate;
                try
                {
                    ((Cell)DataGrid.SelectedCells[0].Tag).Formulate = FormulaBox.Text;
                }
                catch (RecursionException ev)
                {
                    ((Cell)DataGrid.SelectedCells[0].Tag).Formulate = backUpFormula;
                    FormulaBox.Text = backUpFormula;
                    MessageBox.Show("There are one or more circular references where a formula of " + ev.var +
                        " cell refers to it`s own cell either directly or indirectly.\r\nTry to write the new formula again :)", "Error");
                }
                catch (UnknownWordException ev)
                {
                    ((Cell)DataGrid.SelectedCells[0].Tag).Formulate = backUpFormula;
                    FormulaBox.Text = backUpFormula;
                    MessageBox.Show("Unknown operator or variable " + ev.word +
                        " was found in your formula.\r\nTry to write the new formula again :)", "Error");
                }
                catch (NullDivisionException ev)
                {
                    MessageBox.Show("Null division was found in your formula.\r\nTry to fix your mistakes :)", "Error");
                }
                catch (BadSyntaxException ev)
                {
                    ((Cell)DataGrid.SelectedCells[0].Tag).Formulate = backUpFormula;
                    FormulaBox.Text = backUpFormula;
                    MessageBox.Show("Bad syntax at position " + ev.position.ToString() +
                        " was found in your formula.\r\nTry to write the new formula again :)", "Error");
                }
                catch
                {
                    ((Cell)DataGrid.SelectedCells[0].Tag).Formulate = backUpFormula;
                    FormulaBox.Text = backUpFormula;
                    MessageBox.Show("I don`t even know what happened. \r\nUrgently call my creator!", "Error");
                }

                RefreshDataGrid();
            }
        }
        private void CellClickedEvent(object sender, DataGridViewCellEventArgs e)
        {
            var cell = DataGrid.SelectedCells[0].Tag as Cell;
            FormulaBox.Text = cell.Formulate;
        }
        private void CellEditEndEvent(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                formatter.BindFormula(this.DataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag,
                    DataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                RefreshDataGrid();
            }
            catch { }
        }
        /**/
        //SAVE/LOAD DATAGRIDVIEW
        private void FormClosingEvent(object sender, EventArgs e)
        {
            if (MessageBox.Show("Save before ending session?", "Exit", MessageBoxButtons.YesNo) == DialogResult.Yes)

            {
                this.SaveDocument();
            }
            this.FormClosing -= FormClosingEvent;
            this.Close();
        }
        private void LoadDocument(string path)
        {
            try
            {
                this.path = path;
                DataSet dataSet = new DataSet();

                dataSet.ReadXml(path);
                DataGrid.Rows.Clear();
                DataGrid.Columns.Clear();
                DataGrid.Refresh();
                //DataGrid.DataSource = dataSet.Tables[0];
                for (int i = 0; i < dataSet.Tables[0].Columns.Count; i++)
                {
                    DataGrid.Columns.Add("C" + i.ToString(), "C" + i.ToString());
                    DataGrid.Columns[DataGrid.ColumnCount - 1].MinimumWidth = 100;
                }
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    DataGrid.Rows.Add();
                }
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < dataSet.Tables[0].Columns.Count; j++)
                    {
                        DataGrid.Rows[i].Cells[j].Tag = new Cell(dataSet.Tables[0].Rows[i][j].ToString(),
                            "R" + i.ToString() + "C" + j.ToString());
                    }
                }
                RefreshDataGrid();
                for (int i = 0; i < DataGrid.Rows.Count; i++)
                    DataGrid.Rows[i].HeaderCell.Value = "R" + i.ToString();
            }
            catch
            {
                MessageBox.Show("Loading error!","Error");
            }
        }
        private void SaveDocument()
        {
            try
            {
                DataGrid.EndEdit();
                DataGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                for (int i = 0; i < DataGrid.Rows.Count; i++)
                    for (int j = 0; j < DataGrid.ColumnCount; j++)
                        DataGrid[j, i].Value = formatter.GetFormula(DataGrid[j, i].Tag);
                (WinObjFunctions.ToDataTable(DataGrid, "default_table")).WriteXml(path);
            }
            catch
            {

            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDocument();
        }
        private void OpenFileClick(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LoadDocument(openFileDialog.FileName);
            }
        }
        /**/
        //ADDITINAL MENU STRIP FUNCTIONS
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            { Application.Exit(); }
            catch { }
        }
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Printer is not found!");
        }
        /**/
        //WORK WITH SIZE OF TABLE
        private void addColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string columnName = "C" + this.DataGrid.ColumnCount.ToString();
            DataGrid.Columns.Add(columnName, columnName);
            DataGrid.Columns[DataGrid.ColumnCount - 1].MinimumWidth = 100;
            for (int i = 0; i < DataGrid.Rows.Count; i++)
            {
                DataGrid.Rows[i].Cells[DataGrid.Columns.Count - 1].Value = 0;
                DataGrid.Rows[i].Cells[DataGrid.Columns.Count - 1].Tag = new Cell("0", "R" + i.ToString() + columnName);
            }
            ClientSize = new Size(ClientSize.Width, WinObjFunctions.CountGridHeight(DataGrid));
        }
        private void addRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var row = (DataGridViewRow)DataGrid.RowTemplate.Clone();
            DataGrid.Rows.Add(row);
            for (int i = 0; i < DataGrid.Rows.Count; i++)
                DataGrid.Rows[i].HeaderCell.Value = "R" + i.ToString();
            for (int i = 0; i < DataGrid.Columns.Count; i++)
            {
                DataGrid.Rows[DataGrid.Rows.Count - 1].Cells[i].Value = 0;
                DataGrid.Rows[DataGrid.Rows.Count - 1].Cells[i].Tag =
                    new Cell("0", "R" + (DataGrid.Rows.Count - 1).ToString() + "C" + i.ToString());
            }
            ClientSize = new Size(ClientSize.Width, WinObjFunctions.CountGridHeight(DataGrid));
        }

        private void removeColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DataGrid.SelectedCells.Count == 0)
                return;
            int position = DataGrid.SelectedCells[0].ColumnIndex;

            foreach (DataGridViewRow row in DataGrid.Rows)
                foreach (DataGridViewCell elem in row.Cells)
                {
                    var cell = elem.Tag;
                    formatter.CellColumnDecrease(cell, position);
                }

            DataGrid.Columns.RemoveAt(position);

            for (int i = position; i < DataGrid.Columns.Count; i++)
                DataGrid.Columns[i].HeaderText = "C" + i.ToString();
            RefreshDataGrid();

            ClientSize = new Size(ClientSize.Width, WinObjFunctions.CountGridHeight(DataGrid));
        }
        private void removeRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DataGrid.SelectedCells.Count == 0)
                return;
            int position = DataGrid.SelectedCells[0].RowIndex;

            foreach (DataGridViewRow row in DataGrid.Rows)
                foreach (DataGridViewCell elem in row.Cells)
                {
                    var cell = elem.Tag;
                    formatter.CellRowDecrease(cell, position);
                }

            DataGrid.Rows.RemoveAt(position);

            for (int i = 0; i < DataGrid.Rows.Count; i++)
                DataGrid.Rows[i].HeaderCell.Value = "R" + i.ToString();
            RefreshDataGrid();

            ClientSize = new Size(ClientSize.Width, WinObjFunctions.CountGridHeight(DataGrid));
        }

        private void insertColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DataGrid.SelectedCells.Count == 0)
                return;
            int position = DataGrid.SelectedCells[0].ColumnIndex;
            string columnName = "C" + position.ToString();

            foreach (DataGridViewRow row in DataGrid.Rows)
                foreach (DataGridViewCell elem in row.Cells)
                {
                    var cell = elem.Tag;
                    formatter.CellColumnIncrease(cell, position);
                }

            var column = new DataGridViewColumn();
            column.CellTemplate = new DataGridViewTextBoxCell();

            DataGrid.Columns.Insert(position, column);
            DataGrid.Columns[position].MinimumWidth = 100;

            for (int i = position; i < DataGrid.Columns.Count; i++)
                DataGrid.Columns[i].HeaderText = "C" + i.ToString();

            for (int i = 0; i < DataGrid.Rows.Count; i++)
            {
                DataGrid.Rows[i].Cells[position].Value = 0;
                DataGrid.Rows[i].Cells[position].Tag = new Cell("0", "R" + i.ToString() + columnName);
            }
            RefreshDataGrid();

            ClientSize = new Size(ClientSize.Width, WinObjFunctions.CountGridHeight(DataGrid));
        }
        private void insertRowToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (DataGrid.SelectedCells.Count == 0)
                return;
            int position = DataGrid.SelectedCells[0].RowIndex;
            string rowName = "R" + position.ToString();

            foreach (DataGridViewRow row in DataGrid.Rows)
                foreach (DataGridViewCell elem in row.Cells)
                {
                    var cell = elem.Tag;
                    formatter.CellRowIncrease(cell, position);
                }

            var newRow = (DataGridViewRow)DataGrid.RowTemplate.Clone();

            DataGrid.Rows.Insert(position, newRow);

            for (int i = 0; i < DataGrid.Rows.Count; i++)
                DataGrid.Rows[i].HeaderCell.Value = "R" + i.ToString();

            for (int i = 0; i < DataGrid.Columns.Count; i++)
            {
                DataGrid.Rows[position].Cells[i].Value = 0;
                DataGrid.Rows[position].Cells[i].Tag = new Cell("0", rowName + "C" + i.ToString());
            }
            RefreshDataGrid();

            ClientSize = new Size(ClientSize.Width, WinObjFunctions.CountGridHeight(DataGrid));
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string information = "Поради :" + '\n' +
                "- Вказуйте назви клітинок у формулах за шаблоном R0C0-RNCN" + '\n' +
                "- Кнопка Insert вставляє рядочок вище видленої клітинки чи колонку лівіше виділеної клітинки" + '\n' +
                "- Кнопка Delete видаляє рядок чи стовпчик з виділеною клітинкою" + '\n' +
                "- Доступні операції : + - /(аналог DIV) * MIN(,) MAX(,) % (аналог MOD) ^" + '\n' +
                "- Запитайте у розробника щодо проблем, які виникли в процесі використання" + '\n' + '\n' +
                "Боровик Олег . К-26 . 2017 (c)";
            MessageBox.Show(information, "Інформація");
        }
        /**/
        /*CODE BLOCK*/
        private void runCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Model mod = new Model(CodeBox.Text);
            try
            {
                mod.Block().Calculate();
                RefreshDataGrid();
            }
            catch (StackOverflowException ex)
            {
                MessageBox.Show("Infinity loops are bad!","Error");
            }

            catch (RecursionException ev)
            {
                MessageBox.Show("There are one or more circular references where a determination of " + ev.var +
                    " cell refers to it`s own cell either directly or indirectly.\r\nTry to fix your mistakes :)", "Error");
            }
            catch (UnknownWordException ev)
            {
                string _newFormula = CodeBox.Text;
                ev.position -= ev.word.Length;
                CodeBox.Text = _newFormula.Substring(0, ev.position);
                RichTextBoxExtensions.AppendText(CodeBox, ev.word, Color.Red);
                RichTextBoxExtensions.AppendText(CodeBox, _newFormula.Substring(ev.position + ev.word.Length), Color.Black);

                MessageBox.Show("Unknown operator or variable " + ev.word +
                    " was found in your code.\r\nTry to fix your mistakes :)", "Error");
            }
            catch (BadSyntaxException ev)
            {
                string _newFormula = CodeBox.Text;
                ev.position -= (ev.token.Length - 1);
                CodeBox.Text = _newFormula.Substring(0, ev.position);
                RichTextBoxExtensions.AppendText(CodeBox, ev.token, Color.Red);
                RichTextBoxExtensions.AppendText(CodeBox, _newFormula.Substring(ev.position + ev.token.Length), Color.Black);

                MessageBox.Show("Bad syntax at position " + ev.position.ToString() +
                    " was found in your code.\r\nTry to fix your mistakes :)", "Error");
            }
            catch (UnknownSymbolException ev)
            {
                string _newFormula = CodeBox.Text;
                CodeBox.Text = _newFormula.Substring(0, ev.position);
                RichTextBoxExtensions.AppendText(CodeBox, ev._char.ToString(), Color.Red);
                RichTextBoxExtensions.AppendText(CodeBox, _newFormula.Substring(ev.position + 1), Color.Black);

                MessageBox.Show("Unknown symbol at position " + ev.position.ToString() +
                    " was found in your code.\r\nTry to fix your mistakes :)", "Error");
            }
            catch (NullDivisionException ev)
            {
                MessageBox.Show("Null division was found in your code.\r\nTry to fix your mistakes :)", "Error");
            }
            catch
            {
                MessageBox.Show("I don`t even know what happened. \r\nUrgently call my creator!", "Error");
            }
        }

        private void CodeBox_TextChanged(object sender, EventArgs e)
        {

        }
        /**/
    }
}