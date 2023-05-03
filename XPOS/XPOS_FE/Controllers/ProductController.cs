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
        private readonly IWebHostEnvironment web_host;
        private int IdUser = 1;

        public ProductController(VariantService _variantservice, 
            CategoryService _categotyservice, 
            ProductService _productservice,
            IWebHostEnvironment webHost)
        {
            this.variant_service = _variantservice;
            this.category_service = _categotyservice;
            this.product_service = _productservice;
            this.web_host = webHost;
        }

        #region CRUD
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
            data.Image = Upload(data);
            VMRespons respons = await product_service.PostProduct(data);
            if (respons.Success)
            {
                return Json(new { dataRespon = respons });
            }

            List<TblCategory> ListCategory = await category_service.AllCategory();
            ViewBag.ListCategory = ListCategory;

            List<VMVariant> ListVariant = await variant_service.AllVariant();
            ViewBag.ListVariant = ListVariant;

            return Json(new { dataRespon = respons });
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
            data.Image = Upload(data);
            VMRespons respons = await product_service.PutProduct(data);

            if (respons.Success)
            {
                return Json(new { dataRespon = respons });
            }
            List<TblCategory> ListCategory = await category_service.AllCategory();
            ViewBag.ListCategory = ListCategory;

            List<VMVariant> ListVariant = await variant_service.AllVariant();
            ViewBag.ListVariant = ListVariant;
            return Json(new { dataRespon = respons });
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
                return Json(new { dataRespon = respons });
            }
            return Json(new { dataRespon = respons });
        }
        #endregion

        #region addons function

        public string Upload(VMProduct data)
        {
            string fileName = "";

            if(data.ImageFile != null)
            {
                string uploadFolder = Path.Combine(web_host.WebRootPath, "images");
                fileName = data.ImageFile.FileName;
                string filePath = Path.Combine(uploadFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    data.ImageFile.CopyTo(fileStream);
                }
            }

            return fileName;
        }

        #endregion

    }
}
