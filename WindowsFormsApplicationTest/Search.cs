#define SEARCH_ON
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using XMLExplorer;

namespace XMLExplorer {
    class Search {
        public int itmsFd { get; set; }
        public string searchText { get; set; }
        public int cPtr { get; set; }//color index pointer
        public int[] cLst { get; set; }
        public String[] currentTarStr;
        public TreeNode startNode;
        public Search() {
            itmsFd=0;
            cPtr=0;
            cLst=null;
            currentTarStr=null;
            searchText=null;
            startNode=null;
        }
        public Search(string st,TreeNode tn) {
            cLst=new int[]{ 157,206,255,
                            250,190,125,
                            210,255,166,
                            203,155,255,
                            244,252,146,
                            201,201,201,
                            201,201,201};//color array
            searchText=st;
            currentTarStr=searchText.Split(' ');
            startNode=tn;
        }

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
        public static void expendNode(TreeNode child) {
            child.Expand();
            if(child.Parent!=null) expendNode(child.Parent);
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
        [Conditional("SEARCH_ON")]
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
            while(i-->0) {
                if(tarStr[i]==' ') {
                    spaceFlag=true;
                    string subStr = tarStr.Substring(i+1,stringPtr-i-1);
                    stringPtr=i;
                    searchLogic(root,subStr);
                }
            }
            bool temp = !spaceFlag;
            if(!spaceFlag) {
                searchLogic(root,tarStr);
            } else {
                searchLogic(root,tarStr.Substring(i+1,stringPtr));
            }
        }
    }
}
