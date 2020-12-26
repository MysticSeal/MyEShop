using MyEShop.Core.Models;
using MyEShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEShop.WebUI.Controllers
{
    public class ProductCategoryController : Controller
    {
        ProductCategoryRepository context;

        public ProductCategoryController()
        {
            context = new ProductCategoryRepository();
        }
        // GET: ProductCategory
        public ActionResult Index()
        {
            List<ProductCategory> productsCategories = context.Collection().ToList();
            return View(productsCategories);
        }

        public ActionResult CreateProductCategory()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }

        [HttpPost]
        public ActionResult CreateProductCategory(ProductCategory product)
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
            ProductCategory product = context.Find(id);
            if(product == null)
            {
                throw new Exception("Product Not Found");
            }
            else
            {
                return View(product);
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory product, string id)
        {
            ProductCategory productCategory = context.Find(id);
            if(productCategory == null)
            {
                throw new Exception("ProductCategory Not Found");
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                productCategory.id = product.id;
                productCategory.category = product.category;

                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string id)
        {
            ProductCategory productCategory = context.Find(id);

            if (productCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategory);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            ProductCategory productDelete = context.Find(id);

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