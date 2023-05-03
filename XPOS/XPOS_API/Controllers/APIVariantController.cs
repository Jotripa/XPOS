using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XPOS_API.Models;
using XPOS_ViewModels;

namespace XPOS_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class APIVariantController : ControllerBase
    {
        private readonly XPOS_315Context db;
        private VMRespons respons = new VMRespons();

        public APIVariantController(XPOS_315Context _db)
        {
            this.db = _db;
        }

        [HttpGet("GetAllVariant")]
        public List<VMVariant> GetAllVariant()
        {
            List<VMVariant> dataVariant = new List<VMVariant>();
            dataVariant = (from v in db.TblVariants
                           join c in db.TblCategories
                           on v.IdCategory equals c.Id
                           where v.IsDelete == false
                           select new VMVariant
                           {
                               Id = v.Id,
                               NameVariant = v.NameVariant,
                               Description = v.Description,

                               IdCategory = v.IdCategory,
                               NameCategory = c.NameCategory,

                               IsDelete = v.IsDelete
                           }).ToList();
            return dataVariant;
        }
        [HttpGet("GetVariantByCategory/{idcategory}")]
        public List<VMVariant> GetDataByCategory(int idcategory)
        {
            List<VMVariant> dataList = (from v in db.TblVariants
                                        join c in db.TblCategories
                                        on v.IdCategory equals c.Id
                                        where v.IdCategory == idcategory && v.IsDelete ==false
                                        select new VMVariant
                                        {
                                            Id = v.Id,
                                            NameVariant = v.NameVariant,
                                            Description = v.Description,

                                            IsDelete = v.IsDelete,
                                            CreateBy = v.CreateBy,
                                            CreateDate = v.CreateDate,

                                            UpdateBy = v.UpdateBy,
                                            UpdateDate = v.UpdateDate,

                                            IdCategory = v.IdCategory,
                                            NameCategory = c.NameCategory
                                        }).ToList();
            return dataList;
        }

        [HttpGet("GetById/{id}")]
        public VMVariant GetById(int id)
        {
            VMVariant dataVariant = new VMVariant();
            dataVariant = (from v in db.TblVariants
                           join c in db.TblCategories
                           on v.IdCategory equals c.Id
                           where v.Id == id && v.IsDelete == false
                           select new VMVariant
                           {
                               Id = v.Id,
                               NameVariant = v.NameVariant,
                               Description = v.Description,

                               IsDelete = v.IsDelete,
                               CreateBy = v.CreateBy,
                               CreateDate = v.CreateDate,

                               UpdateBy = v.UpdateBy,
                               UpdateDate = v.UpdateDate,

                               IdCategory = v.IdCategory,
                               NameCategory = c.NameCategory
                           }).FirstOrDefault();
            return dataVariant;
        }
        [HttpPost("PostVariant")]
        public VMRespons PostVariant(TblVariant data)
        {
            data.IsDelete = false;
            data.CreateDate = DateTime.Now;
            try
            {
                db.Add(data);
                db.SaveChanges();
                respons.Message = "Data Success Saved";
            }
            catch (Exception ex)
            {
                respons.Success = false;
                respons.Message = "Failed Saved" + ex.Message;
            }
            return respons;
        }
        [HttpPut("PutVariant")]
        public VMRespons PutVariant(TblVariant data)
        {
            TblVariant dataOld = db.TblVariants.Where(a => a.Id == data.Id).FirstOrDefault();
            if (dataOld != null)
            {
                dataOld.NameVariant = data.NameVariant;
                dataOld.Description = data.Description;
                dataOld.IdCategory = data.IdCategory;
                dataOld.UpdateBy = data.UpdateBy;
                dataOld.UpdateDate = DateTime.Now;

                try
                {
                    db.Update(dataOld);
                    db.SaveChanges(true);
                    respons.Message = "Data Success Updated";
                }
                catch (Exception ex)
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
        [HttpDelete("DeleteVariant/{id}")]
        public VMRespons DeleteVariant(int id)
        {
            TblVariant data = db.TblVariants.Where(a => a.Id == id).FirstOrDefault();
            if (data != null)
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
                    respons.Message = "Data failed deleted" + ex.Message;
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
