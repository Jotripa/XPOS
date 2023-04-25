using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XPOS_API.Models;
using XPOS_ViewModels;

namespace XPOS_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class APICategoryController : ControllerBase
    {

        private readonly XPOS_315Context db;
        private VMRespons respons = new VMRespons();


        public APICategoryController(XPOS_315Context _db) 
        {
            this.db = _db;
        }

        [HttpGet("GetAllCategory")]
        public List<TblCategory> GetAllCategory()
        {
            List<TblCategory> dataCategory = new List<TblCategory>();

            dataCategory = db.TblCategories.Where(a => a.IsDelete == false).ToList();

            return dataCategory;
        }

        [HttpGet("GetById/{id}")]
        public TblCategory GetById(int id)
        {
            TblCategory dataCategory = new TblCategory();

            dataCategory = db.TblCategories.Where(a => a.Id == id).FirstOrDefault();

            return dataCategory;
        }

        //Post
        [HttpPost("PostCategory")]
        public VMRespons PostCategory(TblCategory data)
        {
            data.IsDelete = false;
            data.CreateDate = DateTime.Now;
            try
            {
                db.Add(data);
                db.SaveChanges();

                respons.Message = "Data success saved";
            }
            catch (Exception e)
            {
                respons.Success = false;
                respons.Message = "Failed saved : " + e.Message;
            }
            return respons;
        }
        [HttpPut("PutCategory")]
        //find old data in database that want to be updated
        public VMRespons PutCategory(TblCategory data)
        {
            TblCategory dataOld = db.TblCategories.Where(a => a.Id == data.Id).FirstOrDefault();

            //if data not null
            if (dataOld != null)
            {
                dataOld.NameCategory = data.NameCategory;
                dataOld.Description = data.Description;

                dataOld.UpdateDate = DateTime.Now;
                dataOld.UpdateBy = data.UpdateBy;
                //data updated
                try
                {
                    db.Update(dataOld);
                    db.SaveChanges();

                    respons.Message = "Data success updated";
                }
                //if error
                catch (Exception e)
                {
                    respons.Success = false;
                    respons.Message = "Update failed: " + e.Message;
                }
            }
            //if data null
            else
            {
                respons.Success = false;
                respons.Message = "Data not found";
            }



            return respons;
        }
        [HttpDelete("DeleteCategory/{id}")]
        public VMRespons DeleteCategory(int id)
        {
            TblCategory data = db.TblCategories.Where(a => a.Id == id).FirstOrDefault();
            
            if(data != null)
            {
                data.IsDelete = true;
                try
                {
                    db.Update(data);
                    db.SaveChanges();

                    respons.Message = "Data success deleted";
                }
                catch(Exception e)
                {
                    respons.Success = false;
                    respons.Message = "Delete failed" + e.Message;
                }
            }
            else
            {
                respons.Success = false;
                respons.Message = "Data not found";
            }


            return respons;
        }
    }
}
