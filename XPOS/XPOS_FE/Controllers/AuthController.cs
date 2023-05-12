using Microsoft.AspNetCore.Mvc;
using XPOS_API.Models;
using XPOS_FE.Services;
using XPOS_ViewModels;

namespace XPOS_FE.Controllers
{
    public class AuthController : Controller
    {
        private AuthService auth_authService;
        private int IdUser = 0;
        public AuthController(AuthService auth_authService)
        {
            this.auth_authService= auth_authService;
        }
        public IActionResult Login()
        {
            return PartialView();
        }

        public async Task<IActionResult> Register()
        {
            List<TblRole> roles = await auth_authService.GetAllRole();
            ViewBag.TblRole = roles;
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitRegister(VMUserCostumer data)
        {
            data.CreateBy = IdUser;
            data.CreateDate= DateTime.Now;
            VMRespons respons = await auth_authService.Register(data);

            List<TblRole> roles = await auth_authService.GetAllRole();
            ViewBag.TblRole = roles;
            return Json(respons);
        }

      
        public async Task<JsonResult> CheckEmailDuplicate(string email)
        {
            bool IsDuplicate = await auth_authService.CheckEmailDuplicate(email);
            return Json(IsDuplicate);
        }

        [HttpPost]
        public async Task<IActionResult> Login(VMUserCostumer data)
        {
            VMUserCostumer dataUserCust = await auth_authService.CheckLogin(data);
            VMRespons respons = new VMRespons();
            if(dataUserCust != null)
            {
                HttpContext.Session.SetString("NameCustomer", dataUserCust.NameCustomer);
                HttpContext.Session.SetInt32("IdCustomer", dataUserCust.IdCustomer);
                HttpContext.Session.SetInt32("IdUser", dataUserCust.IdUser);
                HttpContext.Session.SetInt32("IdRole", dataUserCust.IdRole);

                respons.Message = $"Welcome, to xpos application {dataUserCust.NameCustomer}"; 
            }
            else
            {
                respons.Success = false;
                respons.Message = "Your Account is not register yet";
            }
            return Json(respons);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Json(true);
        }

    }
}
