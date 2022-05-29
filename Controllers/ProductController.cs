using AdminDashboard.Models;
using AdminDashboard.Models.Repositories;
using AdminDashboard.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository<Product> productRepository;
        private readonly IProductRepository<Category> categoryRepository;
        [Obsolete]
        private readonly IHostingEnvironment hosting;

        [Obsolete]
        public ProductController(IProductRepository<Product> productRepository,
            IProductRepository<Category> categoryRepository, IHostingEnvironment hosting)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.hosting = hosting;
        } 
        // GET: ProductController
        public ActionResult Index()
        {
            var products = productRepository.List();
            return View(products);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            var product = productRepository.Find(id);
            return View(product);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            var model = new ProductCategoryViewModel
            {
                Categories = FillSelectList()
            };
            return View(model);
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public ActionResult Create(ProductCategoryViewModel model)
        {
            try
            {
                string fillName = string.Empty;
                if(model.File != null)
                {
                    //save file in upload folder
                    var upload = Path.Combine(hosting.WebRootPath,"upload");
                    fillName = model.File.FileName;
                    string fullPath = Path.Combine(upload, fillName);
                    model.File.CopyTo(new FileStream(fullPath, FileMode.Create));

                }
              

                if(model.category_id == -1)
                {
                    ViewBag.Message = "Please Select Category!";
                    // fill list of categories to ignore exception 
                    var Vmodel = new ProductCategoryViewModel
                    {
                        Categories = FillSelectList()
                    };
                    return View(Vmodel);
                }
                
                var Category = categoryRepository.Find(model.category_id);

                Product product = new Product
                {
                    Id = model.product_id,
                    product_name = model.product_name,
                    product_Description = model.product_Description,
                    product_price = model.product_price,
                    category = Category,
                    imageURL = fillName

                
                };
                productRepository.Add(product);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            var product = productRepository.Find(id);
            var model = new ProductCategoryViewModel
            {

                product_id = product.Id,
                product_name = product.product_name,
                product_Description = product.product_Description,
                product_price = product.product_price,
                category_id = product.category.category_Id,
                Categories = categoryRepository.List().ToList(),
                imageURL = product.imageURL
           

            };
         
            return View(model);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public ActionResult Edit(int id, ProductCategoryViewModel model)
        {
            try
            {
                string upload = Path.Combine(hosting.WebRootPath, "upload");
                string fillName = string.Empty;
                if (model.File != null)
                {
                    //save file in upload folder
                    fillName = model.File.FileName;
                    string fullPath = Path.Combine(upload, fillName);
                    string oldURL = model.imageURL;
                    if(oldURL != fillName) {
                        // to ckeck that it is not the same image 
                        model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                    }
                }
                else
                {
                    //in case user doesnot choose imge use the old image 
                    fillName = model.imageURL;
                }

                var Category = categoryRepository.Find(model.category_id);
              
                Product product = new Product
                {
                    Id = model.product_id,
                    product_name = model.product_name,
                    product_Description = model.product_Description,
                    product_price = model.product_price,
                    category = Category,
                    imageURL = fillName

                };
                productRepository.Update(model.product_id, product);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
           
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            var product = productRepository.Find(id);
            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult confirmDelete(int id)
        {
            try
            {
                productRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        List<Category> FillSelectList()
        {
            var categories = categoryRepository.List().ToList();
            categories.Insert(0, new Category { category_Id = -1, category_name = " --- Please Select Category ---" });
            return categories;
        }

        public ActionResult Search(string term)
        {
            var result = productRepository.Search(term);
            return View("Index", result);
        }
    }
}
