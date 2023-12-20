using Ecomerce.Repositories.interfaces;
using Ecomerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecomerce.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View(unitOfWork.order.GetAll());
        }
        public IActionResult Details(int Id)
        {
            var order = unitOfWork.order.getById(Id);
            var orderDetailsList = unitOfWork.orderDetails.GetAllById(order.Id);
            OrderAdminVM orderAdminVM = new OrderAdminVM()
            {
                OrderId = order.Id,
                Name = order.Name,
                postalCode = order.postalCode,
                State = order.State,
                City = order.City,
                address = order.address,
                PhoneNumber = order.PhoneNumber,
                OrderTotal = order.OrderTotal,
                OrderStatus = order.OrderStatus,
                detailsList = orderDetailsList
            };
            return View(orderAdminVM);
        }
        [HttpPost]
        public IActionResult Details(OrderAdminVM orderAdminVM)
        {
            var order = unitOfWork.order.getById(orderAdminVM.OrderId);
            order.OrderStatus = orderAdminVM.OrderStatus;
            unitOfWork.order.Update(order);
            unitOfWork.save();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int Id)
        {
            var order = unitOfWork.order.getById(Id);
            var orderDetailsList = unitOfWork.orderDetails.GetAllById(order.Id);
            OrderAdminVM orderAdminVM = new OrderAdminVM()
            {
                OrderId = order.Id,
                Name = order.Name,
                postalCode = order.postalCode,
                State = order.State,
                City = order.City,
                address = order.address,
                PhoneNumber = order.PhoneNumber,
                OrderTotal = order.OrderTotal,
                OrderStatus = order.OrderStatus,
                detailsList = orderDetailsList
            };
            return View(orderAdminVM);
        }
        [HttpPost]
        public IActionResult Delete(OrderAdminVM orderAdminVM)
        {
            var order = unitOfWork.order.getById(orderAdminVM.OrderId);
            var orderDetailsList = unitOfWork.orderDetails.GetAllById(order.Id);
            unitOfWork.order.remove(order);
            foreach (var item in orderDetailsList)
            {
                unitOfWork.orderDetails.remove(item);
            }
            unitOfWork.save();
            return RedirectToAction("Index");


        }
    }
}
