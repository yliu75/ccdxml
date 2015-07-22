#define SEARCH_ON

using System.IO;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Reflection;
namespace WindowsFormsApplicationTest {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        public static Stream path;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing&&(components!=null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        /// 

        private void InitializeComponent() {
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label_title = new System.Windows.Forms.Label();
            this.textbox_content = new System.Windows.Forms.RichTextBox();
            this.textbox_search = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button_expandAll = new System.Windows.Forms.Button();
            this.button_expand = new System.Windows.Forms.Button();
            this.label_itemFound = new System.Windows.Forms.Label();
            this.label_pending = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expendAllNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutAthenaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.label_copyright = new System.Windows.Forms.Label();
            this.historyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.history0ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.history1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.history2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.history3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.history4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.history5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listBox_history = new System.Windows.Forms.ListBox();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.Location = new System.Drawing.Point(16, 151);
            this.treeView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(427, 601);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // label_title
            // 
            this.label_title.AccessibleName = "label";
            this.label_title.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.label_title.AutoSize = true;
            this.label_title.BackColor = System.Drawing.Color.Transparent;
            this.label_title.Font = new System.Drawing.Font("Microsoft YaHei", 35.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_title.ForeColor = System.Drawing.Color.Gray;
            this.label_title.Location = new System.Drawing.Point(505, 43);
            this.label_title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(557, 60);
            this.label_title.TabIndex = 1;
            this.label_title.Text = "Please open an xml file.";
            this.label_title.Click += new System.EventHandler(this.label_title_Click);
            // 
            // textbox_content
            // 
            this.textbox_content.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textbox_content.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textbox_content.Location = new System.Drawing.Point(8, 32);
            this.textbox_content.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textbox_content.Name = "textbox_content";
            this.textbox_content.Size = new System.Drawing.Size(751, 371);
            this.textbox_content.TabIndex = 7;
            this.textbox_content.Text = "";
            // 
            // textbox_search
            // 
            this.textbox_search.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textbox_search.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textbox_search.Location = new System.Drawing.Point(16, 119);
            this.textbox_search.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textbox_search.Name = "textbox_search";
            this.textbox_search.ReadOnly = true;
            this.textbox_search.ShortcutsEnabled = false;
            this.textbox_search.Size = new System.Drawing.Size(314, 27);
            this.textbox_search.TabIndex = 8;
            this.textbox_search.TextChanged += new System.EventHandler(this.textbox_search_TextChanged);
            this.textbox_search.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textbox_search_EnterClicked);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(343, 119);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 27);
            this.button1.TabIndex = 9;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_expandAll
            // 
            this.button_expandAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_expandAll.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_expandAll.Location = new System.Drawing.Point(216, 760);
            this.button_expandAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_expandAll.Name = "button_expandAll";
            this.button_expandAll.Size = new System.Drawing.Size(227, 44);
            this.button_expandAll.TabIndex = 10;
            this.button_expandAll.Text = "Expend All Nodes";
            this.button_expandAll.UseVisualStyleBackColor = true;
            this.button_expandAll.Click += new System.EventHandler(this.button_expandAll_Click);
            // 
            // button_expand
            // 
            this.button_expand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_expand.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_expand.Location = new System.Drawing.Point(16, 760);
            this.button_expand.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_expand.Name = "button_expand";
            this.button_expand.Size = new System.Drawing.Size(192, 44);
            this.button_expand.TabIndex = 11;
            this.button_expand.Text = "Expend Node";
            this.button_expand.UseVisualStyleBackColor = true;
            this.button_expand.Click += new System.EventHandler(this.button_expand_Click);
            // 
            // label_itemFound
            // 
            this.label_itemFound.AutoSize = true;
            this.label_itemFound.BackColor = System.Drawing.Color.White;
            this.label_itemFound.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_itemFound.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label_itemFound.Location = new System.Drawing.Point(16, 89);
            this.label_itemFound.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_itemFound.Name = "label_itemFound";
            this.label_itemFound.Size = new System.Drawing.Size(12, 17);
            this.label_itemFound.TabIndex = 12;
            this.label_itemFound.Text = " ";
            // 
            // label_pending
            // 
            this.label_pending.AutoSize = true;
            this.label_pending.BackColor = System.Drawing.Color.Transparent;
            this.label_pending.Font = new System.Drawing.Font("Microsoft YaHei", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_pending.ForeColor = System.Drawing.Color.Black;
            this.label_pending.Location = new System.Drawing.Point(125, 443);
            this.label_pending.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_pending.Name = "label_pending";
            this.label_pending.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_pending.Size = new System.Drawing.Size(154, 38);
            this.label_pending.TabIndex = 15;
            this.label_pending.Text = "Pending...";
            this.label_pending.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.historyToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1299, 27);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.expendAllNodesToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(42, 21);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // expendAllNodesToolStripMenuItem
            // 
            this.expendAllNodesToolStripMenuItem.Name = "expendAllNodesToolStripMenuItem";
            this.expendAllNodesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.expendAllNodesToolStripMenuItem.Text = "Expend All Nodes";
            this.expendAllNodesToolStripMenuItem.Click += new System.EventHandler(this.expendAllNodesToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1,
            this.aboutAthenaToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(47, 21);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(155, 22);
            this.helpToolStripMenuItem1.Text = "View Help";
            // 
            // aboutAthenaToolStripMenuItem
            // 
            this.aboutAthenaToolStripMenuItem.Name = "aboutAthenaToolStripMenuItem";
            this.aboutAthenaToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.aboutAthenaToolStripMenuItem.Text = "About Athena";
            this.aboutAthenaToolStripMenuItem.Click += new System.EventHandler(this.aboutAthenaToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.richTextBox1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.groupBox1.Location = new System.Drawing.Point(511, 136);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(767, 211);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Attributes";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(8, 32);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(751, 167);
            this.richTextBox1.TabIndex = 19;
            this.richTextBox1.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.webBrowser1);
            this.groupBox2.Controls.Add(this.textbox_content);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.groupBox2.Location = new System.Drawing.Point(511, 355);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(767, 411);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Content";
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(8, 32);
            this.webBrowser1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(27, 27);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.Size = new System.Drawing.Size(751, 371);
            this.webBrowser1.TabIndex = 8;
            this.webBrowser1.Visible = false;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // label_copyright
            // 
            this.label_copyright.AutoSize = true;
            this.label_copyright.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label_copyright.Location = new System.Drawing.Point(892, 805);
            this.label_copyright.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_copyright.Name = "label_copyright";
            this.label_copyright.Size = new System.Drawing.Size(392, 16);
            this.label_copyright.TabIndex = 19;
            this.label_copyright.Text = "Copyright 2015 © By Ames It and Numeric Solution";
            this.label_copyright.Click += new System.EventHandler(this.label1_Click);
            // 
            // historyToolStripMenuItem
            // 
            this.historyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.history0ToolStripMenuItem,
            this.history1ToolStripMenuItem,
            this.history2ToolStripMenuItem,
            this.history3ToolStripMenuItem,
            this.history4ToolStripMenuItem,
            this.history5ToolStripMenuItem});
            this.historyToolStripMenuItem.Name = "historyToolStripMenuItem";
            this.historyToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.historyToolStripMenuItem.Text = "History";
            this.historyToolStripMenuItem.Click += new System.EventHandler(this.historyToolStripMenuItem_Click);
            // 
            // history0ToolStripMenuItem
            // 
            this.history0ToolStripMenuItem.Name = "history0ToolStripMenuItem";
            this.history0ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.history0ToolStripMenuItem.Text = "History0";
            this.history0ToolStripMenuItem.Click += new System.EventHandler(this.history0ToolStripMenuItem_Click);
            // 
            // history1ToolStripMenuItem
            // 
            this.history1ToolStripMenuItem.Name = "history1ToolStripMenuItem";
            this.history1ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.history1ToolStripMenuItem.Text = "History1";
            this.history1ToolStripMenuItem.Click += new System.EventHandler(this.history1ToolStripMenuItem_Click);
            // 
            // history2ToolStripMenuItem
            // 
            this.history2ToolStripMenuItem.Name = "history2ToolStripMenuItem";
            this.history2ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.history2ToolStripMenuItem.Text = "History2";
            this.history2ToolStripMenuItem.Click += new System.EventHandler(this.history2ToolStripMenuItem_Click);
            // 
            // history3ToolStripMenuItem
            // 
            this.history3ToolStripMenuItem.Name = "history3ToolStripMenuItem";
            this.history3ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.history3ToolStripMenuItem.Text = "History3";
            this.history3ToolStripMenuItem.Click += new System.EventHandler(this.history3ToolStripMenuItem_Click);
            // 
            // history4ToolStripMenuItem
            // 
            this.history4ToolStripMenuItem.Name = "history4ToolStripMenuItem";
            this.history4ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.history4ToolStripMenuItem.Text = "History4";
            this.history4ToolStripMenuItem.Click += new System.EventHandler(this.history4ToolStripMenuItem_Click);
            // 
            // history5ToolStripMenuItem
            // 
            this.history5ToolStripMenuItem.Name = "history5ToolStripMenuItem";
            this.history5ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.history5ToolStripMenuItem.Text = "History5";
            this.history5ToolStripMenuItem.Click += new System.EventHandler(this.history5ToolStripMenuItem_Click);
            // 
            // listBox_history
            // 
            this.listBox_history.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F);
            this.listBox_history.FormattingEnabled = true;
            this.listBox_history.ItemHeight = 20;
            this.listBox_history.Location = new System.Drawing.Point(20, 143);
            this.listBox_history.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listBox_history.Name = "listBox_history";
            this.listBox_history.Size = new System.Drawing.Size(309, 124);
            this.listBox_history.TabIndex = 20;
            this.listBox_history.Visible = false;
            this.listBox_history.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1299, 833);
            this.Controls.Add(this.listBox_history);
            this.Controls.Add(this.label_copyright);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label_pending);
            this.Controls.Add(this.label_itemFound);
            this.Controls.Add(this.button_expand);
            this.Controls.Add(this.button_expandAll);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textbox_search);
            this.Controls.Add(this.label_title);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("SimSun", 12F);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "XMLExplorer(v1.0.1.0Beta)";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        public TreeView treeView1;
        public Label label_title;
        public Label l;
        private RichTextBox textbox_content;
        private TextBox textbox_search;
        private Button button1;
        private Button button_expandAll;
        private Button button_expand;
        private Label label_itemFound;
        private Label label_pending;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private OpenFileDialog openFileDialog1;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem1;
        private ToolStripMenuItem aboutAthenaToolStripMenuItem;
        private ToolStripMenuItem expendAllNodesToolStripMenuItem;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private RichTextBox richTextBox1;
        private WebBrowser webBrowser1;
        private Label label_copyright;
        private ToolStripMenuItem historyToolStripMenuItem;
        private ToolStripMenuItem history0ToolStripMenuItem;
        private ToolStripMenuItem history1ToolStripMenuItem;
        private ToolStripMenuItem history2ToolStripMenuItem;
        private ToolStripMenuItem history3ToolStripMenuItem;
        private ToolStripMenuItem history4ToolStripMenuItem;
        private ToolStripMenuItem history5ToolStripMenuItem;
        private ListBox listBox_history;
    }
}

