﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyEShop.Core.Contracts;
using MyEShop.Core.Models;
using MyEShop.Core.ViewModels;
using MyEShop.DataAccess.InMemory; 

namespace MyEShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> contextCategory; 

        public ProductManagerController(IRepository<Product> product, IRepository<ProductCategory> productCategory)
        {
            context = product;
            contextCategory = productCategory;
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList(); 
            return View(products);
        }

        public ActionResult CreateProduct()
        {
            ProductManagerViewModel productViewModel = new ProductManagerViewModel();
            productViewModel.product = new Product();
            productViewModel.productCategories = contextCategory.Collection();
            return View(productViewModel); 
        }

        [HttpPost]
        public ActionResult CreateProduct(Product product,HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(product);

            }
            else
            {
                if(file != null)
                {
                    product.Image = product.id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//content/ProductImages//") + product.Image);
                }
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
                ProductManagerViewModel productManagerViewModel = new ProductManagerViewModel();
                productManagerViewModel.product = product;
                productManagerViewModel.productCategories = contextCategory.Collection();
                return View(productManagerViewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product, string id,HttpPostedFileBase file)
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

                if (file != null)
                {
                    editProduct.Image = product.id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//content/ProductImages//") + editProduct.Image);
                }

                editProduct.Category = product.Category;
                editProduct.Description = product.Description;
                editProduct.id = id;
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