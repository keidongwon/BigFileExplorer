using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Diagnostics;

namespace BigFileExplorer
{
    public partial class Main : Form
    {
        FileControl fc = new FileControl();
        Config config = Config.GetInstance();
        bool isCurrentRoot = true;
        string dirCurrent = "";
        string dirLast = "";
        int rootsFontSize = 10;
        int filesFontSize = 10;
        
        public Main()
        {
            InitializeComponent();
            ComponentUpdate();
            SetRootFolders();
        }

        public void ComponentUpdate()
        {
            this.Width = 1600;
            this.Height = 1000;
            int rootsWidth = 500;
            int filesColumn = 600;

            config.GetComponentSize(ref rootsWidth, ref rootsFontSize, ref filesColumn, ref filesFontSize);

            listViewRoot.Click += new System.EventHandler(this.listViewRoot_Click);
            listViewFiles.DoubleClick += new System.EventHandler(this.listViewFiles_DoubleClick);
            listViewFiles.DrawItem += new DrawListViewItemEventHandler(this.DrawItemHandler);

            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;

            listViewRoot.View = View.Details;
            listViewRoot.Font = new System.Drawing.Font(config.GetFontFace(), rootsFontSize);
            listViewRoot.Location = new System.Drawing.Point(20, 20);
            listViewRoot.Size = new System.Drawing.Size(rootsWidth, this.Height - 150);
            listViewRoot.Columns.Add("Root", rootsWidth - 5);
            listViewRoot.Columns.Add("Path", 0);

            buttonExit.Location = new System.Drawing.Point(this.Width - 160, this.Height - 110);

            int filesWidth = this.Width - (rootsWidth + 90);

            listViewFiles.Font = new System.Drawing.Font(config.GetFontFace(), filesFontSize);
            listViewFiles.Location = new System.Drawing.Point(rootsWidth + 40, 20);
            listViewFiles.Size = new System.Drawing.Size(filesWidth, this.Height - 150);
            if (config.GetViewStyle())
            {
                listViewFiles.View = View.Details;
                listViewFiles.Columns.Add("Files", filesWidth - 21);
            }
            else
            {
                listViewFiles.Columns.Add("Files", filesColumn);
                listViewFiles.View = View.List;
            }
            listViewFiles.Columns.Add("Path", 0);
        }

        private void DrawItemHandler(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();

            Color color;

            string path = listViewFiles.Items[e.ItemIndex].SubItems[1].Text;
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            string type = dirInfo.Attributes.ToString();
            if (type.Contains("Directory"))
                color = Color.Brown;
            else
                color = Color.WhiteSmoke;

            e.Graphics.DrawString(listViewFiles.Items[e.ItemIndex].Text,
                                  new Font(config.GetFontFace(), filesFontSize, FontStyle.Bold),
                                  new SolidBrush(color),
                                  e.Bounds);
        }

        public void SetRootFolders()
        {
            listViewRoot.BeginUpdate();
            listViewRoot.Items.Clear();

            Dictionary<string, string> roots = new Dictionary<string, string>();
            config.GetRootFolders(ref roots);
            foreach (string item in roots.Keys)
            {
                string[] items = { item, roots[item] };
                ListViewItem lvi = new ListViewItem(items);
                listViewRoot.Items.Add(lvi);
            }
            listViewRoot.EndUpdate();
        }

        public void ShowFiles(string path)
        {
            if (path.Length < 5) return;

            dirLast = dirCurrent;
            dirCurrent = path;
            Dictionary<string, string> dirs = new Dictionary<string, string>();
            Dictionary<string, string> files = new Dictionary<string, string>();
            fc.GetFiles(path, ref dirs, ref files);

            listViewFiles.Items.Clear();
            
            if (isCurrentRoot == false)
            {
                string[] items = { "[Parent folder]", ".." };
                ListViewItem lvi = new ListViewItem(items);
                listViewFiles.Items.Add(lvi);
            }

            foreach (string dir in dirs.Keys)
            {
                if (dir.StartsWith(".")) continue;
                string[] items = { dir, dirs[dir] };
                ListViewItem lvi = new ListViewItem(items);
                listViewFiles.Items.Add(lvi);
            }

            foreach (string file in files.Keys)
            {
                string[] items = { file, files[file] };
                ListViewItem lvi = new ListViewItem(items);
                listViewFiles.Items.Add(lvi);
            }
        }

        private void listViewRoot_Click(object sender, EventArgs e)
        {
            int index = listViewRoot.FocusedItem.Index;
            string path = listViewRoot.Items[index].SubItems[1].Text;
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            string type = dirInfo.Attributes.ToString();
            if (type.Contains("Directory"))
            {
                isCurrentRoot = true;
                ShowFiles(path);
            }
        }

        private void listViewFiles_DoubleClick(object sender, EventArgs e)
        {
            int index = listViewFiles.FocusedItem.Index;
            string path = listViewFiles.Items[index].SubItems[1].Text;

            try
            {
                if (path == "..")
                {
                    int pos = dirCurrent.LastIndexOf("\\");
                    string dir = dirCurrent.Substring(0, pos);
                    ShowFiles(dir);
                    return;
                }

                DirectoryInfo dirInfo = new DirectoryInfo(path);
                string type = dirInfo.Attributes.ToString();
                if (type.Contains("Directory"))
                {
                    isCurrentRoot = false;
                    ShowFiles(path);
                }
                else
                {
                    Process.Start(path);
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Can not execute!");
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
