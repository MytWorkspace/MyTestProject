using DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityframWork
{
    public class BookService
    {

        private BookService() { }

        private static BookService instance;

        private static object locker = new object();

        public static BookService GetInstance()
        {
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new BookService();
                    }
                }
            }
            return instance;
        }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <returns></returns>
        public int InsertBook()
        {

            List<Book> books = new List<Book>()
            {
                new Book() { Name = "射雕英雄传", PublishDate = new DateTime(1960, 1, 1), Author = "金庸", Price = 10.5M },
                new Book() { Name = "神雕侠侣", PublishDate = new DateTime(1960, 2, 2), Author = "金庸", Price = 12.5M },
                new Book() { Name = "倚天屠龙记", PublishDate = new DateTime(1960, 3, 3), Author = "金庸", Price = 16.5M },
                new Book() { Name = "小李飞刀", PublishDate = new DateTime(1965, 5, 5), Author = "古龙", Price = 13.5M },
                new Book() { Name = "绝代双骄", PublishDate = new DateTime(1965, 6, 6), Author = "古龙", Price = 15.5M },
            };

            using (var db = new DBEntity())
            {
                db.Books.AddRange(books);
                int count = db.SaveChanges();
                if (count > 0)
                    return count;
                else
                    return -1;
            }

        }


        public List<Book> GetList()
        {

            using (var db = new DBEntity())
            {

                var temp= db.Books.ToList();
                return temp;
            }
        }


    }
}
