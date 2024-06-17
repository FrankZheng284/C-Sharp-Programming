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

            // ���֮ǰ�ļ�¼
            crawledUrls.Clear();
            errorUrls.Clear();
            phoneNumbers.Clear();
            listBoxCrawledUrls.Items.Clear();
            listBoxPhoneNumbers.Items.Clear();

            // ��������������
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

                // ��ȡ�������ҳ���е�����
                List<string> links = ExtractLinksFromSearchResults(html);

                // ������ȡ����ҳ���еĵ绰����
                var tasks = links.Select(url => CrawlUrlForPhones(url)).ToList();
                await Task.WhenAll(tasks);

                // ��ʾ�绰����
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

                // ��ȡ�绰����
                ExtractPhonesFromHtml(html, url);

                // ��URL��ӵ�����ȡ�б���
                crawledUrls.Add(url);
                Invoke(new Action(() => listBoxCrawledUrls.Items.Add(url)));
            }
            catch (Exception ex)
            {
                // ��¼�����URL
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
                if (!phoneNumbers.Contains(phone) && phone.Length >= 10 && phone.Length <= 15) // �򵥵ĵ绰���볤�ȹ���
                {
                    phoneNumbers.Add(phone);
                    Invoke(new Action(() => listBoxPhoneNumbers.Items.Add($"{phone} (found at {url})")));
                }
            }
        }
    }
}
