using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniversityPhoneCrawler
{
    public partial class Form1 : Form
    {
        private HashSet<string> crawledUrls;
        private HashSet<string> errorUrls;
        private HashSet<string> phoneNumbers;
        private HttpClient httpClient;

        public Form1()
        {
            InitializeComponent();
            crawledUrls = new HashSet<string>();
            errorUrls = new HashSet<string>();
            phoneNumbers = new HashSet<string>();
            httpClient = new HttpClient();
        }

        private async void btnStartSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtKeyword.Text.Trim();

            // 清空之前的记录
            crawledUrls.Clear();
            errorUrls.Clear();
            phoneNumbers.Clear();
            listBoxCrawledUrls.Items.Clear();
            listBoxPhoneNumbers.Items.Clear();

            // 启动搜索和爬虫
            await StartCrawling(keyword);
        }

        private async Task StartCrawling(string keyword)
        {
            string searchUrl = $"https://www.bing.com/search?q={Uri.EscapeDataString(keyword + " site:.edu.cn")}";
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(searchUrl);
                response.EnsureSuccessStatusCode();
                string html = await response.Content.ReadAsStringAsync();

                // 提取搜索结果页面中的链接
                List<string> links = ExtractLinksFromSearchResults(html);

                // 并行爬取链接页面中的电话号码
                var tasks = links.Select(url => CrawlUrlForPhones(url)).ToList();
                await Task.WhenAll(tasks);

                // 显示电话号码
                foreach (var phone in phoneNumbers)
                {
                    listBoxPhoneNumbers.Items.Add(phone);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private List<string> ExtractLinksFromSearchResults(string html)
        {
            List<string> links = new List<string>();
            string pattern = @"<a\s+(?:[^>]*?\s+)?href=""(https?://[^""]+?)""";

            MatchCollection matches = Regex.Matches(html, pattern, RegexOptions.IgnoreCase);
            foreach (Match match in matches)
            {
                string href = match.Groups[1].Value;
                if (Uri.TryCreate(href, UriKind.Absolute, out Uri uri))
                {
                    links.Add(uri.AbsoluteUri);
                }
            }

            return links;
        }

        private async Task CrawlUrlForPhones(string url)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string html = await response.Content.ReadAsStringAsync();

                // 提取电话号码
                ExtractPhonesFromHtml(html, url);

                // 将URL添加到已爬取列表中
                crawledUrls.Add(url);
                Invoke(new Action(() => listBoxCrawledUrls.Items.Add(url)));
            }
            catch (Exception ex)
            {
                // 记录错误的URL
                errorUrls.Add(url);
                Invoke(new Action(() => listBoxCrawledUrls.Items.Add(url + " - Error")));
            }
        }

        private void ExtractPhonesFromHtml(string html, string url)
        {
            string pattern = @"((\+?\d{1,4}[-.\s]?)?(?:\d{2,4}[-.\s]?){2,3}\d{2,4})";
            MatchCollection matches = Regex.Matches(html, pattern, RegexOptions.IgnoreCase);
            foreach (Match match in matches)
            {
                string phone = match.Groups[1].Value;
                if (!phoneNumbers.Contains(phone) && phone.Length >= 10 && phone.Length <= 15) // 简单的电话号码长度过滤
                {
                    phoneNumbers.Add(phone);
                    Invoke(new Action(() => listBoxPhoneNumbers.Items.Add($"{phone} (found at {url})")));
                }
            }
        }
    }
}
