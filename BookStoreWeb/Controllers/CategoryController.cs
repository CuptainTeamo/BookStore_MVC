﻿using BookStoreWeb.Data;
using BookStoreWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            /*
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Name and display order canot be same");
            }

            if (obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is not a valid name");
            }
            */
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category create successfully";
                return RedirectToAction("Index");
            }
            return View();
            
        }

        public IActionResult Edit(int? id)
        {

            if(id== null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFormDB = _db.Categories.Find(id);
            //Category? categoryFormDB1 = _db.Categories.FirstOrDefault(c => c.Id == id);
            //Category? categoryFormDB2 = _db.Categories.Where(u=>u.Id == id).FirstOrDefault();
            if (categoryFormDB == null)
            {
                return NotFound();
            }
            return View(categoryFormDB);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Delete(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFormDB = _db.Categories.Find(id);
            //Category? categoryFormDB1 = _db.Categories.FirstOrDefault(c => c.Id == id);
            //Category? categoryFormDB2 = _db.Categories.Where(u=>u.Id == id).FirstOrDefault();
            if (categoryFormDB == null)
            {
                return NotFound();
            }
            return View(categoryFormDB);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _db.Categories.Find(id);
            if (obj == null) 
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");

        }
    }
}