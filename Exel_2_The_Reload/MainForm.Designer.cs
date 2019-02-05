namespace Exel_2_The_Reload
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.DataGrid = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addColumnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.insertColumnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertRowToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.removeColumnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.FormulaBox = new System.Windows.Forms.RichTextBox();
            this.CodeBox = new System.Windows.Forms.RichTextBox();
            this.serviceController1 = new System.ServiceProcess.ServiceController();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataGrid
            // 
            this.DataGrid.AllowUserToAddRows = false;
            this.DataGrid.AllowUserToDeleteRows = false;
            this.DataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGrid.Location = new System.Drawing.Point(3, 28);
            this.DataGrid.Name = "DataGrid";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.DataGrid.RowTemplate.Height = 24;
            this.DataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DataGrid.Size = new System.Drawing.Size(963, 159);
            this.DataGrid.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.runCodeToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(969, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenFile,
            this.toolStripSeparator,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.printToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // OpenFile
            // 
            this.OpenFile.Image = ((System.Drawing.Image)(resources.GetObject("OpenFile.Image")));
            this.OpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenFile.Name = "OpenFile";
            this.OpenFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.OpenFile.Size = new System.Drawing.Size(173, 26);
            this.OpenFile.Text = "&Open";
            this.OpenFile.Click += new System.EventHandler(this.OpenFileClick);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(170, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(173, 26);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(170, 6);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripMenuItem.Image")));
            this.printToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.printToolStripMenuItem.Size = new System.Drawing.Size(173, 26);
            this.printToolStripMenuItem.Text = "&Print";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(170, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(173, 26);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addColumnToolStripMenuItem,
            this.addRowToolStripMenuItem,
            this.toolStripSeparator7,
            this.insertColumnToolStripMenuItem,
            this.insertRowToolStripMenuItem1,
            this.toolStripSeparator6,
            this.removeColumnToolStripMenuItem,
            this.removeRowToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(56, 24);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // addColumnToolStripMenuItem
            // 
            this.addColumnToolStripMenuItem.Name = "addColumnToolStripMenuItem";
            this.addColumnToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.addColumnToolStripMenuItem.Text = "Add column to end";
            this.addColumnToolStripMenuItem.Click += new System.EventHandler(this.addColumnToolStripMenuItem_Click);
            // 
            // addRowToolStripMenuItem
            // 
            this.addRowToolStripMenuItem.Name = "addRowToolStripMenuItem";
            this.addRowToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.addRowToolStripMenuItem.Text = "Add row to end";
            this.addRowToolStripMenuItem.Click += new System.EventHandler(this.addRowToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(209, 6);
            // 
            // insertColumnToolStripMenuItem
            // 
            this.insertColumnToolStripMenuItem.Name = "insertColumnToolStripMenuItem";
            this.insertColumnToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.insertColumnToolStripMenuItem.Text = "Insert column";
            this.insertColumnToolStripMenuItem.Click += new System.EventHandler(this.insertColumnToolStripMenuItem_Click);
            // 
            // insertRowToolStripMenuItem1
            // 
            this.insertRowToolStripMenuItem1.Name = "insertRowToolStripMenuItem1";
            this.insertRowToolStripMenuItem1.Size = new System.Drawing.Size(212, 26);
            this.insertRowToolStripMenuItem1.Text = "Insert row";
            this.insertRowToolStripMenuItem1.Click += new System.EventHandler(this.insertRowToolStripMenuItem1_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(209, 6);
            // 
            // removeColumnToolStripMenuItem
            // 
            this.removeColumnToolStripMenuItem.Name = "removeColumnToolStripMenuItem";
            this.removeColumnToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.removeColumnToolStripMenuItem.Text = "Delete column";
            this.removeColumnToolStripMenuItem.Click += new System.EventHandler(this.removeColumnToolStripMenuItem_Click);
            // 
            // removeRowToolStripMenuItem
            // 
            this.removeRowToolStripMenuItem.Name = "removeRowToolStripMenuItem";
            this.removeRowToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.removeRowToolStripMenuItem.Text = "Delete row";
            this.removeRowToolStripMenuItem.Click += new System.EventHandler(this.removeRowToolStripMenuItem_Click);
            // 
            // runCodeToolStripMenuItem
            // 
            this.runCodeToolStripMenuItem.Name = "runCodeToolStripMenuItem";
            this.runCodeToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.runCodeToolStripMenuItem.Text = "Run code";
            this.runCodeToolStripMenuItem.Click += new System.EventHandler(this.runCodeToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator5,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(131, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(134, 26);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = " ";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.DataGrid, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.FormulaBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.CodeBox, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 28);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 315F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(969, 505);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // FormulaBox
            // 
            this.FormulaBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FormulaBox.HideSelection = false;
            this.FormulaBox.Location = new System.Drawing.Point(3, 3);
            this.FormulaBox.Multiline = false;
            this.FormulaBox.Name = "FormulaBox";
            this.FormulaBox.Size = new System.Drawing.Size(963, 19);
            this.FormulaBox.TabIndex = 1;
            this.FormulaBox.Text = "";
            // 
            // CodeBox
            // 
            this.CodeBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CodeBox.Font = new System.Drawing.Font("Perpetua Titling MT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CodeBox.Location = new System.Drawing.Point(3, 193);
            this.CodeBox.Name = "CodeBox";
            this.CodeBox.Size = new System.Drawing.Size(963, 309);
            this.CodeBox.TabIndex = 2;
            this.CodeBox.Text = "";
            this.CodeBox.TextChanged += new System.EventHandler(this.CodeBox_TextChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 533);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Exel : Reborn";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DataGrid;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addColumnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addRowToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripMenuItem removeColumnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem insertColumnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertRowToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.ServiceProcess.ServiceController serviceController1;
        private System.Windows.Forms.RichTextBox FormulaBox;
        private System.Windows.Forms.ToolStripMenuItem runCodeToolStripMenuItem;
        private System.Windows.Forms.RichTextBox CodeBox;
    }
}

