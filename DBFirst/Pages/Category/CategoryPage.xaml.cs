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
    /// Логика взаимодействия для CategoryPage.xaml
    /// </summary>
    public partial class CategoryPage :Page
    {
        public ObservableCollection<Category> CategoryList { get; set; }

        EshopAzarenkoContext db { get; set; } = DBService.Instance.Context;

        public CategoryPage ()
        {
            Application.Current.MainWindow.Title = "Экран категорий";
            CategoryList = new ObservableCollection<Category>( );

            foreach (var p in db.Categories)
            {
                CategoryList.Add(p);
            }

            InitializeComponent( );
            list.ItemsSource = CategoryList;
        }

        private void EditCard (object sender, MouseButtonEventArgs e)
        {

            Category category = (Category) list.SelectedItem;
            NavigationService.Navigate(new EditCategoryPage(category));
        }

        private void add (object sender, RoutedEventArgs e)
        {
            Category category = new Category( );
            NavigationService.Navigate(new EditCategoryPage(category));
        }

        private void remove (object sender, RoutedEventArgs e)
        {
            if (list.SelectedItem != null)
            {
                var result = MessageBox.Show("Удалить товар?", "Подтверждение", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    var selectedBrand = (Category) list.SelectedItem;

                    db.Categories.Remove(selectedBrand);
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
