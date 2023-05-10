using Microsoft.AspNetCore.Mvc;
using XPOS_API.Models;
using XPOS_FE.Services;
using XPOS_ViewModels;

namespace XPOS_FE.Controllers
{
    public class AuthController : Controller
    {
        private AuthService auth_authService;
        private int IdUser = 1;
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
        public async Task<IActionResult> Register(VMUserCostumer data)
        {
            data.CreateBy = IdUser;
            VMRespons respons = await auth_authService.Register(data);

            List<TblRole> roles = await auth_authService.GetAllRole();
            ViewBag.TblRole = roles;
            return Json(respons);
        }
       
    }
}
