#define SEARCH_ON
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Reflection;
namespace WindowsFormsApplicationTest {
    public partial class Form1:Form {
        public void setup(Stream filePath) {
            StreamReader sr = new StreamReader(filePath,true);
            XDocument xdoc =XDocument.Load(sr);
            //XDocument xdoc = new XDocument();
            firstNode=this.treeView1.Nodes.Add("CCDXml"+"_"+xmlIndex++);
            addTn(firstNode,(XElement)xdoc.FirstNode);
        }
        public Form1() {
            labelText="defaultForm1";
            InitializeComponent();
        }

        private void treeView1_AfterSelect(object sender,TreeViewEventArgs e) {
            currentSelectedNode=e.Node;
            checkBTxt();
            //richTextBox1.BackColor=Color.FromArgb(255,255,255); <--this is wrong!!!
            richTextBox1.SelectAll();
            richTextBox1.SelectionBackColor=Color.FromArgb(255,255,255);
            textbox_content.SelectAll();
            textbox_content.SelectionBackColor=Color.FromArgb(255,255,255);

            TreeNode selectedNode = this.treeView1.SelectedNode;
            if(selectedNode==null)
                return;
            else {
                this.label_title.Text=labelText;
                if(e.Node.Tag!=null) {
                    this.textbox_content.Text=((XElement)e.Node.Tag).Value;
                    setBold(e);
                    highlight();

                } else this.richTextBox1.Text=" ";
                
            }

            //endif selected node is null
            checkBTxt();
        }
        private void textbox_search_TextChanged(object sender,EventArgs e) {
            colorRestore(firstNode);
            itmsFd=0;
            cPtr=0;
            currentTarStr=null;
            this.label_itemFound.Text=" ";
            currentTarStr=textbox_search.Text.ToString().Split(' ');
            if(currentTarStr.Length>6) {
                textbox_search.ReadOnly=true;
                label_itemFound.Text="You can search atmost 6 items at once.";
            }
        }

        private void textbox_search_EnterClicked(object sender,KeyEventArgs e) {
            if(e.KeyData==Keys.Enter) {
                if(textbox_search.Text.Equals(searchText,StringComparison.OrdinalIgnoreCase)) return;
                searchText=textbox_search.Text;
                showP();
                advSearch(firstNode,searchText);
                hideP();
                this.label_itemFound.Text=itmsFd.ToString()+(itmsFd>1 ? " items found" : " item found");

            }else if(e.KeyData==Keys.Back) {
                textbox_search.ReadOnly=false;
                label_itemFound.Text="";
            }
            checkBTxt();
        }
        private void button1_Click(object sender,EventArgs e) {
            //bug fixed :)
            if(textbox_search.Text.Equals(searchText,StringComparison.OrdinalIgnoreCase)) return;
            searchText=textbox_search.Text;
            showP();
            advSearch(firstNode,searchText);
            hideP();
            this.label_itemFound.Text=itmsFd.ToString()+(itmsFd>1 ? " items found" : " item found");
        }

        private void button_expandAll_Click(object sender,EventArgs e) {
            if(firstNode.IsExpanded) {
                firstNode.Collapse(false);
                button_expandAll.Text="Expand All Node";
            } else {
                showP();
                firstNode.ExpandAll();
                hideP();
                button_expandAll.Text="Collapse All Node";
            }
        }

        private void button_expand_Click(object sender,EventArgs e) {
            if(currentSelectedNode.IsExpanded) {
                currentSelectedNode.Collapse(false);
                button_expand.Text="Expand Node";
            } else {
                showP();
                currentSelectedNode.Expand();
                hideP();
                button_expand.Text="Collapse Node";
            }
        }
        private void openToolStripMenuItem_Click(object sender,EventArgs e) {
            this.openFileDialog1.ShowDialog();
            Stream fileStream;
            try {
                if((fileStream=openFileDialog1.OpenFile())!=null) {
                    showP();
                    setup(fileStream);
                    hideP();
                    this.textbox_search.ReadOnly=false;
                }
            } catch(Exception ex) { cutHead(ex.ToString()); }
        }

        private void expendAllNodesToolStripMenuItem_Click(object sender,EventArgs e) {
            if(firstNode!=null)
                if(firstNode.IsExpanded) {
                    firstNode.Collapse(false);
                    button_expandAll.Text="Expand All Node";
                } else {
                    showP();
                    firstNode.ExpandAll();
                    hideP();
                    button_expandAll.Text="Collapse All Node";
                }
            this.label_title.ForeColor=Color.FromArgb(0,0,0);
        }

        private void tableLayoutPanel1_Paint(object sender,PaintEventArgs e) {

        }

        private void dataGridView1_CellContentClick(object sender,DataGridViewCellEventArgs e) {

        }

        private void webBrowser1_DocumentCompleted(object sender,WebBrowserDocumentCompletedEventArgs e) {

        }

        private void label1_Click(object sender,EventArgs e) {

        }
    }
}
