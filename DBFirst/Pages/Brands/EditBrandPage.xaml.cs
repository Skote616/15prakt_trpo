using DBFirst.Models;
using DBFirst.Service;
using Microsoft.EntityFrameworkCore.Metadata;
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
    /// Логика взаимодействия для EditBrandPage.xaml
    /// </summary>
    public partial class EditBrandPage :Page
    {
        bool isNew;
        Brand brand;
        EshopAzarenkoContext db { get; set; } = DBService.Instance.Context;
        public EditBrandPage (Brand _brand)
        {
            Application.Current.MainWindow.Title = "Экран добавления/измеения брендов";
            InitializeComponent( );
            brand = _brand;
            if(brand.Name != "" && brand.Name!=null )
            {
                isNew = false;
            }
            else
                isNew=true;
                DataContext = brand;
        }

        private void back (object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack( );
        }

        private void save (object sender, RoutedEventArgs e)
        {
            if (isNew == true)
            {
                var bran = new Brand( )
                {
                    Id = db.Brands.Count( ) + 1,
                    Name = brand.Name,
                };
                db.Brands.Add(bran);
                MessageBox.Show($"{brand.Name} добавлен");
            }
            else
            {
                var bran = new Brand( )
                {
                    Id = brand.Id,
                    Name = brand.Name,
                };
                db.Brands.Update(bran);
                MessageBox.Show($"{brand.Name} изменен");
            }
                db.SaveChanges( );
            NavigationService.Navigate(new BrandsPage() );
        }
    }
}
