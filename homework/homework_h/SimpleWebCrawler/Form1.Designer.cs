namespace SimpleWebCrawler
{
    partial class Form1
    {
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInitialUrl;
        private System.Windows.Forms.Button btnStartCrawling;
        private System.Windows.Forms.ListBox listBoxCrawledUrls;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBoxErrorUrls;

        private void InitializeComponent()
        {
            label1 = new Label();
            txtInitialUrl = new TextBox();
            btnStartCrawling = new Button();
            listBoxCrawledUrls = new ListBox();
            label2 = new Label();
            listBoxErrorUrls = new ListBox();
            label3 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(36, 45);
            label1.Margin = new Padding(9, 0, 9, 0);
            label1.Name = "label1";
            label1.Size = new Size(169, 39);
            label1.TabIndex = 0;
            label1.Text = "Initial URL:";
            // 
            // txtInitialUrl
            // 
            txtInitialUrl.Location = new Point(255, 36);
            txtInitialUrl.Margin = new Padding(9, 9, 9, 9);
            txtInitialUrl.Name = "txtInitialUrl";
            txtInitialUrl.Size = new Size(892, 46);
            txtInitialUrl.TabIndex = 1;
            // 
            // btnStartCrawling
            // 
            btnStartCrawling.Location = new Point(1173, 30);
            btnStartCrawling.Margin = new Padding(9, 9, 9, 9);
            btnStartCrawling.Name = "btnStartCrawling";
            btnStartCrawling.Size = new Size(225, 69);
            btnStartCrawling.TabIndex = 2;
            btnStartCrawling.Text = "Start Crawling";
            btnStartCrawling.UseVisualStyleBackColor = true;
            btnStartCrawling.Click += btnStartCrawling_Click;
            // 
            // listBoxCrawledUrls
            // 
            listBoxCrawledUrls.FormattingEnabled = true;
            listBoxCrawledUrls.Location = new Point(45, 177);
            listBoxCrawledUrls.Margin = new Padding(9, 9, 9, 9);
            listBoxCrawledUrls.Name = "listBoxCrawledUrls";
            listBoxCrawledUrls.Size = new Size(652, 1018);
            listBoxCrawledUrls.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(36, 129);
            label2.Margin = new Padding(9, 0, 9, 0);
            label2.Name = "label2";
            label2.Size = new Size(220, 39);
            label2.TabIndex = 4;
            label2.Text = "Crawled URLs:";
            // 
            // listBoxErrorUrls
            // 
            listBoxErrorUrls.FormattingEnabled = true;
            listBoxErrorUrls.Location = new Point(753, 177);
            listBoxErrorUrls.Margin = new Padding(9, 9, 9, 9);
            listBoxErrorUrls.Name = "listBoxErrorUrls";
            listBoxErrorUrls.Size = new Size(637, 1018);
            listBoxErrorUrls.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(753, 129);
            label3.Margin = new Padding(9, 0, 9, 0);
            label3.Name = "label3";
            label3.Size = new Size(173, 39);
            label3.TabIndex = 6;
            label3.Text = "Error URLs:";
            label3.Click += label3_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(18F, 39F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1434, 1233);
            Controls.Add(label3);
            Controls.Add(listBoxErrorUrls);
            Controls.Add(label2);
            Controls.Add(listBoxCrawledUrls);
            Controls.Add(btnStartCrawling);
            Controls.Add(txtInitialUrl);
            Controls.Add(label1);
            Margin = new Padding(9, 9, 9, 9);
            Name = "Form1";
            Text = "Simple Web Crawler";
            ResumeLayout(false);
            PerformLayout();
        }

        private Label label3;
    }
}
