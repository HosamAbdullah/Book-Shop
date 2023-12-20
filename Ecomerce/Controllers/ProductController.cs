using Ecomerce.Models;
using Ecomerce.Repositories.interfaces;
using Ecomerce.Repositories.RepoClasses;
using Ecomerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Ecomerce.Helpers;
using System.Security.Policy;
using Microsoft.AspNetCore.Authorization;

namespace Ecomerce.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View(unitOfWork.product.GetAllWithInclude());
        }
        public IActionResult Create()
        {
            List<Category> categories = unitOfWork.category.GetAll().ToList();
            ViewData["Categories"] = categories;
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductViewModel productVm)
        {
            if (ModelState.IsValid && productVm.categoryId != 0 && productVm.ImgUrl != null)
            {
                string url = ImageSettings.UploadImage(productVm.ImgUrl);
                Product product = new Product()
                {
                    Title = productVm.Title,
                    Author = productVm.Author,
                    Description = productVm.Description,
                    ISBN = productVm.ISBN,
                    ListPrice = productVm.ListPrice,
                    Price = productVm.Price,
                    Price50 = productVm.Price50,
                    Price100 = productVm.Price100,
                    ImgUrl = url,
                    categoryId = productVm.categoryId
                };

                unitOfWork.product.add(product);
                unitOfWork.save();
                return RedirectToAction("Index");
            }
            else
            {
                List<Category> categories = unitOfWork.category.GetAll().ToList();
                ViewData["Categories"] = categories;
                return View(productVm);
            }
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
                return NotFound();
            else
            {
                var product = unitOfWork.product.getById(Id);
                if (product == null)
                    return NotFound();
                ProductViewModel productVm = new ProductViewModel()
                {
                    Title = product.Title,
                    Author = product.Author,
                    Description = product.Description,
                    ISBN = product.ISBN,
                    ListPrice = product.ListPrice,
                    Price = product.Price,
                    Price50 = product.Price50,
                    Price100 = product.Price100,
                    ImageName = product.ImgUrl,
                    categoryId = product.categoryId
                };
                List<Category> categories = unitOfWork.category.GetAll().ToList();
                ViewData["Categories"] = categories;
                return View(productVm);
            }

        }
        [HttpPost]
        public IActionResult Edit(ProductViewModel productVm, [FromRoute] int id)
        {
            if (ModelState.IsValid && productVm.categoryId != 0)
            {
                string url = "";
                if (productVm.ImgUrl != null)
                    url = ImageSettings.UploadImage(productVm.ImgUrl);
                else
                    url = productVm.ImageName;
                Product product = unitOfWork.product.getById(id);
                product.Title = productVm.Title;
                product.Author = productVm.Author;
                product.Description = productVm.Description;
                product.ISBN = productVm.ISBN;
                product.ListPrice = productVm.ListPrice;
                product.Price = productVm.Price;
                product.Price50 = productVm.Price50;
                product.Price100 = productVm.Price100;
                product.ImgUrl = url;
                product.categoryId = productVm.categoryId;
                unitOfWork.product.Update(product);
                unitOfWork.save();
                return RedirectToAction("index");
            }
            else
            {
                List<Category> categories = unitOfWork.category.GetAll().ToList();
                ViewData["Categories"] = categories;
                return View(productVm);
            }
        }
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
                return NotFound();
            var product = unitOfWork.product.GetByIdWithInclude(Id);
            if (product != null)
                return View(product);

            return NotFound();
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var product = unitOfWork.product.getById(Id);
            if (product != null)
            {
                unitOfWork.product.remove(product);
                unitOfWork.save();
                return RedirectToAction("index");
            }
            return NotFound();

        }


    }
}
