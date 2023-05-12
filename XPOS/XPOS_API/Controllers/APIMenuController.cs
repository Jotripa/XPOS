using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XPOS_API.Models;
using XPOS_ViewModels;

namespace XPOS_API.Controllers
{
    
    [Route("[controller]")]
    [ApiController]

    public class APIMenuController : ControllerBase
    {
        private readonly XPOS_315Context db;
        private VMRespons respons = new VMRespons();

        public APIMenuController(XPOS_315Context _db)
        {
            this.db = _db;
        }
    
        [HttpGet("GetMenu/{IdRole}")]
        public List<VMMenu> GetMenu(int IdRole)
        {
            List<VMMenu> ListMenu = (from m in db.TblMenus
                                     join ma in db.TblMenuAccesses
                                     on m.Id equals ma.IdMenu
                                     where m.IsParent == true && ma.IdRole == IdRole
                                     select new VMMenu
                                     {                                        
                                         MenuName = m.MenuName,
                                         MenuAction= m.MenuAction,
                                         MenuController= m.MenuController,
                                         MenuIcon= m.MenuIcon,
                                         MenuSorting= m.MenuSorting,
                                         MenuParent= m.MenuParent,
                                         childMenu = (from men in db.TblMenus
                                                      join menas in db.TblMenuAccesses
                                                      on men.Id equals menas.IdMenu
                                                      where men.IsParent == false && men.MenuParent == m.Id && men.IsDelete == false && menas.IdRole == IdRole
                                                      select new VMMenu
                                                      {
                                                          MenuName= men.MenuName,
                                                          MenuAction = men.MenuAction,
                                                          MenuController= men.MenuController,
                                                          MenuIcon= men.MenuIcon,
                                                          MenuSorting= men.MenuSorting,
                                                          MenuParent= men.MenuParent
                                                      }).ToList()
                                     }).ToList();
            return ListMenu;
        }
    }
}
