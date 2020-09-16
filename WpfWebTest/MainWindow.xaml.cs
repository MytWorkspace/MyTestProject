using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfWebTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WebClient MyWebClient = new WebClient();
            MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据


            Byte[] pageData = MyWebClient.DownloadData("http://top.baidu.com/buzz?b=1&c=513&fr=topbuzz"); //从指定网站下载数据
            string pageHtml = Encoding.Default.GetString(pageData);  //如果获取网站页面采用的是GB2312，则使用这句    

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(pageHtml);
            string msg1 = "";
            var artlist = doc.DocumentNode.SelectNodes("//a[@class='list-title']");//选择结点集
            foreach (var item in artlist)
            {
                msg1 = msg1 + "," + item.InnerText;//拼装关键词
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            string url = "https://blog.csdn.net/u011127019/article/details/52571317";
            HttpWebRequest request = WebRequest.CreateHttp(url);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/40.0.2214.111 Safari/537.36";
            request.Referer = "http://www.imooc.com/video/11555";
            //request.Headers.Add("cookie", "imooc_uuid=ec12ea83-f2c0-4c14-9dd1-55fbefea18a0; imooc_isnew_ct=1468544598; loginstate=1; apsid=g2ZmJlMTE1MmExYWEwODE0ZTAzNTZmNjJmZDMzN2MAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAMjI2MDQ1NQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAxMDA3MTczMTMyQHFxLmNvbQAAAAAAAAAAAAAAAAAAAGQwOTNjNWUwNjA5MjI3ZDk5MjIxNzc3OWUwYTBlODEzk%2BK8V5PivFc%3DYj; last_login_username=1007173132%40qq.com; bdshare_firstime=1472599723791; PHPSESSID=gqgpva8utntcni03v2nkk69441; jwplayer.volume=100; imooc_isnew=2; cvde=57d5eee17b1e2-41; Hm_lvt_f0cfcccd7b1393990c78efdeebff3968=1473207620,1473291733,1473638111,1473809917; Hm_lpvt_f0cfcccd7b1393990c78efdeebff3968=1473814335; IMCDNS=0");


            request.Headers.Add("Accept-Encoding", "identity;q=1, *;q=0");
            request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
            //request.Headers.Add("Connection", "keep-alive"); //添加失败
            //request.Connection = "keep-alive";               //添加失败
            request.KeepAlive = true;                          //设置成功
                                                               // request.Headers.Add("Range", "56f105a0-33c14ce");//添加失败
            request.AddRange(0, 1048575);                      //添加成功
            request.Headers.Add("If-None-Match", "56f105a0-33c14ce");
            request.Headers.Add("Cache-Control", "max-age=0");
            //request.Headers.Add("If-Modified-Since", "Tue, 22 Mar 2016 08:43:12 GMT"); //添加失败
            request.IfModifiedSince = DateTime.Now;

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WebCapture webCapture = new WebCapture();
            webCapture.Show();
        }
    }
}
