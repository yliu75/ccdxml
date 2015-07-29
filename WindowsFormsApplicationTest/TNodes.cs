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

namespace XMLExplorer {
    public abstract class TNodes:TreeNode {
        public int maxDepth { get; set; }
        public int totalNodes { get; set; }
        public TreeNode root { get; set; }

        public static string cutHead(string s) {
            //string result;
            //if(s==null||getHead(s)==0) return s;
            string[] res = s.Split('}');
            if(res.Length==1) return res[0];
            else return res[1];
        }
        public virtual Task addTn(TreeNode parentNode,XElement ele,int depth) { return null; }
        public virtual Task addTn(TreeNode parentNode,JObject jobj,int depth) { return null; }

        }
        public class XMLTreeNode:TNodes {
        public XMLTreeNode() {
            maxDepth=0;
            totalNodes=0;
            root=null;
        }
        public XMLTreeNode(int max_depth,int total_nodes,TreeNode _node) {
            maxDepth=max_depth;
            root=_node;
            totalNodes=total_nodes;
        }
        public override async Task addTn(TreeNode parentNode,XElement ele,int depth) {
            //await Task.Run(async() => {//delete this to boost the loading speed to x10
            if(depth>maxDepth) maxDepth=depth;
            try {
                if(parentNode==null||ele==null||ele.Name==null||ele.Name.ToString()==null)
                    return;
                string nodeName = cutHead(ele.Name.ToString());
                if(nodeName==null)
                    return;
                var treeN = parentNode.Nodes.Add(nodeName);
                treeN.Tag=ele;
                totalNodes++;
                foreach(XElement node in ele.Elements())
                    await addTn(treeN,node,depth+1);
            } catch(Exception e) {
                Console.WriteLine(e.Message);
            }
        }
    }
    public class JSONTreeNode:TNodes {
        public JSONTreeNode() {
            maxDepth=0;
            totalNodes=0;
            root=null;
        }
        public JSONTreeNode(int max_depth,int total_nodes,TreeNode _node) {
            maxDepth=max_depth;
            root=_node;
            totalNodes=total_nodes;
        }
        public override async Task addTn(TreeNode parentNode,JObject JObj,int depth) {
            if(depth>maxDepth) maxDepth=depth;
            try {
                if(parentNode==null||JObj==null)
                    return;
                string nodeName = cutHead(JObj.First.Path.ToString());
                if(nodeName==null)
                    return;
                var treeN = parentNode.Nodes.Add(nodeName);
                treeN.Tag=JObj;
                totalNodes++;
                foreach(var node in JObj.First) {
                    if(node.GetType()==typeof(JObject))
                        await addTn(treeN,(JObject)node,depth+1);
                    else if(node.GetType()==typeof(JArray)) {
                        var tn = treeN.Nodes.Add("Array");
                        foreach(var nnode in node)
                            await addTn(tn,(JObject)nnode,depth+1);
                    }
                }
            } catch(Exception e) {
                Console.WriteLine(e.Message);
            }
        }
    }





    //public class A {
    //    public void aMethod() { }

    //}
    //public class B:A {
    //    public override void aMethod() { }
    //}
}
