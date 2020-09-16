using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public class BookModel : ViewModelBase
    {

        public int id { get; set; }

        private string name;
        public string Name
        {
            set
            {
                name = value;
                RaisePropertyChanged();
            }
            get
            {

                return name;
            }

        }


        //出版日期
        private DateTime publishDate;
        public DateTime PublishDate {

            set
            {
                publishDate = value;
                RaisePropertyChanged();
            }
            get
            {
                return publishDate;
            }

        }


        //作者
        private string author;
        public string Author {

            set
            {
                author = value;
                RaisePropertyChanged();
            }
            get
            {
                return author;
            }
        }


        //价格
        private decimal price;
        public decimal Price
        {
            set
            {
                price = value;
                RaisePropertyChanged();
            }
            get
            {
                return price;
            }
        }

    }
}
