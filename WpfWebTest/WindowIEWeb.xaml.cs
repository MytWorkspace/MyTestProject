using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Utility;
using WebBrowser = System.Windows.Controls.WebBrowser;

namespace WpfWebTest
{
    /// <summary>
    /// WindowIEWeb.xaml 的交互逻辑
    /// </summary>
    public partial class WindowIEWeb : Window
    {
        public WindowIEWeb()
        {
            InitializeComponent();

            grid.Children.Add(wbBrowser);
        }
        private WebBrowser wbBrowser = new WebBrowser();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            wbBrowser.Navigate(txtBox.Text);
        }

        private CatchUtil catchUtil = new CatchUtil();

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EltInfo eltInfo = new EltInfo();
            eltInfo.tag = "search-value";
            eltInfo.str = "软件";
            HtmlElement et = catchUtil.GetElementByEltInfo((HtmlDocument)wbBrowser.Document, eltInfo);
            //Mmove(et.OffsetRectangle.Left, et.OffsetRectangle.Top);
            et.Focus();
            SendKeys.Send(eltInfo.str);
        }
    }
}
