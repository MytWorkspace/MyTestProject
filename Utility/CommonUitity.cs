using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utility
{
    public class CommonUitity
    {
        /// <summary>
        /// 去除前后 空格，去除中间空格 只保留一个空格
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static string GetStr(string temp)
        {
            temp = temp.TrimStart().TrimEnd();

            List<char> listChar = new List<char>();
            int index = 0;
            CharEnumerator CEnumerator = temp.GetEnumerator();

            while (CEnumerator.MoveNext())
            {
                byte[] array = new byte[1];
                array = System.Text.Encoding.ASCII.GetBytes(CEnumerator.Current.ToString());
                int asciicode = (short)(array[0]);
                listChar.Add(CEnumerator.Current);
                if (index > 0)
                {
                    if (asciicode == 32 && listChar[index] == listChar[index - 1])
                    {
                        listChar.RemoveAt(index);
                        index = index - 1;
                    }
                }
                index++;
            }

            return new string(listChar.ToArray());
        }

        /// <summary>
        /// 根据百分比 抽取
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int GetPersonValueByp(List<int> list)
        {
            Random random = new Random();
            int persont = random.Next(0, 100);
            int count = list.Count;
            int result = -1;

            for (int i = 0; i < list.Count; i++)
            {
                int count2 = list.Take(i + 1).Sum();
                if (persont <= count2)
                {
                    result = list[i];
                    break;
                }
            }
            return result;
        }


        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <returns></returns>
        public static string ReadConfig(string addKey)
        {
            string fileName = System.IO.Path.GetFileName(Application.ExecutablePath);
            Configuration config = ConfigurationManager.OpenExeConfiguration(fileName);
            return config.AppSettings.Settings[addKey].Value;
        }

        /// <summary>
        /// 修改 配置文件
        /// </summary>
        /// <param name="addKey"></param>
        /// <param name="value"></param>
        public static void SetConfig(string addKey, string value)
        {

            string fileName = System.IO.Path.GetFileName(Application.ExecutablePath);
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(fileName);
            config.AppSettings.Settings[addKey].Value = value;

            config.Save();
        }

        /// <summary>
        /// 判断是否有此文件夹
        /// </summary>
        /// <param name="forlderName">文件夹名称</param>
        /// <returns></returns>
        public static bool ExisFileForlder(string forlderName)
        {
            DirectoryInfo directory = new DirectoryInfo(forlderName);
            if (!directory.Exists)
            {
                return false;
            }
            return true;
        }



        /// <summary>
        /// JSON Serialization
        /// </summary>
        public static string JsonSerializer<T>(T t)
        {
            string jsonString = "";
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream();
                ser.WriteObject(ms, t);
                jsonString = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
            }
            catch (Exception e)
            {
                //  Logger.Error("JsonSerializer类型转换失败" + e.StackTrace);
            }

            return jsonString;
        }

        /// <summary>
        /// JSON Deserialization
        /// </summary>
        public static T JsonDeserialize<T>(string jsonString)
        {
            T obj;
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
                obj = (T)ser.ReadObject(ms);
            }
            catch (Exception e)
            {
                // Logger.Error("JsonDeserialize类型转换失败" + jsonString + e.StackTrace);
                return default(T);
            }
            return obj;
        }

        /// <summary>
        /// JSON反序列化：将JSON字符串解析成预定的数据类型  一般不能用
        /// </summary>
        /// <typeparam name="T">泛型：需要反序列化返回的数据类型</typeparam>
        /// <param name="jsonString">输入需要解析的字符内容</param>
        /// <returns>返回指定的数据类型</returns>
        private static T DeserializeObject<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

    }
}
