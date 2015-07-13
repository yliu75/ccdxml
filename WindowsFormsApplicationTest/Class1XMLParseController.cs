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
using System.Windows.Controls;
using System.Windows.Documents;

namespace WindowsFormsApplicationTest {
    public partial class Form1 {

        //==========================================================================================
        //below this line is my own function definition
        string labelText;
        string searchText;
        int itemFound;
        int colorPtr = 0;
        int[] colorList = {157,206,255,
                           250,190,125,
                           210,255,166,
                           203,155,255,
                           244,252,146,
                           201,201,201
        };
        int xmlIndex = 0;
        System.Windows.Forms.Label SelectedLabel = new System.Windows.Forms.Label();
        TreeNode firstNode;
        TreeNode currentSelectedNode;

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
            if(this.currentSelectedNode!=null)
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
            this.label_pending.Show();
            this.treeView1.Visible=false;
            this.Update();


        }
        public void hidePending() {
            this.label_pending.Hide();
            this.treeView1.Visible=true;
        }
        public void richTextBox() {
            FlowDocument flowDoc = new FlowDocument(new Paragraph(new Run("Simple FlowDocument")));
            Paragraph para1 = new Paragraph(new Run("abc"));
            //para1.FontStyle=FontStyle.Bold;
            richTextBox1.Document=flowDoc;
        }
        //end of definition
        //==========================================================================================
    }
}
