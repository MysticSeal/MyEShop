using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyEShop.Core.Models;

namespace MyEShop.DataAccess.InMemory
{
    
    public class ProductCategoryRepository
    {
        ObjectCache cache2 = MemoryCache.Default;
        List<ProductCategory> productsCategories = new List<ProductCategory>();

        public ProductCategoryRepository()
        {
            productsCategories = cache2["productsCategories"] as List<ProductCategory>; 
            if(productsCategories == null)
            {
                productsCategories = new List<ProductCategory>();
            }
           
        }

        public void Commit()
        {
            cache2["productsCategories"] = productsCategories;
        }

        public void Insert(ProductCategory product)
        {
            productsCategories.Add(product);
        }

        public void Update(ProductCategory product)
        {
            ProductCategory productCategory = productsCategories.Find(p => p.id == product.id);
            if(productCategory == null)
            {
                productCategory = product;
            }
            else
            {
                throw new Exception("Product Category Not Found");
            }
            
        }

        public ProductCategory Find(string id)
        {
            ProductCategory productCategory = productsCategories.Find(p => p.id == id);
            if(productCategory == null)
            {
                throw new Exception("Product Category Not Found");
            }
            else
            {
                return productCategory;
            }

        }

        public IQueryable<ProductCategory> Collection()
        {
            return productsCategories.AsQueryable();
        }

        public void Delete(string id)
        {
            ProductCategory product = productsCategories.Find(p => p.id == id);
            if(product == null)
            {
                throw new Exception("ProductCategory Was Not Found");
            }
            else
            {
                productsCategories.Remove(product);
            }

        }
    }
}
