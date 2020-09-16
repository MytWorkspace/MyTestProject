using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel
{
    public class Book
    {

        public int id { get; set; }

        public string Name { get; set; }//书名

        public DateTime PublishDate { get; set; }//出版日期

        public string Author { get; set; }//作者

        public Decimal Price { get; set; }//价格


    }
}
