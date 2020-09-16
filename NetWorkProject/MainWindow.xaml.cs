using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
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

namespace NetWorkProject
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.ShowInTaskbar = false;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Internet.Status)
            {
                MessageBox.Show("网络正常");
            }
            else
            {
                MessageBox.Show("网络异常");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string ip = "";
            foreach (var item in Internet.GetAddressIP())
            {
                ip += item + "  ";
            }

            MessageBox.Show("IP地址{0}" + ip);
        }
        private HideTaskmgrList hideTaskmgrList = new HideTaskmgrList();
        /// <summary>
        /// 进程隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            hideTaskmgrList.ProcessName = "NetWorkProject.exe";
            hideTaskmgrList.Star();
        }

        /// <summary>
        ///  获取网络 ip地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                //string ip = "";

                string pageHtml = NewMethod();


                //string pageHtml = Encoding.UTF8.GetString(pageData); //如果获取网站页面采用的是UTF-8，则使用这句

                //string[] str = HtmlHelper.GetElementsByTagName(pageHtml, "h2");

                // string[] str1 = str[0].Replace("<h2>", "").Split(',');
                //ip = str1[0];
                // Response.Write(ip);

                string s = GetStr(pageHtml, "<h2>", "</h2>");
                //Response.Write(s);
                Console.WriteLine(s);

                MessageBox.Show(s);

            }
            catch (WebException webEx)
            {
                webEx.Message.ToString();
            }

        }

        private static string NewMethod()
        {
            WebClient MyWebClient = new WebClient();
            MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据

            Byte[] pageData = MyWebClient.DownloadData("http://www.net.cn/static/customercare/yourip.asp"); //从指定网站下载数据

            string pageHtml = Encoding.Default.GetString(pageData);  //如果获取网站页面采用的是GB2312，则使用这句
            return pageHtml;
        }

        #region 网络获取开始
        public string GetStr(string Content, string start, string end)
        {
            var posStart = Content.IndexOf(start);
            var posEnd = Content.IndexOf(end);
            return Content.Substring(posStart, (posEnd - posStart + end.Length));
        }

      

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //获取ip和地理信息
            string url = "http://pv.sohu.com/cityjson";
            WebRequest wRequest = WebRequest.Create(url);
            wRequest.Method = "GET";
            wRequest.ContentType = "text/html;charset=UTF-8";
            WebResponse wResponse = wRequest.GetResponse();
            Stream stream = wResponse.GetResponseStream();
            StreamReader reader = new StreamReader(stream, System.Text.Encoding.Default);
            string str = reader.ReadToEnd();   //url返回的值  

            reader.Close();
            wResponse.Close();
            //var returnCitySN = {"cip": "113.57.68.117", "cid": "420100", "cname": "湖北省武汉市"};
            // Response.Write(str);

            var start = str.IndexOf('{');
            var end = str.IndexOf('}');

            str = str.Substring(start, (end - start) + 1);
            //{"cip": "113.57.68.117", "cid": "420100", "cname": "湖北省武汉市"}
            //Response.Write(str);

            //湖北省武汉市
            JObject jonObj = JObject.Parse(str);
            //Response.Write(jonObj["cname"].ToString() + "  " + jonObj["cip"].ToString());

            Console.WriteLine("cname="+jonObj["cname"] +"    "+jonObj["cip"]);

            MessageBox.Show("cname=" + jonObj["cname"] + "    " + jonObj["cip"]);
        }
        #endregion 网络获取结束



    
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Webclient.IpAddressSearchWebService serviceReference1 = new Webclient.IpAddressSearchWebService();

            string ip = GetStr(NewMethod(), "<h2>", "</h2>");
            string [] temp=   serviceReference1.getCountryCityByIp(ip);

            MessageBox.Show("temp1="+temp[0] +" temp2="+temp[1]);
        }
    }


    /// <summary>
    /// 判断网络相关类
    /// </summary>
    public static class Internet
    {
        [DllImport("winInet.dll")]
        private static extern bool InternetGetConnectedState(ref int dwFlag, int dwReserved);

        /// <summary>
        /// 返回网络状态
        /// </summary>
        public static bool Status
        {
            get
            {
                Int32 dwFlag = new int();
                if (!InternetGetConnectedState(ref dwFlag, 0))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public static List<string> GetAddressIP()
        {
            ///获取本地的IP地址
            List<string> AddressIP = new List<string>();
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP.Add(_IPAddress.ToString());
                }
            }
            return AddressIP;
        }
    }
}
