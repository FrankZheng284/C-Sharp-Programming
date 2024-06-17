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

            // 清空之前的爬取记录
            crawledUrls.Clear();
            errorUrls.Clear();
            listBoxCrawledUrls.Items.Clear();
            listBoxErrorUrls.Items.Clear();

            // 启动爬虫
            await Crawl(initialUrl);
        }

        private async System.Threading.Tasks.Task Crawl(string url)
        {
            try
            {
                // 创建HttpClient实例
                using (HttpClient client = new HttpClient())
                {
                    // 下载网页内容
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string html = await response.Content.ReadAsStringAsync();

                    // 将URL添加到已爬取列表中
                    crawledUrls.Add(url);
                    listBoxCrawledUrls.Items.Add(url);

                    // 解析HTML文本获取页面内容
                    ParseHtmlAndDisplayLinks(html, url);
                }
            }
            catch (Exception ex)
            {
                // 记录错误的URL
                errorUrls.Add(url);
                listBoxErrorUrls.Items.Add(url + " - " + ex.Message);
            }
        }

        private void ParseHtmlAndDisplayLinks(string html, string baseUrl)
        {
            // 使用正则表达式解析HTML获取所有链接
            string pattern = @"<a\s+(?:[^>]*?\s+)?href=""(http[^""]*)""";
            MatchCollection matches = Regex.Matches(html, pattern, RegexOptions.IgnoreCase);

            foreach (Match match in matches)
            {
                string href = match.Groups[1].Value;

                // 转换相对路径为绝对路径
                Uri baseUri = new Uri(baseUrl);
                Uri absoluteUri;
                if (Uri.TryCreate(baseUri, href, out absoluteUri))
                {
                    href = absoluteUri.AbsoluteUri;
                }

                // 确保链接是HTTP或HTTPS协议的链接，并且未被爬取过
                if ((absoluteUri.Scheme == Uri.UriSchemeHttp || absoluteUri.Scheme == Uri.UriSchemeHttps) && !crawledUrls.Contains(href))
                {
                    // 显示解析出来的链接（但不进行进一步爬取）
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
