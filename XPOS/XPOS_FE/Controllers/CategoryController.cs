using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using XPOS_API.Models;
using XPOS_FE.Services;
using XPOS_ViewModels;

namespace XPOS_FE.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryService category_service;
        private int IdUser = 1;
        public CategoryController(CategoryService _categoryservice)
        {
            this.category_service = _categoryservice;
        }
        public async Task<IActionResult> Index()
        {
            List<TblCategory> dataCategory = await category_service.AllCategory();
            return View(dataCategory);
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
