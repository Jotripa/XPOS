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
        private int IdUser = 1;

        public VariantController(VariantService _variantservice, CategoryService _categotyservice)
        {
            this.variant_service = _variantservice;
            this.category_service= _categotyservice;
        }
        public async Task<IActionResult> Index()
        {
            List<VMVariant> dataVariant = await variant_service.AllVariant();
            return View(dataVariant);
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
