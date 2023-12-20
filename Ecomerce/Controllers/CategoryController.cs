using Ecomerce.Models;
using Ecomerce.Repositories.interfaces;
using Ecomerce.Repositories.RepoClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecomerce.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View(unitOfWork.category.GetAll());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.category.add(category);
                unitOfWork.save();
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
                return NotFound();
            else
            {
                var category = unitOfWork.category.getById(Id);
                if (category == null)
                    return NotFound();

                return View(category);
            }

        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.category.Update(category);
                unitOfWork.save();
                return RedirectToAction("index");
            }
            else
            {
                return View(category);
            }
        }
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
                return NotFound();
            var category = unitOfWork.category.getById(Id);
            if (category != null)
                return View(category);
            return NotFound();
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var category = unitOfWork.category.getById(Id);
            if (category != null)
            {
                unitOfWork.category.remove(category);
                unitOfWork.save();
                return RedirectToAction("index");
            }
            return NotFound();

        }


    }
}
