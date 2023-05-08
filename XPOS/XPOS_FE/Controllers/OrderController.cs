using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using XPOS_FE.Services;
using XPOS_ViewModels;

namespace XPOS_FE.Controllers
{
    public class OrderController : Controller
    {
        private ProductService product_service;
        private OrderService order_service;
        private int IdUser = 1;

        public OrderController(ProductService _product_service,
            OrderService order_service)
        {
            this.product_service = _product_service;
            this.order_service = order_service;
        }
        public async Task<IActionResult> Menu()
        {
            List<VMProduct> dataProduct = await product_service.AllProduct();
            return View(dataProduct);
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(VMOrderHeader dataHeader)
        {
            return PartialView(dataHeader);
        }
        [HttpPost]
        public async Task<IActionResult> SubmitPayment(VMOrderHeader dataHeader)
        {
            VMRespons respons = new VMRespons();

            dataHeader.CreateBy = IdUser;
            dataHeader.IdCustomer = IdUser;

            respons = await order_service.SubmitPayment(dataHeader);
            return Json(respons);
        }

        public async Task<IActionResult> GetAllOrder()
        {
            List<VMOrderHeader> ListOrder = await order_service.GetAllOrder();
            return PartialView(ListOrder);
        }
    }
}
