using DBFirst.Models;
using DBFirst.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для TagsPage.xaml
    /// </summary>
    public partial class TagsPage : Page
    {
        public ObservableCollection<Tag> CategoryList { get; set; }

        EshopAzarenkoContext db { get; set; } = DBService.Instance.Context;

        public TagsPage ()
        {
            Application.Current.MainWindow.Title = "Экран тегов";
            CategoryList = new ObservableCollection<Tag>( );

            foreach (var p in db.Tags)
            {
                CategoryList.Add(p);
            }

            InitializeComponent( );
            list.ItemsSource = CategoryList;
        }

        private void EditCard (object sender, MouseButtonEventArgs e)
        {

            Tag category = (Tag) list.SelectedItem;
            NavigationService.Navigate(new EditTagPage(category));
        }

        private void add (object sender, RoutedEventArgs e)
        {
            Tag category = new Tag( );
            NavigationService.Navigate(new EditTagPage(category));
        }

        private void remove (object sender, RoutedEventArgs e)
        {
            if (list.SelectedItem != null)
            {
                var result = MessageBox.Show("Удалить товар?", "Подтверждение", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    var selectedBrand = (Tag) list.SelectedItem;

                    db.Tags.Remove(selectedBrand);
                    db.SaveChanges( );

                    CategoryList.Remove(selectedBrand);
                    MessageBox.Show("Объект удален");
                }
            }
            else
            {
                MessageBox.Show("Объект не выбран");
            }
        }

        private void back (object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage(1));
        }
    }
}
