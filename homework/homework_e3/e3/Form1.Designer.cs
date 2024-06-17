namespace e3
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnSelectFile1 = new Button();
            btnSelectFile2 = new Button();
            btnMergeFiles = new Button();
            txtFilePath2 = new TextBox();
            txtFilePath1 = new TextBox();
            SuspendLayout();
            // 
            // btnSelectFile1
            // 
            btnSelectFile1.Location = new Point(102, 86);
            btnSelectFile1.Name = "btnSelectFile1";
            btnSelectFile1.Size = new Size(188, 58);
            btnSelectFile1.TabIndex = 0;
            btnSelectFile1.Text = "Select File";
            btnSelectFile1.UseVisualStyleBackColor = true;
            btnSelectFile1.Click += btnSelectFile1_Click;
            // 
            // btnSelectFile2
            // 
            btnSelectFile2.Location = new Point(102, 202);
            btnSelectFile2.Name = "btnSelectFile2";
            btnSelectFile2.Size = new Size(188, 58);
            btnSelectFile2.TabIndex = 1;
            btnSelectFile2.Text = "Select File";
            btnSelectFile2.UseVisualStyleBackColor = true;
            btnSelectFile2.Click += btnSelectFile2_Click;
            // 
            // btnMergeFiles
            // 
            btnMergeFiles.Location = new Point(307, 327);
            btnMergeFiles.Name = "btnMergeFiles";
            btnMergeFiles.Size = new Size(188, 58);
            btnMergeFiles.TabIndex = 2;
            btnMergeFiles.Text = "merge";
            btnMergeFiles.UseVisualStyleBackColor = true;
            // 
            // txtFilePath2
            // 
            txtFilePath2.Location = new Point(404, 209);
            txtFilePath2.Name = "txtFilePath2";
            txtFilePath2.Size = new Size(250, 46);
            txtFilePath2.TabIndex = 3;
//            txtFilePath2.TextChanged += txtFilePath2_TextChanged;
            // 
            // txtFilePath1
            // 
            txtFilePath1.Location = new Point(404, 93);
            txtFilePath1.Name = "txtFilePath1";
            txtFilePath1.Size = new Size(250, 46);
            txtFilePath1.TabIndex = 4;
//            txtFilePath1.TextChanged += txtFilePath1_TextChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(18F, 39F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtFilePath1);
            Controls.Add(txtFilePath2);
            Controls.Add(btnMergeFiles);
            Controls.Add(btnSelectFile2);
            Controls.Add(btnSelectFile1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSelectFile1;
        private Button btnSelectFile2;
        private Button btnMergeFiles;
        private TextBox txtFilePath2;
        private TextBox txtFilePath1;
    }
}
