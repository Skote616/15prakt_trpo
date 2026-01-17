using Azure;
using DBFirst.Models;
using DBFirst.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        Product product = new Product();
        public ICollectionView formsView { get; set; }
        public string searchQuery { get; set; } = null!;
        public string filterCategory { get; set; } = null!;
        public string filterBrand { get; set; } = null!;
        public string filterHeightFrom { get; set; } = null!;
        public string filterHeightTo { get; set; } = null!;

        public bool isAdmin;


        public EshopAzarenkoContext db = DBService.Instance.Context;
       public ObservableCollection<Product> products { get; set; } = new ObservableCollection<Product>();

        public MainPage(int role_id)
        {
            Application.Current.MainWindow.Title = "Основной экран";
            isAdmin = Convert.ToBoolean(role_id);
            LoadList();
            
            var products = db.Products
                .Include(p => p.Tags)
                .Include(p => p.Category)
                .ToList();
            formsView = CollectionViewSource.GetDefaultView(products);
            //allPr.DataContext = products.Count( );
            formsView.Filter = FilterForms;
            InitializeComponent();
            allPr.Text = $"Всего записей: { db.Products.Count( ).ToString( )}";
            productList.DataContext = product;
            productList.ItemsSource = products;
            var stock = this.FindName("stockBox") as TextBox;
            /*if (int.Parse(stock.Text) < 10)
            {
                stock.BorderBrush = new SolidColorBrush((Color) ColorConverter.ConvertFromString("#ffd129"));
                stock.BorderThickness = new Thickness(2);
            }*/
        }

        public void LoadList()
        {
            products.Clear();
            foreach (var form in db.Products.ToList())
                products.Add(form);
            
        }

        public bool FilterForms(object obj)
        {
            if (obj is not Product)
                return false;
            var form = (Product) obj;
            if (searchQuery != null && !form.Name.Contains(searchQuery,
            StringComparison.CurrentCultureIgnoreCase))
                return false;

            if (!filterHeightFrom.IsNullOrEmpty() && Convert.ToInt32(filterHeightFrom) > form.Price)
                return false;
            if (!filterHeightTo.IsNullOrEmpty() && Convert.ToInt32(filterHeightTo) < form.Price)
                return false;

            if (!filterBrand.IsNullOrEmpty() && !form.Name.Contains(filterBrand, StringComparison.CurrentCultureIgnoreCase))
                return false;

            if (!filterCategory.IsNullOrEmpty() && !form.Name.Contains(filterCategory, StringComparison.CurrentCultureIgnoreCase))
                return false;
            return true;

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            formsView.Refresh();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (combo.SelectedItem == null)
                return;
            formsView.SortDescriptions.Clear();
            var cb = (ComboBox) sender;
            var selected = (ComboBoxItem) cb.SelectedItem;
            switch (selected.Tag)
            {
                case "Name":
                    formsView.SortDescriptions.Add(new SortDescription("Name",
                    ListSortDirection.Ascending));
                    break;
                case "PriceUp":
                    formsView.SortDescriptions.Add(new SortDescription("Price",
                    ListSortDirection.Ascending));
                    break;
                case "PriceDown":
                    formsView.SortDescriptions.Add(new SortDescription("Price",
                    ListSortDirection.Descending));
                    break;
                case "StockUp":
                    formsView.SortDescriptions.Add(new SortDescription("Stock",
                    ListSortDirection.Ascending));
                    break;
                case "StockDown":
                    formsView.SortDescriptions.Add(new SortDescription("Stock",
                    ListSortDirection.Descending));
                    break;
                default:
                    break;
            }
            formsView.Refresh();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            formsView.SortDescriptions.Clear();
            combo.SelectedItem = null;
            f1.Text = "";
            f2.Text = "";
            f3.Text = "";
            f2.Text = "";

            
            LoadList();
        }

        private void EditCard(object sender, MouseButtonEventArgs e)
        {
            if (isAdmin)
            {
                if (productList.SelectedItem != null)
                {
                    NavigationService.Navigate(new EditPage((Product) productList.SelectedItem));
                }
                else
                { MessageBox.Show("Объект не выбран"); }
            }
            else { MessageBox.Show("У вас недостаточно прав"); }
        }

        private void AddProduct(object sender, RoutedEventArgs e)
        {
            if (isAdmin)
            {
                Product pr = new Product();
                NavigationService.Navigate(new EditPage(pr));
            }
            else
            { MessageBox.Show("У вас недостаточно прав"); }
        }

        private void ChangeCategory(object sender, RoutedEventArgs e)
        {
            if (isAdmin)
            {
                NavigationService.Navigate(new CategoryPage());
            }
            else
            { MessageBox.Show("У вас недостаточно прав"); }
        }

        private void ChangeTags(object sender, RoutedEventArgs e)
        {
            if (isAdmin)
            {
                NavigationService.Navigate(new TagsPage());
            }
            else
            { MessageBox.Show("У вас недостаточно прав"); }
        }
        private void ChangeBrands(object sender, RoutedEventArgs e)
        {
            if (isAdmin)
            {
                NavigationService.Navigate(new BrandsPage());
            }
            else
            { MessageBox.Show("У вас недостаточно прав"); }
        }
    }
}
