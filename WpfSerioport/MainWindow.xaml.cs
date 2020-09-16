using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace WpfSerioport
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
    }

    public class SerialPortComImplement
    {
        public delegate void RecEventHandler(byte[] queueByte);
        public event RecEventHandler DataReceivedEvent;
        private SerialPort serialPort;
        private List<byte> buffer = new List<byte>(4096);
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="portName">端口名称</param>
        /// <param name="baudRate">波特率</param>
        /// <param name="dataBits">数据位</param>
        public SerialPortComImplement(string portName, int baudRate, int dataBits)
        {
            serialPort = new SerialPort(portName, baudRate, Parity.None);
            serialPort.DataBits = dataBits;
            serialPort.StopBits = StopBits.One;
            serialPort.ReadTimeout = 2000;
            serialPort.WriteBufferSize = 1024;
            serialPort.ReadBufferSize = 1024;
            serialPort.RtsEnable = true;
            serialPort.DtrEnable = true;
            serialPort.ReceivedBytesThreshold = 1;
            serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceivedEventHandler);

        }
        /// <summary>
        /// 串口数据接收事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void serialPort_DataReceivedEventHandler(object sender, SerialDataReceivedEventArgs e)
        {
            await Task.Run(() =>
            {
                try
                {
                    byte[] readBuffer = null;
                    int n = serialPort.BytesToRead;
                    byte[] buf = new byte[n];
                    serialPort.Read(buf, 0, n);
                    //1.缓存数据           
                    buffer.AddRange(buf);
                    //2.完整性判断         
                    while (buffer.Count >= 7)
                    {
                        //至少包含标头(1字节),长度(1字节),校验位(2字节)等等
                        //2.1 查找数据标记头            
                        if (buffer[0] == 0x00) //传输数据有帧头，用于判断       
                        {
                            int len = buffer[1];
                            if (buffer.Count < len + 2)
                            {
                                //数据未接收完整跳出循环
                                break;
                            }
                            readBuffer = new byte[len + 2];
                            //得到完整的数据，复制到readBuffer中    
                            buffer.CopyTo(0, readBuffer, 0, len + 2);
                            //从缓冲区中清除
                            buffer.RemoveRange(0, len + 2);

                            //触发外部处理接收消息事件
                        }
                        else //开始标记或版本号不正确时清除           
                        {
                            buffer.RemoveAt(0);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // SerialPortLog.Error(ex, "");
                }

            });

        }

        /// <summary>
        /// 打开端口
        /// </summary>
        public bool Open()
        {
            try
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.Open();
                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                //SerialPortLog.Error(ex, "");
                return false;
            }

        }
        /// <summary>
        /// 发送字节
        /// </summary>
        /// <param name="writeBytes">要发送的字节</param>
        /// <returns></returns>
        public bool Write(byte[] writeBytes)
        {
            if (Open())
            {
                try
                {
                    serialPort.Write(writeBytes, 0, writeBytes.Length);
                    string mergeStr = "发送:";
                    for (int j = 0; j < writeBytes.Length; j++)
                    {
                        mergeStr = mergeStr + writeBytes[j].ToString("x") + " ";
                    }
                    //  SerialPortLog.Info(mergeStr);
                    return true;
                }
                catch (Exception ex)
                {
                    //SerialPortLog.Error(ex, "");
                    return false;
                }
            }
            return false;
        }
        /// <summary>
        /// 发送字符串
        /// </summary>
        /// <param name="writestrs"></param>
        /// <returns></returns>
        public bool Write(string writeStrs)
        {
            if (Open())
            {
                try
                {
                    serialPort.Write(writeStrs);
                    Thread.Sleep(100);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="NumBytes">读取的字节数</param>
        /// <returns></returns>
        public byte[] Read(int NumBytes)
        {
            byte[] inbuffer = null;

            if (serialPort.IsOpen && serialPort.BytesToRead > 0)
            {
                if (NumBytes > serialPort.BytesToRead)
                {
                    NumBytes = serialPort.BytesToRead;
                }
                try
                {
                    inbuffer = new byte[NumBytes];
                    int count = serialPort.Read(inbuffer, 0, NumBytes);
                }
                catch (TimeoutException timeoutEx)
                {
                    //超时异常
                    //  SerialPortLog.Error(timeoutEx, "");
                }
            }
            return inbuffer;
        }
        public byte[] Read()
        {
            return Read(serialPort.BytesToRead);
        }
        public string ReadLine()
        {
            try
            {
                if (serialPort.IsOpen && serialPort.BytesToRead > 0)
                {
                    string s = serialPort.ReadExisting();
                    return serialPort.ReadLine();
                }
                return null;
            }
            catch (TimeoutException timeoutEx)
            {
                // SerialPortLog.Error(timeoutEx, "");
                return timeoutEx.Message;
            }
        }
        /// <summary>
        /// 关闭串口
        /// </summary>
        public void Close()
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                }
            }
            catch (Exception ex)
            {
                // SerialPortLog.Error(ex, "");
            }
        }

        public bool IsOpen
        {
            get
            {
                return serialPort.IsOpen;
            }
        }

    }
}
