using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class WebClientHelper
    {
        public static string DownloadString(string url)
        {
            WebClient wc = new WebClient();
            //wc.BaseAddress = url;   //设置根目录
            wc.Encoding = Encoding.UTF8;    //设置按照何种编码访问，如果不加此行，获取到的字符串中文将是乱码
            string str = wc.DownloadString(url);
            return str;
        }
        public static string DownloadStreamString(string url)
        {
            WebClient wc = new WebClient();
            wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36");
            Stream objStream = wc.OpenRead(url);
            StreamReader _read = new StreamReader(objStream, Encoding.UTF8);    //新建一个读取流，用指定的编码读取，此处是utf-8
            string str = _read.ReadToEnd();
            objStream.Close();
            _read.Close();
            return str;
        }

        public static void DownloadFile(string url, string filename)
        {
            WebClient wc = new WebClient();
            wc.DownloadFile(url, filename);     //下载文件
        }

        public static void DownloadData(string url, string filename)
        {
            WebClient wc = new WebClient();
            byte[] bytes = wc.DownloadData(url);   //下载到字节数组
            FileStream fs = new FileStream(filename, FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Flush();
            fs.Close();
        }

        public static void DownloadFileAsync(string url, string filename)
        {
            WebClient wc = new WebClient();
            wc.DownloadFileCompleted += DownCompletedEventHandler;
            wc.DownloadFileAsync(new Uri(url), filename);
            Console.WriteLine("下载中。。。");
        }
        private static void DownCompletedEventHandler(object sender, AsyncCompletedEventArgs e)
        {
            Console.WriteLine(sender.ToString());   //触发事件的对象
            Console.WriteLine(e.UserState);
            Console.WriteLine(e.Cancelled);
            Console.WriteLine("异步下载完成！");
        }

        public static void DownloadFileAsync2(string url, string filename)
        {
            WebClient wc = new WebClient();
            wc.DownloadFileCompleted += (sender, e) =>
            {
                Console.WriteLine("下载完成!");
                Console.WriteLine(sender.ToString());
                Console.WriteLine(e.UserState);
                Console.WriteLine(e.Cancelled);
            };
            wc.DownloadFileAsync(new Uri(url), filename);
            Console.WriteLine("下载中。。。");
        }


    }
}
