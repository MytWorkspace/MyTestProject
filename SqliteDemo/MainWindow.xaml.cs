using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using ViewModel.ViewModel;

namespace SqliteDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowVM vm;
        public MainWindow()
        {
            InitializeComponent();

            DataContext = vm = new MainWindowVM();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vm.InsertBook();
            vm.Query();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            //多线程测试



        }
    }
}
