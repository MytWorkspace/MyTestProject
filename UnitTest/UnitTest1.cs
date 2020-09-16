using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            double result = DateTime.Now.Subtract(DateTime.Parse("2019-7-6 9:30:20")).TotalDays;

        }

        [TestMethod]
        public void TestSubString()
        {
            string temp = " abc     bb  c ";

            //string temp2 = temp.TrimStart().TrimEnd();

            //string result = GetString(temp);

            //Char[] chartemp = result.ToCharArray();

            string tempd = GetStr(temp);
        }


        public string GetStr(string temp)
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
        /// 去除所有空格
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public string GetString(string temp)
        {
            string result = "";
            CharEnumerator CEnumerator = temp.GetEnumerator();

            while (CEnumerator.MoveNext())
            {
                byte[] array = new byte[1];
                array = System.Text.Encoding.ASCII.GetBytes(CEnumerator.Current.ToString());
                int asciicode = (short)(array[0]);
                if (asciicode != 32)
                {
                    result += CEnumerator.Current.ToString();
                }

            }
            return result;
        }

        [TestMethod]
        public void GetRandomByPerson()
        {

            List<int> list = new List<int>() { 90, 5, 2 };

            var tempresult = list.Take(2);

            for (int i = 0; i < 100; i++)
            {
                int result = GetPersonValueByp(list);

                Console.WriteLine("选中了 " + result);
                Thread.Sleep(10);
            }

        }

        private static object objj = new object();
        //根据百分比 获取随机数
        public static int GetPersonValue(List<int> list)
        {
            lock (objj)
            {
                Random random = new Random();
                int persont = random.Next(0, 100);
                int count = list.Count;
                int result = -1;
                int i = 0;
                try
                {
                    //list 从小到打排序
                    for (i = 0; i < list.Count; i++)
                    {
                        if (persont < list[i])
                        {
                            result = list[i];
                            break;
                        }

                    }
                }
                catch (Exception ee)
                {

                    throw;
                }
                return result;
            }

        }

        /// <summary>
        /// 根据百分比 抽取  可以使用
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
                int count2 = list.Take(i+1).Sum();
                if (persont <= count2) {

                    result = list[i];
                    break;
                }
            }
            return result;
        }


        [TestMethod]
        public void TestRandom() {

            Random random = new Random();
            int index= random.Next(0,0);
        
        }


    }
}
