using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using XPOS_API.Models;
using XPOS_FE.Services;

namespace XPOS_FE.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryService category_service;
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
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            TblCategory dataCategory = await category_service.GetById(id);
            return View(dataCategory);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(TblCategory data)
        {

            return View();
        }
        public async Task<IActionResult> Detail(int id)
        {
            TblCategory dataCategory = await category_service.GetById(id);
            return View(dataCategory);
        }
        public IActionResult Delete()
        {
            return View();
        }
    }
}
