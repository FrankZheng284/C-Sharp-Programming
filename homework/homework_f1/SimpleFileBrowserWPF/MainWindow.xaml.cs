using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FileBrowser
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadDirectories();
        }

        private void LoadDirectories()
        {
            DirectoryInfo userDir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
            foreach (DirectoryInfo dir in userDir.GetDirectories())
            {
                TreeViewItem item = new TreeViewItem();
                item.Header = dir.Name;
                item.Tag = dir.FullName;
                item.Items.Add(null);
                item.Expanded += new RoutedEventHandler(FolderExpanded);
                FoldersTreeView.Items.Add(item);
            }

            // 设置TreeView选中事件处理器
            FoldersTreeView.SelectedItemChanged += FoldersTreeView_SelectedItemChanged;

            // 设置ListView双击事件处理器
            FilesListView.MouseDoubleClick += FilesListView_MouseDoubleClick;
        }

        private void FoldersTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeViewItem item = (TreeViewItem)e.NewValue;
            if (item.Items.Count == 1 && item.Items[0] == null)
            {
                item.Items.Clear();
                DirectoryInfo dir = new DirectoryInfo((string)item.Tag);
                foreach (DirectoryInfo subDir in dir.GetDirectories())
                {
                    TreeViewItem subItem = new TreeViewItem();
                    subItem.Header = subDir.Name;
                    subItem.Tag = subDir.FullName;
                    subItem.Items.Add(null);
                    subItem.Expanded += new RoutedEventHandler(FolderExpanded);
                    item.Items.Add(subItem);
                }
            }
        }
        private void FolderExpanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;
            if (item.Items.Count == 1 && item.Items[0] == null)
            {
                item.Items.Clear();
                DirectoryInfo dir = new DirectoryInfo((string)item.Tag);
                foreach (DirectoryInfo subDir in dir.GetDirectories())
                {
                    TreeViewItem subItem = new TreeViewItem();
                    subItem.Header = subDir.Name;
                    subItem.Tag = subDir.FullName;
                    subItem.Items.Add(null);
                    subItem.Expanded += new RoutedEventHandler(FolderExpanded);
                    item.Items.Add(subItem);
                }
            }
        }

        private void FilesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // 打开选中的txt文件
            if (FilesListView.SelectedItem != null)
            {
                string fileName = (string)((ListViewItem)FilesListView.SelectedItem).Tag;
                if (fileName.EndsWith(".txt"))
                {
                    System.Diagnostics.Process.Start(fileName);
                }
            }
        }
    }
}
