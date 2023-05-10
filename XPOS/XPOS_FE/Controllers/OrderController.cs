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

        public async Task<IActionResult> OrderHistory(VMSearchPage pg)
        {
            ViewBag.NameSort = String.IsNullOrEmpty(pg.sortOrder) ? "date_desc" : "";
            ViewBag.PriceSort = pg.sortOrder == "code" ? "code_desc" : "code";
            ViewBag.CurrentSort = pg.sortOrder;
            ViewBag.pageSize = pg.pageSize;

            ViewBag.searchMinPrice = pg.minPrice;
            ViewBag.searchMaxPrice = pg.maxPrice;

            ViewBag.searchStartDate = pg.startDate;
            ViewBag.searchEndDate = pg.endDate;

            ViewBag.TotalHistory = await order_service.TotalHistory(IdUser);

            if (pg.searchString != null)
            {
                pg.pageNumber = 1;
            }
            else
            {
                pg.searchString = pg.CurrentFilter;
            }
            ViewBag.CurrentFilter = pg.searchString;

            List<VMOrderHeader> orderHeader = await order_service.GetAllOrder();
            if (!String.IsNullOrEmpty(pg.searchString))
            {
                orderHeader = orderHeader.Where(a => a.CodeTransaction.Contains(pg.searchString)).ToList();
            }

            if (pg.minPrice != null || pg.maxPrice != null)
            {
                pg.minPrice = pg.minPrice == null ? decimal.MinValue : pg.minPrice;
                pg.maxPrice = pg.maxPrice == null ? decimal.MaxValue : pg.maxPrice;

                orderHeader = orderHeader.Where(a => a.Amount >= pg.minPrice && a.Amount <= pg.maxPrice).ToList();
            }
            if (pg.startDate != null || pg.endDate != null)
            {
                pg.startDate = pg.startDate == null ? DateTime.MinValue : pg.startDate;
                pg.endDate = pg.endDate == null ? DateTime.MaxValue : pg.endDate;

                orderHeader = orderHeader.Where(a => a.CreateDate >= pg.startDate && a.CreateDate <= pg.endDate).ToList();
            }
            switch (pg.sortOrder)
            {
                case "date_desc":
                    orderHeader = orderHeader.OrderByDescending(a => a.CreateDate).ToList();
                    break;
                case "code_desc":
                    orderHeader = orderHeader.OrderByDescending(a => a.CodeTransaction).ToList();
                    break;
                case "code":
                    orderHeader = orderHeader.OrderBy(a => a.CodeTransaction).ToList();
                    break;
                default:
                    orderHeader = orderHeader.OrderBy(a => a.CreateDate).ToList();
                    break;
            }
            //int pageSize = 3;
            return View(await PaginatedList<VMOrderHeader>.CreateAsync(orderHeader, pg.pageNumber ?? 1, pg.pageSize ?? 3));
        }

        public async Task<IActionResult> searchPage()
        {
            return PartialView();
        }


    }
}
