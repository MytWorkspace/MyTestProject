using AduSkin.Controls.Metro;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TestPPOE
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        public static MainWindow MainWindowVM;

        public MainWindow()
        {
            InitializeComponent();

            MainWindowVM = this;

            this.Closing += MainWindow_Closing;



            this.Loaded += MainWindow_Loaded;

            this.SourceInitialized += MainWindow_SourceInitialized; ;

        }

        private void MainWindow_SourceInitialized(object sender, EventArgs e)
        {
            IntPtr hwnd = new WindowInteropHelper(this).Handle;
            HwndSource.FromHwnd(hwnd).AddHook(new HwndSourceHook(WndProc));
        }

        string temp="";

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            //实现了禁止双击标题栏的消息    
            //if (msg == 0x00A3)
            //{
            //    handled = true;
            //    wParam = IntPtr.Zero;
            //}
            //if (hwnd.ToString() == "4261560")
            //{
            //    handled = true;
            //    hwnd = IntPtr.Zero;
            //}
            //hwnd = IntPtr.Zero;

            //temp += hwnd.ToString()+":";

            //const int WM_SYSCOMMAND = 0x0112;

            //const int SC_CLOSE = 0xF060;

            //if (msg == WM_SYSCOMMAND && msg == SC_CLOSE)
            //{
            //    MessageBox.Show("用户点了关闭按纽了");
            //}



            return IntPtr.Zero;
        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(2);
            dispatcherTimer.Tick += (ss, ee) =>
            {
               
            };
            dispatcherTimer.Start();

        }

        private bool result = false;

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
          //  e.Cancel = true;
            //this.Activate();
        }

        public void ActivateVoid()
        {
            this.Activate();
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisConncet_Click(object sender, RoutedEventArgs e)
        {
            Adsl.Disconnect();

        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Connect_Click(object sender, RoutedEventArgs e)
        {

            string ppoeN = ppoeName.Text.Trim();

            string ppoeUser = ppoeUserName.Text.Trim();

            string ppoePw = ppoePassWorld.Text.Trim();

            string meg = "";
            bool result = Adsl.Connect(ppoeN, ppoeUser, ppoePw, ref meg);
            if (result)
            {
                MessageBox.Show("连接成功");
            }
            else
            {
                MessageBox.Show("连接失败：" + meg);
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

     
    }
}
