using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using NimapProject.Models;
using PagedList.Mvc;
using PagedList;

namespace NimapProject.Controllers
{
    public class ProductController : Controller
    {
        StoreDbContext dc = new StoreDbContext();
        public ViewResult DisplayProducts(int? i)
        {
            dc.Configuration.LazyLoadingEnabled = false;
            var products=dc.Products.Include(P=>P.Category).ToList();
            return View(products.ToList().ToPagedList(i ?? 1,10));
        }
        public ViewResult DisplayProduct(int  productId)
        {
            dc.Configuration.LazyLoadingEnabled = false;
            Product product = (dc.Products.Include(P=>P.Category).Where(P=>P.ProductId == productId)).Single();
            return View(product);
        }
        [HttpGet]
        public ViewResult AddProduct()
        {
            ViewBag.CategoryId = new SelectList(dc.Categories, "CategoryId", "CategoryName");
            Product product = new Product();
            return View(product);
        }
        [HttpPost]
        public RedirectToRouteResult AddProduct(Product product)
        {
            dc.Products.Add(product);
            dc.SaveChanges();
            return RedirectToAction("DisplayProducts");
        }
        public ViewResult EditProduct(int productId)
        {
            Product product = dc.Products.Find(productId);
            ViewBag.CategoryId = new SelectList(dc.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }
        public RedirectToRouteResult UpdateProduct(Product product)
        {
            dc.Entry(product).State = EntityState.Modified;
            dc.SaveChanges();
            return RedirectToAction("DisplayProducts");
        }
        public RedirectToRouteResult DeleteProduct(int productId)
        {
            Product product = dc.Products.Find(productId);
            //dc.Entry(product).State = EntityState.Modified;
            dc.Products.Remove(product);
            dc.SaveChanges();
            return RedirectToAction("DisplayProducts");
        }
    }
}