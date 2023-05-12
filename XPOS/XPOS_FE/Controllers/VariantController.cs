using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using XPOS_API.Models;
using XPOS_FE.Services;
using XPOS_ViewModels;

namespace XPOS_FE.Controllers
{
    public class VariantController : Controller
    {
        private VariantService variant_service;
        private CategoryService category_service;
        private readonly IHttpContextAccessor contextAccessor;
        private int IdUser = 0;

        public VariantController(VariantService _variantservice, CategoryService _categotyservice, IHttpContextAccessor contextAccessor)
        {
            this.variant_service = _variantservice;
            this.category_service= _categotyservice;
            this.IdUser = contextAccessor.HttpContext.Session.GetInt32("IdUser") ?? 0;

        }
        public async Task<IActionResult> Index(VMSearchPage pg)
        {
            ViewBag.NameSort = String.IsNullOrEmpty(pg.sortOrder) ? "nameVariant_desc" : "";
            ViewBag.CurrentSort = pg.sortOrder;
            ViewBag.pageSize = pg.pageSize;
            if (pg.searchString != null)
            {
                pg.pageNumber = 1;
            }
            else
            {
                pg.searchString = pg.CurrentFilter;
            }
            ViewBag.CurrentFilter = pg.searchString;

            List<VMVariant> dataVariant = await variant_service.AllVariant();
            if (!String.IsNullOrEmpty(pg.searchString))
            {
                dataVariant = dataVariant.Where(a => a.NameVariant.ToLower().Contains(pg.searchString.ToLower())).ToList();
            }
            switch (pg.sortOrder)
            {
                case "nameVariant_desc":
                    dataVariant = dataVariant.OrderByDescending(a => a.NameVariant).ToList();
                    break;
                default:
                    dataVariant = dataVariant.OrderBy(a => a.NameVariant).ToList();
                    break;
            }
            //int pageSize = 3;
            return View(await PaginatedList<VMVariant>.CreateAsync(dataVariant, pg.pageNumber ?? 1, pg.pageSize?? 3));
        }
        public async Task<IActionResult> Create()
        {
            VMVariant data = new VMVariant();
            List<TblCategory> ListCategory = await category_service.AllCategory();
            ViewBag.ListCategory = ListCategory;
            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VMVariant data)
        {
            data.CreateBy = IdUser;
            VMRespons respons = await variant_service.PostVariant(data);
            if (respons.Success)
            {
                return Json(new { dataRespon = respons });
            }

            List<TblCategory> ListCategory = await category_service.AllCategory();
            ViewBag.ListCategory = ListCategory;
            return Json(new { dataRespon = respons });
        } 
        public async Task<IActionResult> Edit(int id)
        {
            VMVariant dataVariant = await variant_service.GetById(id);
            List<TblCategory> ListCategory = await category_service.AllCategory();
            ViewBag.ListCategory = ListCategory;
            return PartialView(dataVariant);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(VMVariant data)
        {
            data.UpdateBy= IdUser;
            VMRespons respons = await variant_service.PutVariant(data);

            if(respons.Success)
            {
                return Json(new { dataRespon = respons });
            }
            List<TblCategory> ListCategory = await category_service.AllCategory();
            ViewBag.ListCategory = ListCategory;
            return Json(new { dataRespon = respons });
        }
        public async Task<IActionResult> Detail(int id)
        {
            VMVariant dataVariant = await variant_service.GetById(id);
            return PartialView(dataVariant);
        }
        public async Task<IActionResult> Delete(int id)
        {
            VMVariant data = await variant_service.GetById(id);
            return PartialView(data);
        }
        public async Task<IActionResult> SureDelete(int Id)
        {
            VMRespons respons = await variant_service.DeleteVariant(Id);
            if (respons.Success)
            {
                return Json(new { dataRespon = respons });
            }
            return Json(new { dataRespon = respons });
        }

    }
}
