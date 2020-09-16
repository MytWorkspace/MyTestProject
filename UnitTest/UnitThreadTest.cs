using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTest
{

    [TestClass]
    public class UnitThreadTest
    {

        [TestMethod]
        public void TestMethod1()
        {
            BookShop book = new BookShop();
            //创建两个线程同时访问Sale方法
            Thread t1 = new Thread(new ThreadStart(book.Sale));
            Thread t2 = new Thread(new ThreadStart(book.Sale));
            //启动线程
            t1.Start();
            t2.Start();

            Console.WriteLine("43444");
        }




    }


    class BookShop
    {
        //剩余图书数量
        public int num = 1;
        public void Sale()
        {
            int tmp = num;
            if (tmp > 0)//判断是否有书，如果有就可以卖
            {
                Thread.Sleep(1000);
                num -= 1;
                Console.WriteLine("售出一本图书，还剩余{0}本", num);
            }
            else
            {
                Console.WriteLine("没有了");
            }
        }

    }
}
