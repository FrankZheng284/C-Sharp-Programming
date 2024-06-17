using System;
using System.IO;
using System.Windows.Forms;

namespace SimpleFileBrowser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeTreeView();
        }

        private void InitializeTreeView()
        {
            treeView1.Nodes.Clear();
            TreeNode rootNode = new TreeNode
            {
                Text = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                Tag = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
            };
            treeView1.Nodes.Add(rootNode);
            PopulateTreeView(rootNode);
            rootNode.Expand();
        }

        private void PopulateTreeView(TreeNode node)
        {
            try
            {
                string[] dirs = Directory.GetDirectories(node.Tag.ToString());
                foreach (string dir in dirs)
                {
                    TreeNode newNode = new TreeNode
                    {
                        Text = Path.GetFileName(dir),
                        Tag = dir
                    };
                    node.Nodes.Add(newNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            PopulateListView(e.Node.Tag.ToString());
        }

        private void PopulateListView(string path)
        {
            listView1.Items.Clear();
            try
            {
                string[] dirs = Directory.GetDirectories(path);
                foreach (string dir in dirs)
                {
                    ListViewItem item = new ListViewItem
                    {
                        Text = Path.GetFileName(dir),
                        Tag = dir,
                        ImageIndex = 0 // Assuming 0 is the folder image index
                    };
                    listView1.Items.Add(item);
                }

                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    ListViewItem item = new ListViewItem
                    {
                        Text = Path.GetFileName(file),
                        Tag = file,
                        ImageIndex = 1 // Assuming 1 is the file image index
                    };
                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string selectedPath = listView1.SelectedItems[0].Tag.ToString();
                if (File.Exists(selectedPath) && Path.GetExtension(selectedPath).ToLower() == ".txt")
                {
                    System.Diagnostics.Process.Start("notepad.exe", selectedPath);
                }
            }
        }
    }
}
