using DBFirst.Models;
using DBFirst.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DBFirst.Converters
{
    public class TagsConverter :IValueConverter
    {
       
        public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
        {
            ICollection<Tag> input = (ICollection<Tag>) (value ?? 0);
            StringBuilder sb = new StringBuilder ();
           
                foreach (Tag tag in input)
                {
                    sb.Append($"#{tag.Name.ToString( )} ");
                }
            return sb.ToString();
        }

        public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException( );
        }
    }
}
