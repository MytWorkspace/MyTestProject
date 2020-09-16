using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
