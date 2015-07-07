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

namespace WindowsFormsApplicationTest {
    public partial class Form1:Form {

        //==========================================================================================
        //below this line is my own function definition
        string fileName;
        string labelText;
        string searchText;
        int itemFound;
        int colorPtr = 0;
        int[] colorList = { 0,191,255,
                           69,139,116,
                          205,133,  0,
                          199, 21,133,
                          138, 43,226,
                           65,105,225
        };
        Label SelectedLabel = new Label();
        TreeNode firstNode;
        TreeNode currentSelectedNode;
        //get the filename from a path string
        public static string getFileName(string filePath) {
            int i = 0;
            for(;i<filePath.Length;i++)
                if(filePath[i]=='\\') {
                    filePath=filePath.Remove(0,i+1);
                    i=0;
                }
            return filePath;
        }

        public static int getHead(string s) {
            if(s=="")
                return 0;
            int i = 0;
            while(s[i++]!='}'&&i<s.Length)
                ;
            return i;
        }
        ///cut the head{urn:hl7-org:v3} of the XElement.Name
        public static string cutHead(string s) {
            //string result;
            if(s==null||getHead(s)==0) return s;
            else return s.Remove(0,getHead(s));
        }

        //function that add nodes to treeviews form the xml file
        public static void addTreeNode(TreeNode parentNode,XElement ele) {
            if(parentNode==null||ele==null||ele.Name==null||ele.Name.ToString()==null)
                return;
            string nodeName = cutHead(ele.Name.ToString());
            if(nodeName==null)
                return;
            var treeN = parentNode.Nodes.Add(cutHead(ele.Name.ToString()));
            treeN.Tag=ele;
            foreach(XElement node in ele.Elements())
                addTreeNode(treeN,node);
        }
        //expendnode and all its parent,grandparent, grandgrandparents,etc..
        public static void expendNode(TreeNode child) {
            child.Expand();
            if(child.Parent!=null) expendNode(child.Parent);
        }
        //color restore
        public static void colorRestore(TreeNode tn) {
            tn.BackColor=Color.FromArgb(255,255,255);
            if(tn.Nodes!=null) {
                foreach(TreeNode t in tn.Nodes)
                    colorRestore(t);
            }
        }
        //this function search the tree for a string target,and color it to green
        public void searchTreeView(TreeNode root,string target) {
            if(root==null) return;
            if(root.Text.Equals(target,StringComparison.OrdinalIgnoreCase)) {
                expendNode(root);
                //root.Expand();
                root.BackColor=Color.FromArgb(colorList[colorPtr],colorList[colorPtr+1],colorList[colorPtr+2]);

                itemFound++;
            } else {
                foreach(TreeNode tn in root.Nodes)
                    searchTreeView(tn,target);
            }
        }
        //check button text
        public void checkButtonText() {
            if(this.currentSelectedNode.IsExpanded) this.button_expand.Text="Collapse Node";
            else this.button_expand.Text="Expand Node";
            if(firstNode.IsExpanded) button_expandAll.Text="Collapse All Node";
            else button_expandAll.Text="Expand All Node";
            //check if the textbox need a scroll bar
            if(this.textbox_content.Text.Length>=500)
                this.textbox_content.ScrollBars=ScrollBars.Vertical;
            else this.textbox_content.ScrollBars=ScrollBars.None;
        }
        //advanced search 
        //can search multiple item at the same time
        public void advancedSearch(TreeNode root,string targetString) {
            int i = targetString.Length, stringPtr = i;
            //check if there is space
            bool spaceFlag = false;
            while(i-->0) {
                if(targetString[i]==' ') {
                    spaceFlag=true;
                    string subString = targetString.Substring(i+1,stringPtr-i-1);
                    stringPtr=i;
                    searchTreeView(root,subString);
                    colorPtr+=3;
                    if(colorPtr>17) colorPtr=0;
                }
            }
            if(!spaceFlag) {
                searchTreeView(root,targetString);
                colorPtr+=3;
                if(colorPtr>17) colorPtr=0;
            } else {
                searchTreeView(root,targetString.Substring(i+1,stringPtr));
                colorPtr+=3;
                if(colorPtr>17) colorPtr=0;
            }
        }
        public void showPending() {
            this.label_pending.Visible=true;
            this.treeView1.Visible=false;
        }
        public void hidePending() {
            this.label_pending.Visible=false;
            this.treeView1.Visible=true;
        }


        //----------------------------------------------------
        //bug fixed
        //Bug 1
        //multiple enter pressed or multiple search clicked
        //if(textbox_search.Text.Equals(searchText,StringComparison.OrdinalIgnoreCase)) return;
        //

        //end of bugs
        //----------------------------------------------------
        //end of definition
        //==========================================================================================
        public Form1() {
            labelText="defaultForm1";
            InitializeComponent();

            //load the xml file and add nodes to the tree view
            StreamReader sr = new StreamReader(path,true);
            XDocument xdoc = XDocument.Load(sr);
            fileName=getFileName(path);
            firstNode=this.treeView1.Nodes.Add(fileName);
            addTreeNode(firstNode,(XElement)xdoc.FirstNode);


        }

        private void treeView1_AfterSelect(object sender,TreeViewEventArgs e) {
            currentSelectedNode=e.Node;
            checkButtonText();

            TreeNode selectedNode = this.treeView1.SelectedNode;
            if(selectedNode==null)
                return;
            else {
                //MessageBox.Show(e.Node.Text);
                labelText=e.Node.Text;
                this.label_title.Text=labelText;
                if(e.Node.Tag!=null) {
                    XElement nodeXElem = (XElement)e.Node.Tag;
                    if(nodeXElem.HasAttributes) {
                        string nodeAttribute = nodeXElem.FirstAttribute.ToString();
                        this.textbox_attributes.Text=nodeAttribute;
                        //richTextBox2.AppendText();
                    } else this.textbox_attributes.Text=" ";
                    this.textbox_content.Text=nodeXElem.Value;
                }
            }
            checkButtonText();
        }
        private void label_title_Click(object sender,EventArgs e) {

        }

        private void label_attributes_Click(object sender,EventArgs e) {

        }

        private void label1_Click(object sender,EventArgs e) {

        }

        private void textbox_attributes_TextChanged(object sender,EventArgs e) {

        }

        private void textbox_content_TextChanged(object sender,EventArgs e) {

        }

        private void textbox_search_TextChanged(object sender,EventArgs e) {
            colorRestore(firstNode);
            itemFound=0;
            colorPtr=0;
            this.label_itemFound.Text=" ";
        }
        private void textbox_search_EnterClicked(object sender,KeyEventArgs e) {
            if(e.KeyData==Keys.Enter) {
                if(textbox_search.Text.Equals(searchText,StringComparison.OrdinalIgnoreCase)) return;
                searchText=textbox_search.Text;
                showPending();
                advancedSearch(firstNode,searchText);
                hidePending();
                this.label_itemFound.Text=itemFound.ToString()+(itemFound>1 ? " items found" : " item found");

            }
            checkButtonText();
        }

        private void button1_Click(object sender,EventArgs e) {
            //bug fixed :)
            if(textbox_search.Text.Equals(searchText,StringComparison.OrdinalIgnoreCase)) return;
            searchText=textbox_search.Text;
            showPending();
            advancedSearch(firstNode,searchText);
            hidePending();
            this.label_itemFound.Text=itemFound.ToString()+(itemFound>1 ? " items found" : " item found");
        }

        private void button_expandAll_Click(object sender,EventArgs e) {
            if(firstNode.IsExpanded) {
                firstNode.Collapse(false);
                button_expandAll.Text="Expand All Node";
            } else {
                showPending();
                firstNode.ExpandAll();
                hidePending();
                button_expandAll.Text="Collapse All Node";
            }
        }

        private void button_expand_Click(object sender,EventArgs e) {
            if(currentSelectedNode.IsExpanded) {
                currentSelectedNode.Collapse(false);
                button_expand.Text="Expand Node";
            } else {
                showPending();
                currentSelectedNode.Expand();
                hidePending();
                button_expand.Text="Collapse Node";
            }
        }

        private void label_itemFound_Click(object sender,EventArgs e) {

        }

        private void richTextBox2_TextChanged(object sender,EventArgs e) {

        }

        private void process1_Exited(object sender,EventArgs e) {

        }

        private void label_pending_Click(object sender,EventArgs e) {

        }
    }
}
