using EntityframWork;
using GalaSoft.MvvmLight;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ViewModel
{
    public class MainWindowVM : ViewModelBase
    {

        private ObservableCollection<BookModel> bookModels = new ObservableCollection<BookModel>();

        public ObservableCollection<BookModel> Books
        {
            set
            {
                bookModels = value;
            }
            get
            {

                return bookModels;
            }
        }


        public MainWindowVM()
        {


        }


        public int InsertBook()
        {

            return BookService.GetInstance().InsertBook();
        }

        public void Query()
        {
            foreach (var item in BookService.GetInstance().GetList())
            {
                Books.Add(new BookModel()
                {
                    id = item.id,
                    Author = item.Author,
                    Name = item.Name,
                    Price = item.Price,
                    PublishDate = item.PublishDate
                });
            }
        }

    }
}
