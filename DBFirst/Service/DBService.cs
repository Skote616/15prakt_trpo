using DBFirst.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DBFirst.Service
{
    public class DBService
    {
        private EshopAzarenkoContext context;
        public EshopAzarenkoContext Context => context;
        public ObservableCollection<Product> products { get; set; } = new();
        private static DBService? instance;
        public int Commit() => Context.SaveChanges();

        public static DBService Instance
        {
            get
            {
                if (instance == null)
                    instance = new DBService( );
                return instance;
            }
        }
        public DBService ()
        {
            context = new EshopAzarenkoContext( );
        }

        public void Remove(Product group)
        {
            context.Remove<Product>(group);
            if (Commit() > 0)
                if (products.Contains(group))
                    products.Remove(group);
        }

    }
}
