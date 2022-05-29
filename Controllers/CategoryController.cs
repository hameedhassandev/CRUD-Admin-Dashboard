using AdminDashboard.Models;
using AdminDashboard.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IProductRepository<Category> categoryRepository;
        public CategoryController(IProductRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        // GET: CategoryController
        public ActionResult Index()
        {
            var categories = categoryRepository.List();
            return View(categories);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            var categories = categoryRepository.Find(id);
            return View(categories);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
      
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category categories)
        {
            try
            {
                categoryRepository.Add(categories); 
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var category = categoryRepository.Find(id);
            return View(category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category category)
        {
            try
            {
                categoryRepository.Update(id, category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            var category = categoryRepository.Find(id);
            return View(category);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category category)
        {
            try
            {
                categoryRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
