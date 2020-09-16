using Fiddler;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace WpfCatchWeb
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;

        }

        //设置fiddler的启动标识 需要允许远程计算机连接和对https的解密
        public FiddlerCoreStartupFlags oFCSF = FiddlerCoreStartupFlags.AllowRemoteClients | FiddlerCoreStartupFlags.DecryptSSL;

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //  SetSSLcer();

            InstallCertificate();

            FiddlerApplication.Prefs.SetBoolPref("fiddler.network.streaming.abortifclientaborts", true);

            // 创建一个https侦听器，用于伪装成https服务器

            //Fiddler.CertMaker.trustRootCert();

            FiddlerApplication.BeforeRequest += FiddlerApplication_BeforeRequest;

            FiddlerApplication.OnWebSocketMessage += FiddlerApplication_OnWebSocketMessage;

            FiddlerApplication.Startup(8080, true, true);

            this.Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            FiddlerApplication.Shutdown();

        }

        /// <summary>
        /// 获取请求信息
        /// </summary>
        /// <param name="oSession"></param>
        private void FiddlerApplication_BeforeRequest(Session oSession)
        {
            byte[] resultBoty = oSession.responseBodyBytes;
            if (resultBoty == null) return;

            string temp = Encoding.Unicode.GetString(resultBoty);

            Dispatcher.BeginInvoke(new Action(() =>
            {
                textBox.Text = temp;
            }));
        }
        /// <summary>
        /// 获取到数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FiddlerApplication_OnWebSocketMessage(object sender, WebSocketMessageEventArgs e)
        {


        }



        private bool SetSSLcer()
        {
            var a = new BCCertMaker.BCCertMaker();
            a.CreateRootCertificate();

            return a.TrustRootCertificate();
        }

        public bool InstallCertificate()
        {
            if (!CertMaker.rootCertExists())
            {
                if (!CertMaker.createRootCert())
                    return false;
                if (!CertMaker.trustRootCert())
                    return false;
            }
            return true;
        }
    }
}
