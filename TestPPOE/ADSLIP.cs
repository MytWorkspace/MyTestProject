using DotRas;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestPPOE
{
    public class ADSLIP
    {

        #region 变量
        /// <summary>
        ///生成的临时批处理文件名称
        /// </summary>
        static String _temppath = "temp.bat";
        public static String temppath
        {
            get { return ADSLIP._temppath; }
            set { ADSLIP._temppath = value; }
        }
        /// <summary>
        /// 字符串拼接用
        /// </summary>
        private static StringBuilder sb = new StringBuilder();
        /// <summary>
        /// 拨号等待 默认15秒
        /// </summary>
        public static int delay = 15;
        #endregion

        #region 方法
        /// <summary>
        /// 开始拨号
        /// </summary>
        /// <param name="ADSL_Name">宽带连接名称</param>
        /// <param name="ADSL_UserName">宽带连接用户名</param>
        /// <param name="ADSL_PassWord">宽带连接密码</param>
        public static void ChangeIp(String ADSL_Name = "宽带连接", String ADSL_UserName = "", String ADSL_PassWord = "")
        {
            sb.Clear();
            sb.AppendLine("@echo off");
            sb.AppendLine("set adslmingzi=" + ADSL_Name);
            sb.AppendLine("set adslzhanghao=" + ADSL_UserName);
            sb.AppendLine("set adslmima=" + ADSL_PassWord);
            sb.AppendLine("@Rasdial %adslmingzi% /disconnect");
            sb.AppendLine("ping 127.0.0.1 -n 2");
            sb.AppendLine("Rasdial %adslmingzi% %adslzhanghao% %adslmima%");
            sb.AppendLine("echo 连接中");
            sb.AppendLine("ping 127.0.0.1 -n 2");
            sb.AppendLine("ipconfig");
            // sb.AppendLine("pause");

            using (StreamWriter sw = new StreamWriter(temppath, false, Encoding.Default))
            {
                sw.Write(sb.ToString());
            }
            Process.Start(temppath);
            System.Threading.Thread.Sleep(delay * 1000);
            //while (!HttpMethod.CheckIp(null))
            //{
            Process.Start(temppath);
            System.Threading.Thread.Sleep(2 * delay * 1000);
            //}
            File.Delete(temppath);
        }
        /// <summary>
        /// 获得本地ip
        /// </summary>
        /// <returns></returns>
        public static String GetIP()
        {
            ///获取本地的IP地址
            string AddressIP = string.Empty;
            //遍历
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
            }
            return AddressIP;
        }
        #endregion



    }


    /// <summary>
    /// 断开
    /// </summary>
    public static class Adsl
    {
        /// <summary>
        /// 创建或更新一个PPPOE连接(指定PPPOE名称)
        /// </summary>
        static void CreateOrUpdatePPPOE(string updatePPPOEname)
        {
            RasDialer dialer = new RasDialer();
            RasPhoneBook allUsersPhoneBook = new RasPhoneBook();
            string path = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);
            allUsersPhoneBook.Open(path);
            // 如果已经该名称的PPPOE已经存在，则更新这个PPPOE服务器地址
            if (allUsersPhoneBook.Entries.Contains(updatePPPOEname))
            {
                allUsersPhoneBook.Entries[updatePPPOEname].PhoneNumber = " ";
                // 不管当前PPPOE是否连接，服务器地址的更新总能成功，如果正在连接，则需要PPPOE重启后才能起作用
                allUsersPhoneBook.Entries[updatePPPOEname].Update();
            }
            // 创建一个新PPPOE
            else
            {
                string adds = string.Empty;
                ReadOnlyCollection<RasDevice> readOnlyCollection = RasDevice.GetDevices();
                //                foreach (var col in readOnlyCollection)
                //                {
                //                    adds += col.Name + ":" + col.DeviceType.ToString() + "|||";
                //                }
                //                _log.Info("Devices are : " + adds);
                // Find the device that will be used to dial the connection.
                RasDevice device = RasDevice.GetDevices().Where(o => o.DeviceType == RasDeviceType.PPPoE).First();
                RasEntry entry = RasEntry.CreateBroadbandEntry(updatePPPOEname, device);    //建立宽带连接Entry
                entry.PhoneNumber = " ";
                allUsersPhoneBook.Entries.Add(entry);
            }
        }

        /// <summary>
        /// 断开 宽带连接
        /// </summary>
        public static void Disconnect()
        {
            ReadOnlyCollection<RasConnection> conList = RasConnection.GetActiveConnections();
            foreach (RasConnection con in conList)
            {
                con.HangUp();
            }
        }

        /// <summary>
        /// 宽带连接，成功返回true,失败返回 false
        /// </summary>
        /// <param name="PPPOEname">宽带连接名称</param>
        /// <param name="username">宽带账号</param>
        /// <param name="password">宽带密码</param>
        /// <returns></returns>
        public static bool Connect(string PPPOEname, string username, string password, ref string msg)
        {
            try
            {
                CreateOrUpdatePPPOE(PPPOEname);
                using (RasDialer dialer = new RasDialer())
                {
                    dialer.EntryName = PPPOEname;
                    dialer.AllowUseStoredCredentials = true;
                    dialer.Timeout = 1000;
                    dialer.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);
                    dialer.Credentials = new NetworkCredential(username, password);
                    dialer.Dial();
                    return true;
                }
            }
            catch (RasException re)
            {
                msg = re.ErrorCode + " " + re.Message;
                return false;
            }
        }
    }


}
