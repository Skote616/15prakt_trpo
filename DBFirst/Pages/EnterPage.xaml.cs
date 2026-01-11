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

namespace DBFirst.Pages
{
    /// <summary>
    /// Логика взаимодействия для Enter.xaml
    /// </summary>
    public partial class EnterPage :Page
    {
        int role_id;
        public EnterPage ()
        {
            InitializeComponent( );
            Application.Current.MainWindow.Title = "Экран входа";
        }

        private void EnterManager (object sender, RoutedEventArgs e)
        {
            if (PinLine.Password == "1234")
            {
                MessageBox.Show("Вход выполнен");
                role_id = 1;
                NavigationService.Navigate(new MainPage(role_id));
            }
            else
                MessageBox.Show("Пин-код неверный");
        }

        private void EnterVisitor (object sender, RoutedEventArgs e)
        {
            role_id = 0;
            NavigationService.Navigate(new MainPage(0));
        }
    }
}
