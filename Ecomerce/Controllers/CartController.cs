using Ecomerce.Models;
using Ecomerce.Repositories.interfaces;
using Ecomerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecomerce.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<user> userManager;

        public CartController(IUnitOfWork unitOfWork, UserManager<user> _userManager)
        {
            this.unitOfWork = unitOfWork;
            userManager = _userManager;
        }

        public IActionResult Index()
        {
            var user = userManager.GetUserAsync(User).Result;
            string userId = user.Id;
            ShoppingCartVM ShoppingCartVM = new ShoppingCartVM()
            {
                ShoppingCartList = unitOfWork.shoppingCart.GetAllByUserIdIncludeProducts(userId),
            };
            foreach (var item in ShoppingCartVM.ShoppingCartList)
            {
                item.price = GetPriceBasedOnQuantity(item);
                ShoppingCartVM.OrderTotal += item.price * item.Count;
            }

            return View(ShoppingCartVM);
        }

        public IActionResult Order()
        {
            var user = userManager.GetUserAsync(User).Result;
            var userId = user.Id;
            OrderVM orderVM = new OrderVM()
            {
                shoppingCartList = unitOfWork.shoppingCart.GetAllByUserIdIncludeProducts(userId),
                Name = "",
                postalCode = "",
                PhoneNumber = "",
                address = "",
                City = "",
                State = "",
                OrderStatus = "pending"

            };
            foreach (var item in orderVM.shoppingCartList)
            {
                item.price = GetPriceBasedOnQuantity(item);
                orderVM.OrderTotal += item.price * item.Count;
            }

            return View(orderVM);
        }

        [HttpPost]
        [ActionName("Order")]
        public IActionResult OrderPost(OrderVM orderVM)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.GetUserAsync(User).Result;
                var userId = user.Id;
                Order order = new Order()
                {
                    OrderDate = DateTime.Now,
                    ShippingDate = DateTime.Now.AddDays(7).Date,
                    OrderTotal = orderVM.OrderTotal,
                    OrderStatus = "pending",
                    UserId = userId,
                    Name = orderVM.Name,
                    postalCode = orderVM.postalCode,
                    PhoneNumber = orderVM.PhoneNumber,
                    address = orderVM.address,
                    City = orderVM.City,
                    State = orderVM.State

                };
                unitOfWork.order.add(order);
                unitOfWork.save();
                var cartList = unitOfWork.shoppingCart.GetAllByUserIdIncludeProducts(userId);
                foreach (var item in cartList)
                {
                    item.price = GetPriceBasedOnQuantity(item);
                    unitOfWork.shoppingCart.Update(item);
                }
                unitOfWork.save();


                foreach (var detail in cartList)
                {
                    OrderDetails details = new OrderDetails()
                    {
                        ProductId = detail.ProductId,
                        Quantity = detail.Count,
                        OrderId = order.Id,
                        Price = detail.price
                    };
                    unitOfWork.orderDetails.add(details);

                }
                unitOfWork.save();

                foreach (var cart in cartList)
                {
                    unitOfWork.shoppingCart.remove(cart);
                }
                unitOfWork.save();

                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }
            else return NotFound();

        }

        public IActionResult Increase(int Id)
        {
            var cart = unitOfWork.shoppingCart.GetByIdWithInclude(Id);
            cart.Count++;
            unitOfWork.shoppingCart.Update(cart);
            unitOfWork.save();
            return RedirectToAction("Index");
        }
        public IActionResult Decrease(int Id)
        {
            var cart = unitOfWork.shoppingCart.GetByIdWithInclude(Id);
            if (cart.Count > 1)
            {
                cart.Count--;
                unitOfWork.shoppingCart.Update(cart);
            }
            else
            {
                unitOfWork.shoppingCart.remove(cart);
            }
            unitOfWork.save();
            return RedirectToAction("Index");

        }
        public IActionResult Delete(int Id)
        {
            var cart = unitOfWork.shoppingCart.GetByIdWithInclude(Id);
            unitOfWork.shoppingCart.remove(cart);
            unitOfWork.save();
            return RedirectToAction("Index");
        }
        private double GetPriceBasedOnQuantity(ShoppingCart shoppingCart)
        {
            if (shoppingCart.Count <= 50)
                return shoppingCart.product.Price;
            else
            {
                if (shoppingCart.Count <= 100)
                    return shoppingCart.product.Price50;
                else
                    return shoppingCart.product.Price100;
            }
        }

    }
}
