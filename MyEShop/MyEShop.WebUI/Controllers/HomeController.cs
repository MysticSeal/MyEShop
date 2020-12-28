using MyEShop.Core.Contracts;
using MyEShop.Core.Models;
using MyEShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> contextCategory;

        public HomeController(IRepository<Product> product, IRepository<ProductCategory> productCategory)
        {
            context = product;
            contextCategory = productCategory;
        }

        
        public ActionResult Index(string Category = null)
        {
            List<Product> products;
            List<ProductCategory> categories = contextCategory.Collection().ToList();

            if(Category == null)
            {
                products = context.Collection().ToList();
            }
            else
            {
                products = context.Collection().Where(p => p.Category == Category).ToList();
            }

            ProductListViewModel productListView = new ProductListViewModel();
            productListView.products = products;
            productListView.productCategory = categories;

            return View(productListView);
            
        }


        public ActionResult Details(string id)
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
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}