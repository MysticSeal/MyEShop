﻿using MyEShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEShop.Core.ViewModels
{
    public class ProductManagerViewModel
    {
        public Product product { get; set; }
        public IEnumerable<ProductCategory> productCategories { get; set; }

    }
}
