using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEShop.Core.Models
{
    public class ProductCategory
    {
        public string id { get; set; }
        public string category { get; set; }

        public ProductCategory()
        {
            this.id = Guid.NewGuid().ToString();
        }
    }
}
