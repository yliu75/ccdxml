#define SEARCH_ON

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Reflection;
using System.Diagnostics;
namespace WindowsFormsApplicationTest {
    public partial class Form1 {
        //==========================================================================================
        //below this line is my own function definition
        string labelText;
        string searchText;
        int itmsFd;
        int cPtr = 0;//color index pointer
        int[] cLst = {  157,206,255,
                        250,190,125,
                        210,255,166,
                        203,155,255,
                        244,252,146,
                        201,201,201
        };//color array
        String[] currentTarStr = null;
        int xmlIndex = 0;
        System.Windows.Forms.Label SelectedLabel = new System.Windows.Forms.Label();
        TreeNode firstNode;
        TreeNode currentSelectedNode;

        ///cut the head{urn:hl7-org:v3} of the XElement.Name
        public static string cutHead(string s) {
            //string result;
            //if(s==null||getHead(s)==0) return s;
            string[] res = s.Split('}');
            if(res.Length==1) return res[0];
            else return res[1];
        }
        //function that add nodes to treeviews form the xml file
        public static void addTn(TreeNode parentNode,XElement ele) {
            if(parentNode==null||ele==null||ele.Name==null||ele.Name.ToString()==null)
                return;
            string nodeName = cutHead(ele.Name.ToString());
            if(nodeName==null)
                return;
            var treeN = parentNode.Nodes.Add(cutHead(ele.Name.ToString()));
            treeN.Tag=ele;
            foreach(XElement node in ele.Elements())
                addTn(treeN,node);
        }
        //expendnode and all its parent,grandparent, grandgrandparents,etc..
        public static void expendNode(TreeNode child) {
            child.Expand();
            if(child.Parent!=null) expendNode(child.Parent);
        }
        //color restore
        public static void colorRestore(TreeNode tn) {
            tn.BackColor=Color.FromArgb(255,255,255);
            if(tn.Nodes!=null)
                foreach(TreeNode t in tn.Nodes)
                    colorRestore(t);
        }
        //this function search the tree for a string target,and color it to green
        [Conditional("SEARCH_ON")]
        public void searchTV(TreeNode root,string target) {
            if(root==null) return;
            if(root.Text.Equals(target,StringComparison.OrdinalIgnoreCase)) {
                expendNode(root);
                //root.Expand();
                root.BackColor=Color.FromArgb(cLst[cPtr],cLst[cPtr+1],cLst[cPtr+2]);
                itmsFd++;
            }
            foreach(TreeNode tn in root.Nodes)
                searchTV(tn,target);
        }
        //kmp algorithm
        //next func
        [Conditional("SEARCH_ON")]
        public static void getNext(string str,int[] next) {
            int len = str.Length;
            next[0]=-1;
            int k = -1;
            int j = 0;
            while(j<len-1)
                if(k==-1||str[j]==str[k]) {
                    j++; k++;
                    if(str[j]!=str[k]) next[j]=k;
                    else next[j]=next[k];
                } else k=next[k];

        }
        public static int kmp(string str,string target,int[] next) {
            int i = 0, j = 0;
            target=target.ToLower();
            str=str.ToLower();
            int strLen = str.Length;
            int tarLen = target.Length;
            while(i<strLen&&j<tarLen)
                if(j==-1||str[i]==target[j]) {
                    i++; j++;
                } else j=next[j];
            if(j==tarLen) return i-j;
            else return -1;
        }
        public static int searchTxt(string str,string target) {
            int[] next = new int[target.Length];
            getNext(target,next);
            return kmp(str,target,next);
        }
        //this func search content other than the node name
        [Conditional("SEARCH_ON")]
        public void searchC(TreeNode root,string target) {
            if(root==null) return;
            if(root.Tag!=null) {
                string attrsStr = "";// ((XElement)root.Tag).Attributes().ToString();
                string content = ((XElement)root.Tag).Value;
                foreach(var s in ((XElement)root.Tag).Attributes())
                    attrsStr+=s.ToString();
                int result = searchTxt(attrsStr,target);
                if(result!=-1||searchTxt(content,target)!=-1) {
                    expendNode(root);
                    root.BackColor=Color.FromArgb(cLst[cPtr],cLst[cPtr+1],cLst[cPtr+2]);
                    itmsFd++;
                }
            }
            foreach(TreeNode tn in root.Nodes)
                searchC(tn,target);
        }
        //check button text
        [Conditional("SEARCH_ON")]
        public void checkBTxt() {
            if(this.currentSelectedNode!=null)
                if(this.currentSelectedNode.IsExpanded) this.button_expand.Text="Collapse Node";
                else this.button_expand.Text="Expand Node";
            if(firstNode!=null)
                if(firstNode.IsExpanded) button_expandAll.Text="Collapse All Node";
                else button_expandAll.Text="Expand All Node";
            //check if the textbox need a scroll bar
            if(this.textbox_content.Text.Length>=500)
                this.textbox_content.ScrollBars=RichTextBoxScrollBars.Vertical;
            else this.textbox_content.ScrollBars=RichTextBoxScrollBars.None;
        }
        //advanced search 
        //can search multiple item at the same time
        //public void searchLogic() {

        //}
        public void searchLogic(TreeNode tn,string target) {
            searchTV(tn,target);
            searchC(tn,target);
            cPtr+=3;
            if(cPtr>17) cPtr=0;
        }
        [Conditional("SEARCH_ON")]
        public void advSearch(TreeNode root,string tarStr) {
            int i = tarStr.Length, stringPtr = i;
            //check if there is space
            bool spaceFlag = false;
            while(i-->0)
                if(tarStr[i]==' ') {
                    spaceFlag=true;
                    string subStr = tarStr.Substring(i+1,stringPtr-i-1);
                    stringPtr=i;
                    searchLogic(root,subStr);
                }

            if(!spaceFlag)
                searchLogic(root,tarStr);
            else
                searchLogic(root,tarStr.Substring(i+1,stringPtr));
        }

        //find the left quotation mark
        public int leftQM(string str,int start) {
            for(int i = start;i<str.Length;i++) {
                if(str[i]=='\"') return i;
            }
            return -1;
        }

        //find the right quotaiton mark
        public int rightQM(string str,int start) {
            bool flag = true;
            for(int i = start;i<str.Length;i++) {
                if(str[i]=='\"')
                    if(!flag) return i;
                    else flag=false;
            }
            return -1;
        }
        //show pending label and hide pending label
        public void showP() {
            this.label_pending.Show();
            this.treeView1.Visible=false;
            this.Update();
        }
        public void hideP() {
            this.label_pending.Hide();
            this.treeView1.Visible=true;
        }
        //-----------------------------------------


        //make attributes to strings
        public string attrToStr(XElement xEle) {
            string nAttr = "";
            foreach(var attr in xEle.Attributes()) {
                try {
                    nAttr+=attr.ToString()+"\n";
                } catch(Exception ex) { ex.ToString(); }
            }
            return nAttr;
        }

        //make interesting data to bold in attributes
        [Conditional("SEARCH_ON")]
        public void setBold(TreeViewEventArgs e) {
            XElement nodeXElem = (XElement)e.Node.Tag;
            if(nodeXElem.HasAttributes) {
                //attributes of a node into stirng format
                string nAttr = attrToStr(nodeXElem);
                this.richTextBox1.Text=nAttr;
                int start = 0, left = 0, right = 0, len = nAttr.Length;

                //find all the data and set them to bold
                while(start<len&&(leftQM(nAttr,start)!=-1)) {

                    //find the left and right index of interesting text                
                    left=leftQM(nAttr,start);
                    right=rightQM(nAttr,start);

                    //select them and bold them
                    richTextBox1.Select(left,right-left+1);
                    richTextBox1.SelectionFont=new Font("Microsoft YaHei",12f,FontStyle.Bold);
                    this.Update();
                    start=right+1;
                }
            } else this.richTextBox1.Text=" ";
        }
        [Conditional("SEARCH_ON")]
        public void highlight() {
            //try {
            int colorPtr = (currentTarStr.Length-1)*3;
            foreach(string str in currentTarStr) {
                int aRes = searchTxt(this.richTextBox1.Text,str);
                int cRes = searchTxt(this.textbox_content.Text,str);
                if(aRes!=-1) {
                    richTextBox1.Select(aRes,str.Length);
                    richTextBox1.SelectionBackColor=Color.FromArgb(cLst[colorPtr],cLst[colorPtr+1],cLst[colorPtr+2]);
                }
                if(cRes!=-1) {
                    textbox_content.Select(cRes,str.Length);
                    textbox_content.SelectionBackColor=Color.FromArgb(cLst[colorPtr],cLst[colorPtr+1],cLst[colorPtr+2]);
                }
                colorPtr-=3;
            }
            // } catch(Exception ex) { ex.ToString(); }
        }

        //end of definition
        //==========================================================================================
        //----------------------------------------------------
        //bug fixed
        //Bug 1
        //Problem: Multiple enter pressed or multiple search clicked
        //Solution :if(textbox_search.Text.Equals(searchText,StringComparison.OrdinalIgnoreCase)) return;
        //
        //Bug 2
        //Problem: Asthma searched return no results.
        //Solution: It fixed per se...
        //end of bugs
        //----------------------------------------------------
    }
}
