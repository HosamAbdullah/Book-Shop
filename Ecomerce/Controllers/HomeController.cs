using Ecomerce.Models;
using Ecomerce.Repositories.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ecomerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<user> userManager;

        public HomeController(IUnitOfWork unitOfWork, UserManager<user> _userManager)
        {
            this.unitOfWork = unitOfWork;
            userManager = _userManager;
        }
        public IActionResult Index()
        {

            return View(unitOfWork.product.GetAllWithInclude());
        }
        public IActionResult Details(int Id)
        {
            ShoppingCart Cart = new ShoppingCart()
            {
                product = unitOfWork.product.GetByIdWithInclude(Id),
                ProductId = Id,
                Count = 1
            };
            return View(Cart);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var user = userManager.GetUserAsync(User).Result;
            var userId = user.Id;
            ShoppingCart newCart = new ShoppingCart()
            {
                ProductId = shoppingCart.ProductId,
                UserId = userId,
                Count = shoppingCart.Count,
            };
            ShoppingCart cartFromDb = unitOfWork.shoppingCart.GetByUserIDAndProductId(userId, shoppingCart.ProductId);
            if (cartFromDb != null)
            {
                cartFromDb.Count += shoppingCart.Count;
                unitOfWork.shoppingCart.Update(cartFromDb);
            }
            else
            {
                unitOfWork.shoppingCart.add(newCart);
            }
            unitOfWork.save();
            return RedirectToAction("Index");
        }
    }
}