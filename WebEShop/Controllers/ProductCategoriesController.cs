﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebEShop.Data;
using WebEShop.Models;

namespace WebEShop.Controllers
{
    public class ProductCategoriesController : Controller
    {
        private WebEShopDBContext context;

        public ProductCategoriesController()
        {
            context = new WebEShopDBContext();
        }

        // HTTP CLASSICAL METHODS: POST(C), GET(R), PUT(U), DELETE(D)
        // GET: ProductCategories
        public ActionResult Index()
        {
            context.ProductCategories.Add(new ProductCategory()
            {
                Title = "Pen",
                Description = "Pen Description",
                Products = new List<CustomerProduct>()
                {
                    new CustomerProduct()
                    {
                        Title = "Red  Pen",
                        Description = "Red Pen Description",
                        Price = 10.45
                    },
                    new CustomerProduct()
                    {
                        Title = "Blue  Pen",
                        Description = "Blue Pen Description",
                        Price = 10.45
                    }
                }
            });
            context.SaveChanges();
            return View();
        }

        public ViewResult MyDummyList()
        {
            //var listOfProductCategories = context.ProductCategories.ToList();
            ////int idOfFirstProductOnCat0 = listOfProductCategories[0].Products.ToList()[0].Id;
            
            //ViewBag.ProductCategories = listOfProductCategories;
            return View();
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(ProductCategory productCategory)
        {
            string created = "created";
            string updated = "updated";
            string result = "";
            if (productCategory.Id == 0)
            {
                result = created;
            }
            else
            {
                result = updated;
            }
            context.ProductCategories.AddOrUpdate(productCategory); // ModelBinding
            context.SaveChanges();
            return RedirectToAction("List", 
                new { message = $"A New Product Category is been {result} successfully!" });
        }

        public ViewResult List(string message)
        {
            ViewBag.Message = message;
            return View(context.ProductCategories.AsEnumerable<ProductCategory>()); //as IEnumerable<ProductCategory>);
        }

        public ActionResult Details(int id)
        {
            using (var category = context.ProductCategories.Find(id))
            {
                if(category != null)
                {
                    var products = category.Products.ToList();
                    if(products.Count > 0)
                    {
                        //ViewBag.Products = true;
                        ViewData.Add("ProductsList", products); // ViewBag.Products = products;
                    }

                    //else
                    //{
                    //    ViewData.Add("Products", "No Products");
                    //}
                    return View(category);
                }
                ViewBag.Result = $"No ProductCategory exists with id = {id}";
                return View();
            }
            
        }

        public ActionResult Delete(int id)
        {
            //var productCategoryToBeDeleted = context.ProductCategories.Find(id);
            //if(productCategoryToBeDeleted != null)
            //{
            //    context.ProductCategories.Remove(productCategoryToBeDeleted);
            //}

            using (var productCategoryToBeDeleted1 = context.ProductCategories.Find(id))
            {
                try
                {
                    context.ProductCategories.Remove(productCategoryToBeDeleted1);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index", "Home", new { message = $"Product Category with id {id} was not found!" });
                    //return ex.Message;
                    //throw new NotFoundEntityException(ex.Message);
                }
                string path = $"ProductCategory with id {id} is deleted succesfully!";
                return RedirectToAction("List", new { message = path });
            }
        }

        public ActionResult Edit(int id)
        {
            return View(context.ProductCategories.Find(id));
        }
    }
}