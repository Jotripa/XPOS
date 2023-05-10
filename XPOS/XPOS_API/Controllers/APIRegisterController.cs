using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XPOS_API.Models;
using XPOS_ViewModels;

namespace XPOS_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class APIRegisterController : ControllerBase
    {
        private readonly XPOS_315Context db;
        private VMRespons respons = new VMRespons();

        public APIRegisterController(XPOS_315Context _db)
        {
            this.db = _db;
        }

        [HttpGet("GetRegister")]
        public List<TblRole> GetAllRole()
        {
            List<TblRole> ListRole = db.TblRoles.Where(a => a.IsDelete == false).ToList();
            return ListRole;
        }

       
    }
}
