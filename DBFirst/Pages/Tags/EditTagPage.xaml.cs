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
    /// Логика взаимодействия для EditTagPage.xaml
    /// </summary>
    public partial class EditTagPage :Page
    {
        bool isNew;
        Tag tag;
        EshopAzarenkoContext db { get; set; } = DBService.Instance.Context;
        public EditTagPage (Tag _tag)
        {
            Application.Current.MainWindow.Title = "Экран добавления/изменения тегов";
            InitializeComponent( );
            tag = _tag;
            if (tag.Name != "" && tag.Name != null)
            {
                isNew = false;
            }
            else
                isNew = true;
            DataContext = tag;
        }

        private void back (object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TagsPage() );
        }

        private void save (object sender, RoutedEventArgs e)
        {
            if (isNew == true)
            {
                var categ = new Tag( )
                {
                    Id = db.Tags.Count( ) + 1,
                    Name = tag.Name,
                };
                db.Tags.Add(categ);
                MessageBox.Show($"{tag.Name} добавлен");
            }
            else
            {
                var categ = new Tag( )
                {
                    Id = tag.Id,
                    Name = tag.Name,
                };
                db.Tags.Update(categ);
                MessageBox.Show($"{tag.Name} изменен");
            }
            db.SaveChanges( );
            NavigationService.GoBack( );
        }
    }
}
