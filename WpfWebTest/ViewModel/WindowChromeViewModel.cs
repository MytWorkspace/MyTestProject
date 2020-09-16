using GalaSoft.MvvmLight;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace WpfWebTest.ViewModel
{
    public class WindowChromeViewModel : ViewModelBase
    {

        private ObservableCollection<HtmlTestModel> observableCollection = new ObservableCollection<HtmlTestModel>();

        public ObservableCollection<HtmlTestModel> HtmlTestModels
        {
            set
            {
                observableCollection = value;
            }
            get
            {
                return observableCollection;
            }
        }

        public void InitHtmlTest()
        {
            WebClient MyWebClient = new WebClient();
            MyWebClient.Encoding = Encoding.UTF8;
            MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据

            Byte[] pageData = MyWebClient.DownloadData("https://task.zbj.com/?k=%E8%BD%AF%E4%BB%B6"); //从指定网站下载数据
            string pageHtml = Encoding.UTF8.GetString(pageData);  //如果获取网站页面采用的是GB2312，则使用这句    

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(pageHtml);
            string msg1 = "";
            var artlist = doc.DocumentNode.SelectNodes("//div[@class='demand-card']");//选择结点集
            int index = 1;
            foreach (var item in artlist)
            {
                //获得标签内的内容
                string aInterText = item.InnerText;
                //解析html
                string tempUrl = item.InnerHtml;

                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(tempUrl);


                var ate = htmlDocument.DocumentNode.SelectSingleNode("//a[@class='prevent-defalut-link']").Attributes["href"].Value;

                HtmlTestModel htmlTestModel = new HtmlTestModel();

                string temp = aInterText.Replace("\n", " ");
                string[] tempresult = CommonUitity.GetStr(temp).Split(' ');

                htmlTestModel.SendTime = tempresult[0];

                int indexStart = tempresult[2].IndexOf("剩");
                int indexEnd = tempresult[2].IndexOf("个");

                try
                {
                    string resultss = tempresult[2].Substring(indexStart + 1, indexEnd - indexStart - 1);

                    if (int.Parse(resultss) == 0) continue;
                }
                catch (Exception)
                {

                }
                htmlTestModel.LessPerson = tempresult[1] + tempresult[2];
                htmlTestModel.FindUser = tempresult[6];
                htmlTestModel.Price = tempresult[5];
                htmlTestModel.Title = tempresult[7];
                htmlTestModel.UserNeed = tempresult[3];

                htmlTestModel.Url = "";
                ////发布时间
                //HtmlNode timeTitle = item.SelectSingleNode("//span[@class='card-pub-time flt']");

                //htmlTestModel.Title = CommonUitity.GetStr(timeTitle.InnerText);
                ////剩余人数
                //HtmlNode personNum = item.SelectSingleNode("//span[@class='card-pub-left frt']");
                //htmlTestModel.LessPerson = CommonUitity.GetStr(personNum.InnerText);
                ////用户需求
                //HtmlNode userTitle = item.SelectSingleNode("//div[@class='demand-card-body']");
                //htmlTestModel.UserNeed = CommonUitity.GetStr(userTitle.InnerText);
                ////需求名称
                //HtmlNode userUse = item.SelectSingleNode("//a[@class='prevent-defalut-link']");
                //// 是否招标 
                //HtmlNode userCall = item.SelectSingleNode("//span[@class='demand-mode']");
                ////价格
                //HtmlNode userPrice = item.SelectSingleNode("//div[@class='demand-price']");
                //htmlTestModel.Price = CommonUitity.GetStr(userPrice.InnerText);
                //// 是否匹配中 
                //HtmlNode userFind = item.SelectSingleNode("//div[@class='demand-price']/span");
                //htmlTestModel.FindUser = CommonUitity.GetStr(userUse.InnerText);

                observableCollection.Add(htmlTestModel);
            }

        }


    }



    public class HtmlTestModel : ViewModelBase
    {

        private string title;

        public string Title
        {

            set
            {

                title = value;
                RaisePropertyChanged();
            }
            get
            {
                return title;
            }
        }

        private string sendTime;

        public string SendTime
        {

            set
            {

                sendTime = value;
                RaisePropertyChanged();
            }
            get
            {
                return sendTime;
            }
        }

        private string lessPerson;

        public string LessPerson
        {

            set
            {

                lessPerson = value;
                RaisePropertyChanged();
            }
            get
            {
                return lessPerson;
            }
        }

        private string price;

        public string Price
        {

            set
            {

                price = value;
                RaisePropertyChanged();
            }
            get
            {
                return price;
            }
        }

        private string userNeed;

        public string UserNeed
        {

            set
            {

                userNeed = value;
                RaisePropertyChanged();
            }
            get
            {
                return userNeed;
            }
        }

        private string findUser;

        public string FindUser
        {

            set
            {

                findUser = value;
                RaisePropertyChanged();
            }
            get
            {
                return findUser;
            }
        }


        private string url;

        public string Url
        {
            set
            {
                url = value;
                RaisePropertyChanged();
            }
            get
            {
                return url;
            }
        }

    }
}
