using DBFirst.Models;
using DBFirst.Service;
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
    /// Логика взаимодействия для EditCategoryPage.xaml
    /// </summary>
    public partial class EditCategoryPage :Page
    {
        bool isNew;
        Category category;
        EshopAzarenkoContext db { get; set; } = DBService.Instance.Context;
        public EditCategoryPage (Category _category)
        {
            Application.Current.MainWindow.Title = "Экран добавления/измеения категорий";
            InitializeComponent( );
            category = _category;
            if (category.Name != "" && category.Name != null)
            {
                isNew = false;
            }
            else
                isNew = true;
            DataContext = category;
        }

        private void back (object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack( );
        }

        private void save (object sender, RoutedEventArgs e)
        {
            if (isNew == true)
            {
                var categ = new Category( )
                {
                    Id = db.Categories.Count( ) + 1,
                    Name = category.Name,
                };
                db.Categories.Add(categ);
                MessageBox.Show($"{category.Name} добавлен");
            }
            else
            {
                var categ = new Category( )
                {
                    Id = category.Id,
                    Name = category.Name,
                };
                db.Categories.Update(categ);
                MessageBox.Show($"{category.Name} изменен");
            }
            db.SaveChanges( );
            NavigationService.Navigate(new CategoryPage() );
        }
    }
}
