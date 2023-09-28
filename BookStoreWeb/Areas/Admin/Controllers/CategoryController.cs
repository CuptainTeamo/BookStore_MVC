using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
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
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category create successfully";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Edit(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFormDB = _unitOfWork.Category.Get(u => u.Id == id);
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
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
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
            Category? categoryFormDB = _unitOfWork.Category.Get(u => u.Id == id);
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
            Category? obj = _unitOfWork.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
