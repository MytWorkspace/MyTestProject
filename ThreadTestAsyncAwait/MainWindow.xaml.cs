using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace ThreadTestAsyncAwait
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 使用async  await
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            double result = await Task.Run(() =>
            {
                double resultInt = 0;
                for (double i = 0; i < 500000; i++)
                {
                    resultInt += i;
                }
                Thread.Sleep(5000);
                return resultInt;
            });
            MessageBox.Show("输出结果:" + result);
        }
        /// <summary>
        /// 使用  ThreadPool
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(Subject);
        }
        /// <summary>
        /// 算法
        /// </summary>
        /// <param name="obj"></param>
        private void Subject(object obj)
        {
            double resultInt = 0;
            for (int i = 0; i < 100000; i++)
            {
                resultInt += i;
            }
            Thread.Sleep(5000);
            Console.WriteLine(resultInt);

            Dispatcher.BeginInvoke(new Action(() =>
            {
                MessageBox.Show(resultInt + "");
            })
           );
        }
        /// <summary>
        /// 使用beginInvoke
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                double resultInt = 0;
                for (int i = 0; i < 100000; i++)
                {
                    resultInt += i;
                }

                Thread.Sleep(5000);
                MessageBox.Show(resultInt + "");
            }));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }
    }
}
