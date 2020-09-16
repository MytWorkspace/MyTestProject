using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

namespace OZIPROpen
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

        Process process = new Process();
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            process.StartInfo.FileName = @"E:\WeChat\jetrenzhe\WeChat Files\wxid_2403054029312\FileStorage\File\2020-08\OZIPR_DOS_步骤-0818\OZIPR_DOS_步骤\DOSBoxPortable\DOSBoxPortable.exe";

            process.StartInfo.Arguments = "-c \"mount c d://ozipr//ozipr\" -c \"c: \" -c \"dir \"  -c \"OZIPR.EXE TJ.INP\" ";

            process.Start();

        }

        IntPtr MianHandle = new IntPtr();

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //获取到这个应用程序的进程 给这个进程发送数据
            Process[] pro = Process.GetProcesses();

            foreach (var item in pro)
            {
                //if (item.ProcessName.Contains("dosbox") )
                if (item.ProcessName.Contains("cmd"))
                {
                    //MianHandle = item.MainWindowHandle;
                    MianHandle = item.MainWindowHandle;
                    break;
                }
            }


            //str = "1";
            //byte[] bb = Encoding.Default.GetBytes
            //    (str);
            //fixed (byte* p = bb)
            //{
            //    SendMessage(hWnd4, WM_SETTEXT, 0, (int)p);
            //}


            if (MianHandle != IntPtr.Zero)
            {
                //string sendString = "aaa";
                //byte[] sarr = System.Text.Encoding.Default.GetBytes(sendString);
                //int len = sarr.Length;
                //COPYDATASTRUCT cds;
                //cds.dwData = (IntPtr)0;
                //cds.cbData = len + 1;
                //cds.lpData = sendString;
                //SendMessage(MianHandle, WM_COPYDATA, 0, ref cds);


                //  int result=  SendMessage(MianHandle, WM_SYSKEYDOWN, 0X0D, 0); //输入ENTER（0x0d）

                //IntPtr hWnd4 = FindWindow(null, "值班员登录");

                unsafe
                {
                    string str = "cd ";
                    byte[] bb = Encoding.Default.GetBytes(str);
                    fixed (byte* p = bb)
                    {
                        SendMessage(MianHandle, WM_COPYDATA, 0, (int)p);
                    }
                }

            }





        }



        //声明 API 函数
        //[DllImport("User32.dll", EntryPoint = "SendMessage")]
        //private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, ref COPYDATASTRUCT lParam);


        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);



        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        //定义消息常数
        public const int CUSTOM_MESSAGE = 0X400 + 2;//自定义消息

        const int WM_COPYDATA = 0x004A;//必须是这个数值，不能更改


        //SendMessage参数
        private const int WM_KEYDOWN = 0X100;
        private const int WM_KEYUP = 0X101;
        private const int WM_SYSCHAR = 0X106;
        private const int WM_SYSKEYUP = 0X105;
        private const int WM_SYSKEYDOWN = 0X104;
        private const int WM_CHAR = 0X102;




        //[DllImport("User32.dll", EntryPoint = "FindWindow")]
        //public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern int GetWindow(int hwnd, int wCmd);
        public const int GW_CHILD = 5;
        public const int WM_SETTEXT = 0x0C;


    }

    /// <summary>
    /// 定义结构体
    /// </summary>
    public struct COPYDATASTRUCT
    {
        public IntPtr dwData; //可以是任意值
        public int cbData;    //指定lpData内存区域的字节数
        [MarshalAs(UnmanagedType.LPStr)]
        public string lpData; //发送给目录窗口所在进程的数据
    }

}
