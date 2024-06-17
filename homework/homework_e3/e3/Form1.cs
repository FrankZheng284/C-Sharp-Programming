using System;
using System.IO;
using System.Windows.Forms;

namespace e3
{
    public partial class Form1 : Form
    {
        private string filePath1;
        private string filePath2;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnSelectFile1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath1 = openFileDialog.FileName;
                txtFilePath1.Text = filePath1;
            }
        }

        private void btnSelectFile2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath2 = openFileDialog.FileName;
                txtFilePath2.Text = filePath2;
            }
        }

        private void btnMergeFiles_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(filePath1) || string.IsNullOrEmpty(filePath2))
            {
                MessageBox.Show("Please select both files before merging.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string dataDirectory = Path.Combine(exeDirectory, "Data");

                if (!Directory.Exists(dataDirectory))
                {
                    Directory.CreateDirectory(dataDirectory);
                }

                string mergedFilePath = Path.Combine(dataDirectory, "MergedFile.txt");

                using (StreamWriter writer = new StreamWriter(mergedFilePath, false))
                {
                    foreach (string filePath in new[] { filePath1, filePath2 })
                    {
                        using (StreamReader reader = new StreamReader(filePath))
                        {
                            writer.Write(reader.ReadToEnd());
                        }
                    }
                }

                MessageBox.Show($"Files have been merged successfully into {mergedFilePath}.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtFilePath1_TextChanged( object sender, EventArgs e)
        { 
        
        }
        private void txtFilePath2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
