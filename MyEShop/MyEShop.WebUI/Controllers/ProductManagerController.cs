using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyEShop.Core.Models;
using MyEShop.DataAccess.InMemory; 

namespace MyEShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;

        public ProductManagerController()
        {
            context = new ProductRepository(); 
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList(); 
            return View(products);
        }

        public ActionResult CreateProduct()
        {
            Product product = new Product();
            return View(product); 
        }

        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);

            }
            else
            {
                context.Insert(product);
                context.Commit();

                return RedirectToAction("Index"); 
            }
        }

        public ActionResult Edit(string id)
        {
            Product product = context.Find(id);
            if(product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product, string id)
        {
            Product editProduct = context.Find(id);

            if (editProduct == null)
            {
                return HttpNotFound();
            }
            else
            {
               if (!ModelState.IsValid)
                {
                    return View(product);
                }

                editProduct.Category = product.Category;
                editProduct.Description = product.Description;
                editProduct.Id = id;
                editProduct.Image = product.Image;
                editProduct.Name = product.Name;
                editProduct.Price = product.Price;

                context.Commit();

                return RedirectToAction("Index"); 
            }

        }

        public ActionResult Delete(string id)
        {
            Product productToDelete = context.Find(id);
            if(productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }

            
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ComfirmDelete(string id)
        {
            Product productDelete = context.Find(id);

            if(productDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(id);
                context.Commit();

                return RedirectToAction("Index");
            }

            
        }
        
    }
}