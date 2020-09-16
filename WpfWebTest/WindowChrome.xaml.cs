using CefSharp.Wpf;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfWebTest.ViewModel;
using Path = System.IO.Path;

namespace WpfWebTest
{
    /// <summary>
    /// WindowChrome.xaml 的交互逻辑
    /// </summary>
    public partial class WindowChrome : Window
    {
        public WindowChrome()
        {
            InitializeComponent();

            InitCEF();

            this.Loaded += WindowChrome_Loaded;

            DataContext = vm = new WindowChromeViewModel();
        }
        private ChromiumWebBrowser webView;

        private WindowChromeViewModel vm;

        private bool resultBool = false;

        private void InitCEF()
        {
            if (resultBool) { return; }

            resultBool = true;

            var setting = new CefSettings();
            // 设置语言
            setting.Locale = "zh-CN";
            //cef设置userAgent
            setting.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36";
            //配置浏览器路径
            setting.BrowserSubprocessPath = AppDomain.CurrentDomain.BaseDirectory + @"x86\CefSharp.BrowserSubprocess.exe";
            setting.CefCommandLineArgs.Add("proxy-auto-detect", "0");
            setting.CefCommandLineArgs.Add("no-proxy-server", "1");
            CefSharp.Cef.Initialize(setting, performDependencyCheck: true, browserProcessHandler: null);

            webView = new CefSharp.Wpf.ChromiumWebBrowser();
            grid.Children.Add(webView);
        }

        private void WindowChrome_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            webView.Address = "https://task.zbj.com/hall/bid";
        }


        private void GetgetElementsByClassName(string key, string value)
        {
            webView.GetBrowser().MainFrame.ExecuteJavaScriptAsync("document.getElementsByClassName('" + key + "')[0].value = '" + value + "';");
        }

        private void ClickElement(string key)
        {
            webView.GetBrowser().MainFrame.ExecuteJavaScriptAsync("document.getElementsByClassName('" + key + "')[0].click();");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GetgetElementsByClassName("search-value", "软件");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ClickElement("j-search search-btn");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            /* WebClient MyWebClient = new WebClient();
             MyWebClient.Encoding = Encoding.UTF8;
             MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据

             Byte[] pageData = MyWebClient.DownloadData("https://task.zbj.com/?k=%E8%BD%AF%E4%BB%B6"); //从指定网站下载数据
             string pageHtml = Encoding.UTF8.GetString(pageData);  //如果获取网站页面采用的是GB2312，则使用这句    

             HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
             doc.LoadHtml(pageHtml);
             string msg1 = "";
             var artlist = doc.DocumentNode.SelectNodes("//div[@class='demand-card']");//选择结点集

             foreach (var item in artlist)
             {
                 //获得标签内的内容
                 string aInterText = item.InnerText;
                 //解析html
                 HtmlDocument tempHtml = new HtmlDocument();
                 tempHtml.LoadHtml(aInterText);

                 //发布时间
                 HtmlNode timeTitle = doc.DocumentNode.SelectSingleNode("//span[@class='card-pub-time flt']");

                 //剩余人数
                 HtmlNode personNum = doc.DocumentNode.SelectSingleNode("//span[@class='card-pub-left frt']");

                 //用户需求
                 HtmlNode userTitle = doc.DocumentNode.SelectSingleNode("//div[@class='demand-card-body']");

                 //需求名称
                 HtmlNode userUse = doc.DocumentNode.SelectSingleNode("//a[@class='prevent-defalut-link']");

                 // 是否招标 
                 HtmlNode userCall = doc.DocumentNode.SelectSingleNode("//span[@class='demand-mode']");

                 //价格
                 HtmlNode userPrice = doc.DocumentNode.SelectSingleNode("//div[@class='demand-price']");

                 // 是否匹配中 
                 HtmlNode userFind = doc.DocumentNode.SelectSingleNode("//div[@class='demand-price']/span");


             }*/

            vm.InitHtmlTest();
        }


    }
}
