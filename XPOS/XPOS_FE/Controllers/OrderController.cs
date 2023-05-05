using Microsoft.AspNetCore.Mvc;
using XPOS_FE.Services;
using XPOS_ViewModels;

namespace XPOS_FE.Controllers
{
    public class OrderController : Controller
    {
        private ProductService product_service;

        public OrderController(ProductService _product_service)
        {
            this.product_service = _product_service;
        }
        public async Task<IActionResult> Menu()
        {
            List<VMProduct> dataProduct = await product_service.AllProduct();
            return View(dataProduct);
        }
    }
}
