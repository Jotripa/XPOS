using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XPOS_API.Models;
using XPOS_ViewModels;

namespace XPOS_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class APIProductController : ControllerBase
    {
        private readonly XPOS_315Context db;
        private VMRespons respons = new VMRespons();

        public APIProductController(XPOS_315Context _db) 
        {
            this.db = _db;
        }

        [HttpGet("GetAllProduct")]
        public List<VMProduct> GetAllProduct()
        {
            List<VMProduct> dataProduct = new List<VMProduct>();
            dataProduct = (from p in db.TblProducts
                           join v in db.TblVariants
                           on p.IdVariant equals v.Id
                           join c in db.TblCategories
                           on v.IdCategory equals c.Id
                           where p.IsDelete == false
                           select new VMProduct
                           {
                               Id = p.Id,
                               NameProduct = p.NameProduct,
                               Price = p.Price,
                               Stock = p.Stock,

                               IdVariant = v.Id,
                               NameVariant = v.NameVariant,

                               IdCategory = v.IdCategory,
                               NameCategory = c.NameCategory,

                               IsDelete = p.IsDelete
                           }).ToList(); 
            return dataProduct;
        }
        [HttpGet("GetById/{id}")]
        public VMProduct GetById(int id)
        {
            VMProduct dataProduct = new VMProduct();
            dataProduct = (from p in db.TblProducts
                           join v in db.TblVariants
                           on p.IdVariant equals v.Id
                           join c in db.TblCategories
                           on v.IdCategory equals c.Id
                           where p.Id == id && p.IsDelete!= false
                           select new VMProduct
                           {
                               Id = p.Id,
                               NameProduct = p.NameProduct,
                               Price = p.Price,
                               Stock = p.Stock,

                               IsDelete = p.IsDelete,
                               CreateBy = p.CreateBy,
                               CreateDate = p.CreateDate,
                               
                               UpdateBy = p.UpdateBy,
                               UpdateDate = p.UpdateDate,

                               IdVariant = v.Id,
                               NameVariant = v.NameVariant,

                               IdCategory = v.IdCategory,
                               NameCategory = c.NameCategory
                           }).FirstOrDefault();
            return dataProduct;
        }
        [HttpPost("PostProduct")]
        public VMRespons PostProduct(TblProduct data)
        {
            data.IsDelete = false;
            data.CreateDate = DateTime.Now;

            try
            {
                db.Add(data);
                db.SaveChanges();
                respons.Message = "Data saved success"; 
            }
            catch (Exception ex)    
            {
                respons.Success = false;
                respons.Message = "Failed Saved" + ex.Message;
            }

            return respons;
        }
        [HttpPut("PutProduct")]
        public VMRespons PutProduct(TblProduct data)
        {
            TblProduct dataOld = db.TblProducts.Where(a => a.Id == data.Id).FirstOrDefault();

            if(dataOld != null)
            {
                dataOld.NameProduct = data.NameProduct;
                dataOld.IdVariant = data.IdVariant;
                dataOld.Price = data.Price;
                dataOld.Image = data.Image;
                dataOld.Stock = data.Stock;
                dataOld.UpdateBy = data.UpdateBy;
                dataOld.UpdateDate = data.UpdateDate;
                try
                {
                    db.Update(dataOld);
                    db.SaveChanges();
                    respons.Message = "Data saved success";
                }
                catch(Exception ex)
                {
                    respons.Success = false;
                    respons.Message = "Data failed updated" + ex.Message;
                }
            }
            else
            {
                respons.Success = false;
                respons.Message = "Data not found";
            }
            return respons;
            
        }
        [HttpDelete("DeleteProduct")]
        public VMRespons DeleteProduct(int id)
        {
            TblProduct data = db.TblProducts.Where(a => a.Id == id).FirstOrDefault();  
            if(data != null)
            {
                data.IsDelete = true;
                try
                {
                    db.Update(data);
                    db.SaveChanges();
                    respons.Message = "Data success deleted";
                }
                catch (Exception ex) 
                {
                    respons.Success = false;
                    respons.Message = "Data failed Deleted" + ex.Message;
                }
            }
            return respons;
        }
        
    }
}
