using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
        private readonly IHttpContextAccessor contextAccessor;
        private int IdUser = 0;

        public ProductController(VariantService _variantservice, 
            CategoryService _categotyservice, 
            ProductService _productservice,
            IWebHostEnvironment webHost,
            IHttpContextAccessor contextAccessor)
        {
            this.variant_service = _variantservice;
            this.category_service = _categotyservice;
            this.product_service = _productservice;
            this.web_host = webHost;
            this.IdUser = contextAccessor.HttpContext.Session.GetInt32("IdUser") ?? 0;
        }

        #region CRUD
        public async Task<IActionResult> Index(VMSearchPage pg)
        {
            ViewBag.NameSort = String.IsNullOrEmpty(pg.sortOrder) ? "name_desc" : "";
            ViewBag.PriceSort = pg.sortOrder == "price" ? "price_desc" : "price";
            ViewBag.CurrentSort = pg.sortOrder;
            ViewBag.pageSize = pg.pageSize;
            ViewBag.searchMinPrice = pg.minPrice;
            ViewBag.searchMaxPrice = pg.maxPrice;

            if(pg.searchString != null)
            {
                pg.pageNumber = 1;
            }
            else
            {
                pg.searchString = pg.CurrentFilter;
            }
            ViewBag.CurrentFilter = pg.searchString;

            List<VMProduct> dataProduct = await product_service.AllProduct();
            if (!String.IsNullOrEmpty(pg.searchString))
            {
                dataProduct = dataProduct.Where(a => a.NameProduct.ToLower().Contains(pg.searchString.ToLower())).ToList();
            }

            if(pg.minPrice != null || pg.maxPrice != null)
            {
                pg.minPrice = pg.minPrice == null ? decimal.MinValue : pg.minPrice;
                pg.maxPrice = pg.maxPrice == null ? decimal.MaxValue : pg.maxPrice;

                dataProduct = dataProduct.Where(a => a.Price >= pg.minPrice && a.Price <= pg.maxPrice).ToList();
            }

            switch (pg.sortOrder)
            {
                case "name_desc":
                    dataProduct = dataProduct.OrderByDescending(a => a.NameProduct).ToList();
                    break;
                case "price_desc":
                    dataProduct = dataProduct.OrderByDescending(a => a.Price).ToList();
                    break;
                case "price":
                    dataProduct = dataProduct.OrderBy(a => a.Price).ToList();
                    break;
                default:
                    dataProduct = dataProduct.OrderBy(a => a.NameProduct).ToList();
                    break;
            }
            //int pageSize = 3;
            return View(await PaginatedList<VMProduct>.CreateAsync(dataProduct, pg.pageNumber ?? 1, pg.pageSize?? 3));
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

        public async Task<JsonResult> GetVariantByCategory(int idcategory)
        {
            List<VMVariant> ListVariant = await variant_service.GetVariantByCategory(idcategory);
            return Json(ListVariant);
        }
        public async Task<IActionResult> searchPage()
        {
            return PartialView();
        }

        #endregion

    }
}
