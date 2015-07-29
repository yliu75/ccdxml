#define SEARCH_ON
//================================================================================================//
//  Project xmlExplorer                                                                           //
//  Author: Yonglun Liu                                                                           //
//  Date:2015/07/20                                                                               //
//                                                                                                //
//  Instruction:                                                                                  //
//  Pretty self explained appliction                                                              //
//                                                                                                //
//  If any further support is needed email me:yliu75 at syr dot edu                               //
//================================================================================================//
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Controls;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace WindowsFormsApplicationTest {
    public partial class Form1 {
        //==========================================================================================
        //below this line is my own function definition
        //string labelText;
        string searchText;
        int itmsFd;
        int cPtr = 0;//color index pointer
        int[] cLst = {  157,206,255,
                        250,190,125,
                        210,255,166,
                        203,155,255,
                        244,252,146,
                        201,201,201,
                        201,201,201
        };//color array
        String[] currentTarStr = null;
        int treeIndex = 0;
        static int totalNodes = 0, maxDepth = 0;
        System.Windows.Forms.Label SelectedLabel = new System.Windows.Forms.Label();
        TreeNode firstNode, statsNode;
        TreeNode currentSelectedNode;
        List<string> history = new List<string>();

        ///cut the head{urn:hl7-org:v3} of the XElement.Name
        public static string cutHead(string s) {
            //string result;
            //if(s==null||getHead(s)==0) return s;
            string[] res = s.Split('}');
            if(res.Length==1) return res[0];
            else return res[1];
        }
        private void tnInsertion(TreeNode tn) {
            if(InvokeRequired) {
                Invoke((Action<TreeNode>)tnInsertion,tn);
                return;
            }
            treeView1.Nodes.Insert(0,tn);
        }
        public static string getExtention(string filePath) {
            if(filePath!=null) {
                string[] temp = filePath.Split('.');
                return temp[temp.Length-1];
            }
            return null;
        }
        public async Task setup(Stream filePath,string filePathStr) {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Reset();
            stopwatch.Start();
            //if the loaded file is an xml file
            using(StreamReader sr = new StreamReader(filePath,true)) {
                string extn =getExtention( filePathStr);
                if(extn.Equals("xml",StringComparison.OrdinalIgnoreCase)) {
                    XDocument xdoc = null;
                    await Task.Run(() => {
                        xdoc=XDocument.Load(sr);
                    });

                    //XDocument xdoc = new XDocument();
                    //firstNode=this.treeView1.Nodes.Add();
                    firstNode=new TreeNode("Xml_"+treeIndex++);
                    XNode xEle = xdoc.FirstNode;
                    while(xEle.GetType().IsEquivalentTo(typeof(XComment))) xEle=xEle.NextNode;

                    await Task.Run(async () => {
                        await addTn(firstNode,(XElement)xEle,maxDepth);
                        //treeView1.Nodes.Insert(0,firstNode); <--this will cause infinitly pending...
                        //tnInsertion(firstNode);
                    });
                    //if the loaded file is a json file
                } else if(extn.Equals("json",StringComparison.OrdinalIgnoreCase)) {
                    using(StreamReader reader = new StreamReader(filePath,true)) {
                        await Task.Run(() => {
                            JObject o = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                        });
                        firstNode=new TreeNode("Json_"+treeIndex++);
    
                    }
                }
            }
            //VirtualizingStackPanel.SetIsVirtualizing(treeView1,true);
            statsNode=new TreeNode("Info_"+(treeIndex-1).ToString());
            string ts = totalNodes.ToString()+" nodes loaded.\nMaximum depth is "+maxDepth.ToString()+".";
            firstNode.Tag=new XElement("info",ts);

            treeView1.Nodes.Insert(0,firstNode);
            stopwatch.Stop();

            label_itemFound.Text=totalNodes.ToString()+" nodes loaded in "+stopwatch.ElapsedMilliseconds.ToString()+"ms";
            //treeView1.Nodes.Insert(0,statsNode);
            totalNodes=0;
            maxDepth=0;
            //((XElement)firstNode.Tag)="Total "+totalNodes+" nodes loaded.\nMaximum depth is "+maxDepth;
            this.Update();
        }



        //function that add nodes to treeviews form the xml file
        public async static Task addTn(TreeNode parentNode,XElement ele,int depth) {
            //await Task.Run(async() => {//delete this to boost the loading speed to x10
            if(depth>maxDepth) maxDepth=depth;
            try {
                if(parentNode==null||ele==null||ele.Name==null||ele.Name.ToString()==null)
                    return;
                string nodeName = cutHead(ele.Name.ToString());
                if(nodeName==null)
                    return;
                var treeN = parentNode.Nodes.Add(cutHead(ele.Name.ToString()));
                treeN.Tag=ele;
                totalNodes++;
                foreach(XElement node in ele.Elements())
                    await addTn(treeN,node,depth+1);
            } catch(Exception e) {
                Console.WriteLine(e.Message);
            }
            //});

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
            if(target.Length==0) return -1;
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
            try {
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
            } catch(Exception ex) { Console.WriteLine(ex.Message); }
        }

        //end of definition
        //==========================================================================================
        //----------------------------------------------------
        //bug collection
        //Bug 1
        //Problem: Multiple enter pressed or multiple search clicked
        //Solution :if(textbox_search.Text.Equals(searchText,StringComparison.OrdinalIgnoreCase)) return;
        //
        //Bug 2
        //Problem: Asthma searched return no results.
        //Solution: It fixed per se...
        //
        //Bug 3
        //Problem: When the search target array end with space char, getnext will throw a outofrange
        //exception
        //Solution:Add a if in searchTxt();
        //
        //Bug 4
        //Problem: when more than 6 items was searched oor exception for colorPtr
        //Solution:Add a detection for the numbers of items searched. If exceeded 6, lock the search textbox.
        //Only backspace could be entered and in and only in this case, search textbox will be unlock.
        //
        //Bug 5
        //Problem: when six items was input and end with a space. oor ex thrown.
        //Solution: add a color set in color list.
        //
        //Bug 6
        //Problem: when Chinese char is existed, can display correct. But if select Chinese char node. Ex thrown.
        //No solution yet.
        //
        //Bug 7
        //Problem: errors occur when the xml file starts with XComments element
        //Solution: skip all the comments
        //end of bugs
        //----------------------------------------------------
        //Asynchronizational optmised for UI and loading
        //Hiding treeview can accelerate the UI speed
        //Notes:
        //0.Asynchronization is difficult.
        //1.Billions of requirements.
        //2.Most of them are helpful yet.
        //3.Implemented few of them.
        //4.I am feeling good at present.
    }
}
