#define SEARCH_ON
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace WindowsFormsApplicationTest {
    public partial class Form1:Form {

        public Form1() {
            //labelText="defaultForm1";
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
                this.label_title.Text=cutHead(e.Node.Text);
                if(e.Node.Tag!=null) {
                    try {
                        this.textbox_content.Text=((XElement)e.Node.Tag).Value;
                        setBold(e);
                        highlight();
                    } catch {
                        try {
                            foreach(var text in (((JObject)e.Node.Tag).Children()))
                                textbox_content.Text+=(text.ToString()+"\n");
                            highlight();
                        } catch(Exception ex) {
                            Console.Write(ex.Message);
                        }
                    }
                } else this.richTextBox1.Text=" ";

            }

            //endif selected node is null
            checkBTxt();
        }
        private void resetSearch() {
            colorRestore(firstNode);
            itmsFd=0;
            cPtr=0;
            currentTarStr=null;
            this.label_itemFound.Text=" ";
        }
        private void textbox_search_TextChanged(object sender,EventArgs e) {
            resetSearch();
            currentTarStr=textbox_search.Text.ToString().Split(' ');
            if(currentTarStr.Length>6) {
                textbox_search.ReadOnly=true;
                label_itemFound.Text="You can search atmost 6 items at once.";
            }
        }
        private void updateHistory() {
            int index = history.Count;
            history0ToolStripMenuItem.Text=(history[--index]+"..");
            if(index-->0) history1ToolStripMenuItem.Text=(history[index]+"..");
            if(index-->0) history2ToolStripMenuItem.Text=(history[index]+"..");
            if(index-->0) history3ToolStripMenuItem.Text=(history[index]+"..");
            if(index-->0) history4ToolStripMenuItem.Text=(history[index]+"..");
            if(index-->0) history5ToolStripMenuItem.Text=(history[index]+"..");

        }
        private void textbox_search_EnterClicked(object sender,KeyEventArgs e) {
            if(listBox_history.Items.Count!=0) listBox_history.Show();
            this.Update();
            switch(e.KeyData) {
                case Keys.Enter:
                resetSearch();
                listBox_history.Hide();
                this.Update();
                //if(textbox_search.Text.Equals(searchText,StringComparison.OrdinalIgnoreCase)) return;
                generalSearch();
                break;
                case Keys.Back:
                textbox_search.ReadOnly=false;
                label_itemFound.Text="";
                break;
                case Keys.Down:
                listBox_history.Show();
                this.Update();
                break;
                default:
                break;
            }
            checkBTxt();
        }

        //this is a extracted logic for the general search
        //===================================================
        private void generalSearch() {
            history.Add(textbox_search.Text);
            //listBox_history.Items.Add(textbox_search.Text);
            listBox_history.Items.Insert(0,textbox_search.Text);
            updateHistory();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Reset();
            stopwatch.Start();
            searchText=textbox_search.Text;
            currentTarStr=searchText.Split(' ');

            showP();
            foreach(TreeNode tn in treeView1.Nodes)
                advSearch(tn,searchText);
            hideP();
            stopwatch.Stop();
            this.label_itemFound.Text=itmsFd.ToString()+(itmsFd>1 ? " items found in " : " item found in ")+stopwatch.ElapsedMilliseconds.ToString()+"ms.";
        }
        //===================================================
        private void button1_Click(object sender,EventArgs e) {
            //bug fixed :)
            //if(textbox_search.Text.Equals(searchText,StringComparison.OrdinalIgnoreCase)) return;
            resetSearch();
            generalSearch();
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
            try {
                if(currentSelectedNode.IsExpanded) {
                    currentSelectedNode.Collapse(false);
                    button_expand.Text="Expand Node";
                } else {
                    showP();
                    currentSelectedNode.Expand();
                    hideP();
                    button_expand.Text="Collapse Node";
                }
            } catch(Exception) { };
        }
        private async void openToolStripMenuItem_Click(object sender,EventArgs e) {
            this.openFileDialog1.ShowDialog();
            Stream fileStream;
            showP();
            try {
                if((fileStream=openFileDialog1.OpenFile())!=null)
                    
                    await setup(fileStream,openFileDialog1.FileName);
            } catch(Exception ex) { cutHead(ex.ToString()); }
            hideP();
            this.textbox_search.ReadOnly=false;
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

        private void label_title_Click(object sender,EventArgs e) {

        }

        private void historyToolStripMenuItem_Click(object sender,EventArgs e) {

        }

        private void aboutAthenaToolStripMenuItem_Click(object sender,EventArgs e) {

        }
        private void historySearch(int i) {
            resetSearch();
            searchText=history[history.Count-i];
            textbox_search.Text=history[history.Count-i];
            generalSearch();
        }
        private void history0ToolStripMenuItem_Click(object sender,EventArgs e) {
            historySearch(1);
        }

        private void history1ToolStripMenuItem_Click(object sender,EventArgs e) {
            historySearch(2);
        }

        private void history2ToolStripMenuItem_Click(object sender,EventArgs e) {
            historySearch(3);
        }

        private void history3ToolStripMenuItem_Click(object sender,EventArgs e) {
            historySearch(4);
        }

        private void history4ToolStripMenuItem_Click(object sender,EventArgs e) {
            historySearch(5);
        }

        private void history5ToolStripMenuItem_Click(object sender,EventArgs e) {
            historySearch(6);
        }

        private void listBox1_SelectedIndexChanged(object sender,EventArgs e) {
            textbox_search.Text=listBox_history.SelectedItem.ToString();
            listBox_history.Hide();

        }

        private void Form1_Load(object sender,EventArgs e) {

        }
    }
}
