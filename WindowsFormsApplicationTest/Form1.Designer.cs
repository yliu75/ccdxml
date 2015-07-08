using System.IO;
using System.Xml.Linq;
using System.Windows.Forms;

namespace WindowsFormsApplicationTest {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        public static string path = @"C:\Users\Administrator\Documents\C#\ClinicalDocument.xml";

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


        //add treeNode recursively
        /*
        public static void addTreeNode(System.Windows.Forms.TreeNode root,XElement ele,TreeView tv){
            if(root.Parent!=null) {
                System.Windows.Forms.TreeNode treeNode = new System.Windows.Forms.TreeNode(cutHead(ele.Name.ToString()),new System.Windows.Forms.TreeNode[] {
            root.Parent});
            } else {
                TreeNode treeNode = new System.Windows.Forms.TreeNode(cutHead(ele.Name.ToString()),new System.Windows.Forms.TreeNode[] {
            tv.Nodes});
            }
            treeNode.Name=cutHead(ele.Name.ToString());
            treeNode.Text=cutHead(ele.Name.ToString());
            if(root.Parent!=null) {
                root.Parent.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            root});
            } else {
                tv.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            root});
            }

            foreach(XElement node in ele.Elements())
                addTreeNode(treeNode,node,tv);

        }*/
        /*
        //function that add nodes to treeviews form the xml file
        public static void addTreeNode(TreeNode parentNode,XElement ele) {
            if(parentNode==null||ele==null||ele.Name==null||ele.Name.ToString()==null)
                return;
            string nodeName = cutHead(ele.Name.ToString());
            if(nodeName==null)
                return;
                var treeN = parentNode.Nodes.Add(cutHead(ele.Name.ToString()));
            foreach(XElement node in ele.Elements())
                addTreeNode(treeN,node);
        }
        */
        private void InitializeComponent() {
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label_title = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textbox_attributes = new System.Windows.Forms.TextBox();
            this.label_content = new System.Windows.Forms.Label();
            this.textbox_content = new System.Windows.Forms.TextBox();
            this.textbox_search = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button_expandAll = new System.Windows.Forms.Button();
            this.button_expand = new System.Windows.Forms.Button();
            this.label_itemFound = new System.Windows.Forms.Label();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.label_pending = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(12, 67);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(321, 452);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // label_title
            // 
            this.label_title.AccessibleName = "label";
            this.label_title.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.label_title.AutoSize = true;
            this.label_title.BackColor = System.Drawing.SystemColors.Control;
            this.label_title.Font = new System.Drawing.Font("Georgia", 42F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_title.Location = new System.Drawing.Point(368, 12);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(200, 65);
            this.label_title.TabIndex = 1;
            this.label_title.Text = "default";
            this.label_title.Click += new System.EventHandler(this.label_title_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(381, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Attributes：";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // textbox_attributes
            // 
            this.textbox_attributes.Location = new System.Drawing.Point(383, 127);
            this.textbox_attributes.Multiline = true;
            this.textbox_attributes.Name = "textbox_attributes";
            this.textbox_attributes.Size = new System.Drawing.Size(232, 92);
            this.textbox_attributes.TabIndex = 5;
            this.textbox_attributes.Text = " ";
            this.textbox_attributes.TextChanged += new System.EventHandler(this.textbox_attributes_TextChanged);
            // 
            // label_content
            // 
            this.label_content.AutoSize = true;
            this.label_content.Location = new System.Drawing.Point(383, 237);
            this.label_content.Name = "label_content";
            this.label_content.Size = new System.Drawing.Size(47, 12);
            this.label_content.TabIndex = 6;
            this.label_content.Text = "Content";
            // 
            // textbox_content
            // 
            this.textbox_content.Location = new System.Drawing.Point(383, 266);
            this.textbox_content.Multiline = true;
            this.textbox_content.Name = "textbox_content";
            this.textbox_content.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textbox_content.Size = new System.Drawing.Size(498, 253);
            this.textbox_content.TabIndex = 7;
            this.textbox_content.TextChanged += new System.EventHandler(this.textbox_content_TextChanged);
            // 
            // textbox_search
            // 
            this.textbox_search.Location = new System.Drawing.Point(12, 34);
            this.textbox_search.Name = "textbox_search";
            this.textbox_search.Size = new System.Drawing.Size(236, 21);
            this.textbox_search.TabIndex = 8;
            this.textbox_search.TextChanged += new System.EventHandler(this.textbox_search_TextChanged);
            this.textbox_search.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textbox_search_EnterClicked);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(258, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_expandAll
            // 
            this.button_expandAll.Location = new System.Drawing.Point(163, 531);
            this.button_expandAll.Name = "button_expandAll";
            this.button_expandAll.Size = new System.Drawing.Size(170, 23);
            this.button_expandAll.TabIndex = 10;
            this.button_expandAll.Text = "Expend All Nodes";
            this.button_expandAll.UseVisualStyleBackColor = true;
            this.button_expandAll.Click += new System.EventHandler(this.button_expandAll_Click);
            // 
            // button_expand
            // 
            this.button_expand.Location = new System.Drawing.Point(13, 530);
            this.button_expand.Name = "button_expand";
            this.button_expand.Size = new System.Drawing.Size(144, 23);
            this.button_expand.TabIndex = 11;
            this.button_expand.Text = "Expend Node";
            this.button_expand.UseVisualStyleBackColor = true;
            this.button_expand.Click += new System.EventHandler(this.button_expand_Click);
            // 
            // label_itemFound
            // 
            this.label_itemFound.AutoSize = true;
            this.label_itemFound.BackColor = System.Drawing.Color.White;
            this.label_itemFound.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label_itemFound.Location = new System.Drawing.Point(145, 38);
            this.label_itemFound.Name = "label_itemFound";
            this.label_itemFound.Size = new System.Drawing.Size(11, 12);
            this.label_itemFound.TabIndex = 12;
            this.label_itemFound.Text = " ";
            this.label_itemFound.Click += new System.EventHandler(this.label_itemFound_Click);
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(621, 127);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(260, 92);
            this.richTextBox2.TabIndex = 14;
            this.richTextBox2.Text = "";
            this.richTextBox2.TextChanged += new System.EventHandler(this.richTextBox2_TextChanged);
            // 
            // label_pending
            // 
            this.label_pending.AutoSize = true;
            this.label_pending.BackColor = System.Drawing.Color.White;
            this.label_pending.Font = new System.Drawing.Font("Segoe UI Symbol", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_pending.ForeColor = System.Drawing.Color.Black;
            this.label_pending.Location = new System.Drawing.Point(87, 266);
            this.label_pending.Name = "label_pending";
            this.label_pending.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_pending.Size = new System.Drawing.Size(139, 40);
            this.label_pending.TabIndex = 15;
            this.label_pending.Text = "Pending...";
            this.label_pending.Visible = false;
            this.label_pending.Click += new System.EventHandler(this.label_pending_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 566);
            this.Controls.Add(this.label_pending);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.label_itemFound);
            this.Controls.Add(this.button_expand);
            this.Controls.Add(this.button_expandAll);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textbox_search);
            this.Controls.Add(this.textbox_content);
            this.Controls.Add(this.label_content);
            this.Controls.Add(this.textbox_attributes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_title);
            this.Controls.Add(this.treeView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TreeView treeView1;
        public Label label_title;
        public Label l;
        private Label label1;
        private TextBox textbox_attributes;
        private Label label_content;
        private TextBox textbox_content;
        private TextBox textbox_search;
        private Button button1;
        private Button button_expandAll;
        private Button button_expand;
        private Label label_itemFound;
        private RichTextBox richTextBox2;
        private Label label_pending;
    }
}

