using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SimpleWebCrawler
{
    public partial class Form1 : Form
    {
        HashSet<string> crawledUrls;
        HashSet<string> errorUrls;

        public Form1()
        {
            InitializeComponent();

            crawledUrls = new HashSet<string>();
            errorUrls = new HashSet<string>();
        }

        private async void btnStartCrawling_Click(object sender, EventArgs e)
        {
            string initialUrl = txtInitialUrl.Text.Trim();

            // ���֮ǰ����ȡ��¼
            crawledUrls.Clear();
            errorUrls.Clear();
            listBoxCrawledUrls.Items.Clear();
            listBoxErrorUrls.Items.Clear();

            // ��������
            await Crawl(initialUrl);
        }

        private async System.Threading.Tasks.Task Crawl(string url)
        {
            try
            {
                // ����HttpClientʵ��
                using (HttpClient client = new HttpClient())
                {
                    // ������ҳ����
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string html = await response.Content.ReadAsStringAsync();

                    // ��URL��ӵ�����ȡ�б���
                    crawledUrls.Add(url);
                    listBoxCrawledUrls.Items.Add(url);

                    // ����HTML�ı���ȡҳ������
                    ParseHtmlAndDisplayLinks(html, url);
                }
            }
            catch (Exception ex)
            {
                // ��¼�����URL
                errorUrls.Add(url);
                listBoxErrorUrls.Items.Add(url + " - " + ex.Message);
            }
        }

        private void ParseHtmlAndDisplayLinks(string html, string baseUrl)
        {
            // ʹ��������ʽ����HTML��ȡ��������
            string pattern = @"<a\s+(?:[^>]*?\s+)?href=""(http[^""]*)""";
            MatchCollection matches = Regex.Matches(html, pattern, RegexOptions.IgnoreCase);

            foreach (Match match in matches)
            {
                string href = match.Groups[1].Value;

                // ת�����·��Ϊ����·��
                Uri baseUri = new Uri(baseUrl);
                Uri absoluteUri;
                if (Uri.TryCreate(baseUri, href, out absoluteUri))
                {
                    href = absoluteUri.AbsoluteUri;
                }

                // ȷ��������HTTP��HTTPSЭ������ӣ�����δ����ȡ��
                if ((absoluteUri.Scheme == Uri.UriSchemeHttp || absoluteUri.Scheme == Uri.UriSchemeHttps) && !crawledUrls.Contains(href))
                {
                    // ��ʾ�������������ӣ��������н�һ����ȡ��
                    listBoxCrawledUrls.Items.Add(href);
                    crawledUrls.Add(href);
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
