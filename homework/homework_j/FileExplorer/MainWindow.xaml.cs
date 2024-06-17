using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FileExplorer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // 加载根目录
            LoadDirectoryTree();
        }

        private void LoadDirectoryTree()
        {
            // 获取用户目录路径
            string userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            // 创建根节点
            var rootDirectory = new DirectoryInfo(userDirectory);
            var rootNode = new TreeViewItem
            {
                Header = rootDirectory.Name,
                Tag = rootDirectory.FullName
            };

            // 添加根节点到树形视图
            treeView.Items.Add(rootNode);

            // 加载根节点下的子目录
            LoadSubDirectories(rootDirectory, rootNode);
        }

        private void LoadSubDirectories(DirectoryInfo parentDirectory, ItemsControl parentNode)
        {
            try
            {
                // 获取目录中的子目录
                DirectoryInfo[] subDirectories = parentDirectory.GetDirectories();

                foreach (var directory in subDirectories)
                {
                    // 创建子目录节点
                    var directoryNode = new TreeViewItem
                    {
                        Header = directory.Name,
                        Tag = directory.FullName
                    };

                    // 添加到父节点
                    parentNode.Items.Add(directoryNode);

                    // 递归加载子目录
                    LoadSubDirectories(directory, directoryNode);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // 处理权限访问异常
                MessageBox.Show($"Access to {parentDirectory.Name} is denied.");
            }
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
                // 当树形视图选择项改变时，加载右侧列表内容
                var selectedItem = (TreeViewItem)treeView.SelectedItem;

                if (selectedItem != null)
                {
                    string selectedDirectory = (string)selectedItem.Tag;

                    // 清空右侧列表
                    listView.Items.Clear();

                    // 加载选中目录的子文件夹和文件
                    LoadFilesAndSubDirectories(selectedDirectory);
                }
            }
        

        private void LoadFilesAndSubDirectories(string directoryPath)
        {
            try
            {
                // 获取目录下的文件夹和文件
                DirectoryInfo dirInfo = new DirectoryInfo(directoryPath);

                // 加载子文件夹
                DirectoryInfo[] subDirectories = dirInfo.GetDirectories();
                foreach (var subDir in subDirectories)
                {
                    listView.Items.Add(new FileSystemItemViewModel
                    {
                        Name = subDir.Name,
                        Type = "Folder",
                        Size = "",
                        DateModified = subDir.LastWriteTime.ToString()
                    });
                }

                // 加载文件
                FileInfo[] files = dirInfo.GetFiles();
                foreach (var file in files)
                {
                    listView.Items.Add(new FileSystemItemViewModel
                    {
                        Name = file.Name,
                        Type = "File",
                        Size = file.Length.ToString(),
                        DateModified = file.LastWriteTime.ToString()
                    });
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show($"Access to {directoryPath} is denied.");
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show($"Directory {directoryPath} not found.");
            }
        }

        private void listView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // 双击打开选中的txt文件
            var selectedItem = (FileSystemItemViewModel)listView.SelectedItem;

            if (selectedItem != null && selectedItem.Type == "File" && selectedItem.Name.EndsWith(".txt"))
            {
                string filePath = Path.Combine((string)((TreeViewItem)treeView.SelectedItem).Tag, selectedItem.Name);

                try
                {
                    // 打开txt文件
                    System.Diagnostics.Process.Start(filePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening file: {ex.Message}");
                }
            }
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {  

        }
    }

    // 文件和文件夹的视图模型
    public class FileSystemItemViewModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Size { get; set; }
        public string DateModified { get; set; }
    }
}
