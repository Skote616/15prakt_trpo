using DBFirst.Models;
using DBFirst.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DBFirst.Converters
{
    class CategoryConverter :IValueConverter
    {
        public EshopAzarenkoContext db = DBService.Instance.Context;

        ObservableCollection<Category> categories = new ObservableCollection<Category>( );


        public void LoadList ()
        {
            categories.Clear( );
            foreach (var form in db.Categories.ToList( ))
                categories.Add(form);
        }
        public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
        {
            LoadList( );

            int input = (int) (value ?? 0);

            return categories [ (int)input-1 ].Name;
        }

        public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException( );
        }
    }
}
