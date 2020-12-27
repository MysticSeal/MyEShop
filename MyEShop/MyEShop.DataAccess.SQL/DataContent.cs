using MyEShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEShop.DataAccess.SQL
{
    public class DataContent : DbContext
    {
        public DataContent() : base("DefaultConnection")
        {

        }

        public DbSet<Product> products { get; set; }

        public DbSet<ProductCategory> productsCategories { get; set; }
    }
}
