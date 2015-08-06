#define SEARCH_ON
//#define DEBUG
#undef DEBUG
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Diagnostics;
using XMLExplorer;

namespace WindowsFormsApplicationTest {
    public partial class Form1:Form {

        public Form1() {
            //labelText="defaultForm1";
            InitializeComponent();
            load();
            setup();
        }
        public void setup() {
            AutoScaleMode=AutoScaleMode.Dpi;
            listBox_histAndSmt.Items.Add("----------------------------------");
            resizeComp();
        }
        public void resizeComp() {
            formHeight=Height;
            formWidth=Width;
            treeView1.Height=formHeight-280;
            button_expand.Location=new Point(16,formHeight-120);
            button_expandAll.Location=new Point(216,formHeight-120);
            groupBox1.Width=formWidth-550;
            richTextBox1.Width=formWidth-560;
            textbox_content.Width=formWidth-560;
            groupBox2.Width=formWidth-550;
            groupBox2.Height=formHeight-450;
            textbox_content.Height=formHeight-500;
            label_copyright.Location=new Point(formWidth-450,formHeight-80);

        }
        private void resize(object sender,EventArgs e) {

            resizeComp();
            Update();
        }
        public Stream GenerateStreamFromString(string s) {
            FileStream SourceStream = File.Open(s,FileMode.Open);
            //StreamWriter writer = new StreamWriter(stream);
            //writer.Write(s);
            //writer.Flush();
            //stream.Position=0;
            return SourceStream;
        }
        [Conditional("DEBUG")]
        public async void load() {
            Stream path = GenerateStreamFromString("C:\\Users\\Administrator\\Documents\\C#\\ClinicalDocument_orig.xml");
            await filePathToNode(path,"ClinicalDocument_orig.xml");
            await loadFile();
            textbox_search.ReadOnly=false;
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
                label_title.Text=cutHead(e.Node.Text);
                if(e.Node.Tag!=null) {
                    textbox_content.Text=((XElement)e.Node.Tag).Value;
                    setBold(e);
                    highlight();

                } else richTextBox1.Text=" ";

            }

            //endif selected node is null
            checkBTxt();
        }
        private void resetSearch() {
            colorRestore(firstNode);
            itmsFd=0;
            cPtr=0;
            currentTarStr=null;
            label_itemFound.Text=" ";
        }
        private void textbox_search_TextChanged(object sender,EventArgs e) {
            resetSearch();
            currentTarStr=textbox_search.Text.ToString().Split(' ');
            smartGuess(currentTarStr[currentTarStr.Length-1],DEFAULT_SMART_GUESS_ROWS);
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
            Update();
            //string[] currentString = textbox_search.Text.Split(' ');
            //smartGuess(currentString[currentString.Length-1]);
            if(listBox_histAndSmt.Items.Count!=0) listBox_histAndSmt.Show();
            Update();
            switch(e.KeyData) {
                case Keys.Enter:
                    resetSearch();
                    listBox_histAndSmt.Hide();
                    Update();
                    generalSearch();
                    break;
                case Keys.Back:
                    textbox_search.ReadOnly=false;
                    label_itemFound.Text="";
                    break;
                case Keys.Down:
                    listBox_histAndSmt.Show();
                    Update();
                    break;
                case Keys.Escape:
                    listBox_histAndSmt.Hide();
                    Update();
                    break;
                default:
                    break;

            }
            checkBTxt();
        }

        //this is a extracted logic for the general search
        //===================================================
        //private void generalSearch() {
        //    history.Add(textbox_search.Text);
        //    //listBox_histAndSmt.Items.Add(textbox_search.Text);
        //    listBox_histAndSmt.Items.Insert(0,textbox_search.Text);
        //    updateHistory();
        //    Stopwatch stopwatch = new Stopwatch();
        //    stopwatch.Reset();
        //    stopwatch.Start();
        //    searchText=textbox_search.Text;
        //    Search search = new Search(textbox_search.Text,treeView1.TopNode);

        //    showP();
        //    foreach(TreeNode tn in treeView1.Nodes)
        //        advSearch(tn,searchText);
        //    hideP();
        //    stopwatch.Stop();
        //    this.label_itemFound.Text=itmsFd.ToString()+(itmsFd>1 ? " items found in " : " item found in ")+stopwatch.ElapsedMilliseconds.ToString()+"ms.";
        //}
        private void generalSearch() {
            history.Add(textbox_search.Text);
            currentTarStr=textbox_search.Text.Split(' ');
            //listBox_histAndSmt.Items.Add(textbox_search.Text);
            listBox_histAndSmt.Items.Insert(0,textbox_search.Text);
            updateHistory();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Reset();
            stopwatch.Start();
            Search search = new Search(textbox_search.Text,firstNode);
            showP();
            search.advSearch(search.startNode,search.searchText);
            hideP();
            stopwatch.Stop();
            label_itemFound.Text=search.itmsFd.ToString()
                +(search.itmsFd>1 ? " items found in " : " item found in ")
                +stopwatch.ElapsedMilliseconds.ToString()
                +"ms.";
        }
        //===================================================
        private void button1_Click(object sender,EventArgs e) {
            //bug fixed :)
            //if(textbox_search.Text.Equals(searchText,StringComparison.OrdinalIgnoreCase)) return;
            listBox_histAndSmt.Hide();
            this.Update();
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
            fileTypeID=fileType.local;
            showP();
            try {
                if((fileStream=openFileDialog1.OpenFile())!=null) {
                    await filePathToNode(fileStream,openFileDialog1.FileName);
                    await loadFile();
                }
            } catch(Exception ex) { cutHead(ex.ToString()); }
            hideP();
            textbox_search.ReadOnly=false;
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
            try {
                //int selectedItem = listBox_histAndSmt.SelectedIndex;
                //if(selectedItem!=listBox_histAndSmt.Items.Count-1||smartGuessNum<1) {
                textbox_search.Text=listBox_histAndSmt.SelectedItem.ToString();
                generalSearch();
                listBox_histAndSmt.Hide();
                //}
            } catch(Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void Form1_Load(object sender,EventArgs e) {

        }

        private void copyToolStripMenuItem_Click(object sender,EventArgs e) {
            if(textbox_search.SelectedText!="")
                Clipboard.SetText(textbox_search.SelectedText);
            if(textbox_content.SelectedText!="")
                Clipboard.SetText(textbox_content.SelectedText);
            if(richTextBox1.SelectedText!="")
                Clipboard.SetText(richTextBox1.SelectedText);
            Update();
        }

        private void pasteToolStripMenuItem_Click(object sender,EventArgs e) {
            textbox_search.Paste(Clipboard.GetText());
        }

        private void selectAllToolStripMenuItem_Click(object sender,EventArgs e) {
            textbox_search.SelectAll();
            Update();
        }

        private void undoToolStripMenuItem_Click(object sender,EventArgs e) {
            textbox_search.Undo();
            Update();
        }

        private void cutToolStripMenuItem_Click(object sender,EventArgs e) {
            textbox_search.Cut();
            Update();
        }

        private void webAPIToolStripMenuItem_Click(object sender,EventArgs e) { }


        public async Task ShowMyDialogBox() {
            testDialog=new Form2();
            //testDialog.Show(this);
            //url=testDialog.textBox_url.Text;
            showP();
            if(testDialog.ShowDialog(this)==DialogResult.OK) {
                // Read the contents of testDialog's TextBox.
                url=testDialog.textBox_url.Text;
                
                await filePathToNode();
                await loadFile();
                textbox_search.ReadOnly=false;

            }
            //testDialog.Hide();
            hideP();
        }

        private async void jsonToolStripMenuItem_Click(object sender,EventArgs e) {

            fileTypeID=fileType.json;
            await ShowMyDialogBox();
        }

        private async void xmlToolStripMenuItem_Click(object sender,EventArgs e) {
            fileTypeID=fileType.xml;
            await ShowMyDialogBox();
        }

        private async void jsonToolStripMenuItem_Click_1(object sender,EventArgs e) {
            fileTypeID=fileType.json;
            await ShowMyDialogBox();
        }
    }
}
