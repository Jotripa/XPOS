using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Drawing.Printing;
using XPOS_API.Models;
using XPOS_FE.Services;
using XPOS_ViewModels;
using Microsoft.AspNetCore.Http;


namespace XPOS_FE.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryService category_service;
        private int IdUser = 1;
        private readonly IHttpContextAccessor contextAccessor;
        public CategoryController(CategoryService _categoryservice, IHttpContextAccessor _contextAccessor)
        {
            this.category_service = _categoryservice;
            this.contextAccessor = _contextAccessor;
            this.IdUser = contextAccessor.HttpContext.Session.GetInt32("IdUser") ?? 1;
        }
        public async Task<IActionResult> Index(VMSearchPage pg)
        {

            ViewBag.NameSort = String.IsNullOrEmpty(pg.sortOrder) ? "nameCategory_desc" : "";
            ViewBag.CurrentSort = pg.sortOrder;
            ViewBag.pageSize = pg.pageSize;
            if(pg.searchString != null)
            {
                pg.pageNumber = 1;
            }
            else
            {
                pg.searchString = pg.CurrentFilter;
            }
            ViewBag.CurrentFilter = pg.searchString;

            List<TblCategory> dataCategory = await category_service.AllCategory();
            if (!String.IsNullOrEmpty(pg.searchString))
            {
                dataCategory = dataCategory.Where(a => a.NameCategory.ToLower().Contains(pg.searchString.ToLower())).ToList();
            }

            switch (pg.sortOrder)
            {
                case "nameCategory_desc":
                    dataCategory = dataCategory.OrderByDescending(a => a.NameCategory).ToList();
                    break;
                default:
                    dataCategory = dataCategory.OrderBy(a => a.NameCategory).ToList();
                    break;
            }
            //int pageSize = 3;
            return View(await PaginatedList<TblCategory>.CreateAsync(dataCategory, pg.pageNumber ?? 1, pg.pageSize ?? 3));
        }

        public IActionResult Create()
        {
            TblCategory data = new TblCategory();
            return PartialView(data);
        }
        [HttpPost]
        public async Task<IActionResult> Create(TblCategory data)
        {
            data.CreateBy = IdUser;


            VMRespons respons = await category_service.PostCategory(data);
            if (respons.Success)
            {
                return Json(new { dataRespon = respons});
            }
            return Json(new { dataRespon = respons});
        }
        public async Task<IActionResult> Edit(int id)
        {
            TblCategory dataCategory = await category_service.GetById(id);
            return PartialView(dataCategory);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(TblCategory data)
        {
            data.UpdateBy = IdUser;

            VMRespons respons = await category_service.PutCategory(data);

            if (respons.Success)
            {
                return Json(new { dataRespon = respons});
            }

            return Json(new {dataRespon = respons});
        }
        public async Task<IActionResult> Detail(int id)
        {
            TblCategory dataCategory = await category_service.GetById(id);
            return PartialView(dataCategory);
        }
        public async Task<IActionResult> Delete(int id)
        {
            TblCategory data = await category_service.GetById(id);
            return PartialView(data);
        }
        public async Task<IActionResult> SureDelete(int Id)
        {
            VMRespons respons = await category_service.DeleteCategory(Id);
            if (respons.Success)
            {
                return Json(new { dataRespon = respons });
            }
            return Json(new { dataRespon = respons });
        }
    }
}
