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
    /// Логика взаимодействия для Brands.xaml
    /// </summary>
    public partial class BrandsPage : Page
    {
        public ObservableCollection<Brand> BrandsList { get; set; }
        EshopAzarenkoContext db { get; set; } = DBService.Instance.Context;
        public BrandsPage ()
        {
            Application.Current.MainWindow.Title = "Экран брендов";
            BrandsList = new ObservableCollection<Brand>( );

            foreach (var p in db.Brands)
            {
                BrandsList.Add(p);
            }
            
            InitializeComponent( );
            list.ItemsSource = BrandsList;
        }

        private void EditCard (object sender, MouseButtonEventArgs e)
        {

            Brand brand = (Brand) list.SelectedItem;
            NavigationService.Navigate(new EditBrandPage(brand));
        }

        private void add (object sender, RoutedEventArgs e)
        {
            Brand brand=new Brand();
            NavigationService.Navigate(new EditBrandPage(brand));
        }

        private void remove (object sender, RoutedEventArgs e)
        {
            //Удаляет только объект без связи
            if (list.SelectedItem != null)
            {
                var result = MessageBox.Show("Удалить товар?", "Подтверждение", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    var selectedBrand = (Brand) list.SelectedItem;

                    // При каскадном удалении Entity Framework автоматически удалит связанные товары
                    db.Brands.Remove(selectedBrand);
                    db.SaveChanges( );

                    // Обновляем UI
                    BrandsList.Remove(selectedBrand);
                    MessageBox.Show("Бренд и все связанные товары успешно удалены");
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
