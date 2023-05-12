using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using XPOS_FE.Models;
using XPOS_FE.Services;

namespace XPOS_FE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly OrderService orderService;
        private readonly IHttpContextAccessor contextAccessor;
        private int IdCustomer = 0;
        public HomeController(ILogger<HomeController> logger, OrderService order_service, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            this.orderService = order_service;
            this.contextAccessor = contextAccessor;
            this.IdCustomer = contextAccessor.HttpContext.Session.GetInt32("IdCustomer") ?? 0;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.TotalHistory = await orderService.TotalHistory(IdCustomer);
            ViewBag.SumTotalHistory = await orderService.SumTotalHistory(IdCustomer);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Category()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}