using Microsoft.AspNetCore.Mvc;
using XPOS_API.Models;
using XPOS_FE.Services;
using XPOS_ViewModels;

namespace XPOS_FE.Controllers
{
    public class ProductController : Controller
    {
        private VariantService variant_service;
        private CategoryService category_service;
        private ProductService product_service;
        private int IdUser = 1;

        public ProductController(VariantService _variantservice, CategoryService _categotyservice, ProductService _productservice)
        {
            this.variant_service = _variantservice;
            this.category_service = _categotyservice;
            this.product_service = _productservice;
        }
        public async Task<IActionResult> Index()
        {
            List<VMProduct> dataProduct = await product_service.AllProduct();
            return View(dataProduct);
        }
        public async Task<IActionResult> Create()
        {
            VMProduct data = new VMProduct();
            List<TblCategory> ListCategory = await category_service.AllCategory();
            ViewBag.ListCategory = ListCategory;

            List<VMVariant> ListVariant = await variant_service.AllVariant();
            ViewBag.ListVariant = ListVariant;

            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VMProduct data)
        {
            data.CreateBy = IdUser;
            VMRespons respons = await product_service.PostProduct(data);
            if (respons.Success)
            {
                return RedirectToAction("Index");
            }

            List<TblCategory> ListCategory = await category_service.AllCategory();
            ViewBag.ListCategory = ListCategory;

            List<VMVariant> ListVariant = await variant_service.AllVariant();
            ViewBag.ListVariant = ListVariant;

            return View(data);
        }

        public async Task<IActionResult> Edit(int id)
        {
            VMProduct dataProduct = await product_service.GetById(id);
            List<TblCategory> ListCategory = await category_service.AllCategory();
            ViewBag.ListCategory = ListCategory;

            List<VMVariant> ListVariant = await variant_service.AllVariant();
            ViewBag.ListVariant = ListVariant;

            return PartialView(dataProduct);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(VMProduct data)
        {
            data.UpdateBy = IdUser;
            VMRespons respons = await product_service.PutProduct(data);

            if (respons.Success)
            {
                return RedirectToAction("Index");
            }
            List<TblCategory> ListCategory = await category_service.AllCategory();
            ViewBag.ListCategory = ListCategory;

            List<VMVariant> ListVariant = await variant_service.AllVariant();
            ViewBag.ListVariant = ListVariant;
            return View(data);
        }

        public async Task<IActionResult> Detail(int id)
        {
            VMProduct DataProduct = await product_service.GetById(id);
            return PartialView(DataProduct);
        }
        public async Task<IActionResult> Delete(int id)
        {
            VMProduct dataProduct = await product_service.GetById(id);
            return PartialView(dataProduct);
        }

        public async Task<IActionResult> SureDelete(int Id)
        {
            VMRespons respons = await product_service.DeleteProduct(Id);
            if (respons.Success)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Delete", Id);
        }

    }
}
