using DBFirst.Models;
using DBFirst.Service;
using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для EditPage.xaml
    /// </summary>
    public partial class EditPage : Page
    {
        Product prod=new Product();
        List<Tag> tagsName=new List<Tag>();
        List<Tag> SelectedTags = new( );
        List<Product>products = new List<Product>();
        bool isNew;
        EshopAzarenkoContext context { get; set; } = DBService.Instance.Context;

        public EditPage (Product product)
        {
            if(product.Name==null || product.Name == "")
                isNew = true;
            else
                isNew= false;

            DataContext = product;
            prod = product;
            Application.Current.MainWindow.Title = "Экран изменения продукта";

            LoadList();
            InitializeComponent( );
            var categories = context.Categories.ToList( );
            var brands = context.Brands.ToList( );
            var tags = context.Tags.ToList( );
            foreach (var category in categories)
            {
                CategoryCombo.Items.Add(category.Name);
            }
            foreach (var brand in brands)
            {
                BrandCombo.Items.Add(brand.Name);
            }
            foreach (Tag tag in tags)
            {
                tagsName.Add(tag);
            }
            TagsListBox.ItemsSource = tagsName;
            if (isNew == false)
            {
                CategoryCombo.SelectedIndex = prod.CategoryId - 1;
                BrandCombo.SelectedIndex = prod.BrandId - 1;
                foreach (Tag tag in prod.Tags)
                {
                    TagsListBox.SelectedItems.Add(tag);
                    SelectedTags.Add(tag);
                }
                
            }
        }

        private void save(object sender, RoutedEventArgs e)
        {
            var categories = context.Categories.ToList( );
            var brands = context.Brands.ToList( );
           
            if (isNew == true)
            {
                var product = new Product( )
                {
                    Name = prod.Name,
                    Description = prod.Description,
                    Price = Math.Round(prod.Price,2),
                    Category = categories [ CategoryCombo.SelectedIndex ],
                    Brand = brands [ BrandCombo.SelectedIndex ],
                    Stock = prod.Stock,
                    Tags = SelectedTags.ToList( ),
                    Rating = Math.Round(prod.Rating,1),
                    CreatedAt = DateOnly.FromDateTime(DateTime.Today),
                    CategoryId = CategoryCombo.SelectedIndex + 1,
                    BrandId = BrandCombo.SelectedIndex + 1,
                };
                context.Products.Add(product);
                MessageBox.Show($"{prod.Name} добавлен");
            }
            else 
            {
                /*var product = new Product( )
                {
                    Name = prod.Name,
                    Description = prod.Description,
                    Price = prod.Price,
                    Category = categories [ CategoryCombo.SelectedIndex ],
                    Brand = brands [ BrandCombo.SelectedIndex ],
                    Stock = prod.Stock,
                    Tags = SelectedTags.ToList( ),
                    Rating = Math.Round(prod.Rating,1),
                    CreatedAt = DateOnly.FromDateTime(DateTime.Today),
                    CategoryId = CategoryCombo.SelectedIndex + 1,
                    BrandId = BrandCombo.SelectedIndex + 1,
                };*/

                var cat = context.Categories.ToList( );
                var br = context.Brands.ToList( );
                prod.Brand = br [BrandCombo.SelectedIndex];
                prod.Category = cat [ CategoryCombo.SelectedIndex ];
                prod.Tags = SelectedTags.ToList( );
                foreach (var tag in prod.Tags)
                {
                    if(SelectedTags.Contains(tag))
                        SelectedTags.Remove(tag);
                }
                prod.Rating = Math.Round(prod.Rating, 1);
                context.Products.Update(prod);
                MessageBox.Show($"{prod.Name} изменен");
                
            }
            context.SaveChanges( );
            NavigationService.Navigate(new MainPage(1));
        }

        public void LoadList ()
        {
            products.Clear( );
            foreach (var form in context.Products.ToList( ))
                products.Add(form);
        }


        private void remove(object sender, RoutedEventArgs e)
        {
            if (!isNew)
            {
                var result = MessageBox.Show("Удалить товар?", "Подтверждение", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    context.Products.Remove(prod);
                    context.SaveChanges( );
                    NavigationService.Navigate(new MainPage(1));
                }
            }
            else
            {
                MessageBox.Show("Ошибка");
            }

        }

        private void back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage(1));
        }

        private void TagsListBox_SelectionChanged (object sender, SelectionChangedEventArgs e)
        {
            
                SelectedTags.Clear( );
                foreach (Tag tag in TagsListBox.SelectedItems)
                    SelectedTags.Add(tag);
            
        }
    }
}
