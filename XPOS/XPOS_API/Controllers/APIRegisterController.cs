using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XPOS_API.Models;
using XPOS_ViewModels;

namespace XPOS_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class APIRegisterController : ControllerBase
    {
        private readonly XPOS_315Context db;
        private VMRespons respons = new VMRespons();

        public APIRegisterController(XPOS_315Context _db)
        {
            this.db = _db;
        }

        [HttpGet("GetRegister")]
        public List<TblRole> GetAllRole()
        {
            List<TblRole> ListRole = db.TblRoles.Where(a => a.IsDelete == false).ToList();
            return ListRole;
        }

        [HttpGet("CheckEmailDuplicate/{email}")]
        public bool CheckEmailDuplicate(string email)
        {
            bool IsDuplicate = false;
            TblUser data = db.TblUsers.Where(a => a.Email == email).FirstOrDefault();
            if(data != null)
            {
                IsDuplicate = true;
            }

            return IsDuplicate;
        }

        [HttpPost("PostRegister")]
        public VMRespons PostRegister(VMUserCostumer data)
        {
            TblUser user = new TblUser();
            TblCustomer cust = new TblCustomer();
            //Add User
            try
            {
                user.CreateDate = data.CreateDate;
                user.CreateBy = data.CreateBy;
                user.Email = data.Email;
                user.Password = data.Password;
                user.IsDelete = data.IsDelete;
                user.IdRole = data.IdRole;
                user.UpdateDate = data.UpdateDate;
                user.UpdateBy = data.UpdateBy;
                db.Add(user);
                db.SaveChanges();
                respons.Message = "Data saved success";
            }
            catch (Exception e)
            {
                respons.Success = false;
                respons.Message = e.Message;
            }
            //Add Customer
            try
            {
                cust.IdUser = user.Id;
                cust.Address = data.Address;
                cust.NameCustomer = data.NameCustomer;
                cust.Phone = data.Phone;
                cust.IsDelete = data.IsDelete;
                cust.CreateBy = data.CreateBy;
                cust.CreateDate = data.CreateDate;
                cust.UpdateBy = data.UpdateBy;
                cust.UpdateDate = data.UpdateDate;
                db.Add(cust);
                db.SaveChanges();
                respons.Message = "Data saved success";
            }
            catch (Exception e)
            {
                respons.Success = false;
                respons.Message = e.Message;
            }
            return respons;
        }

        [HttpGet("CheckLogin/{email}/{password}")]
        public VMUserCostumer CheckLogin(string email, string password)
        {
            VMUserCostumer data = (from u in db.TblUsers
                                   join c in db.TblCustomers
                                   on u.Id equals c.IdUser
                                   where u.Email == email && u.Password == password
                                   select new VMUserCostumer
                                   {
                                       IdUser = u.Id,
                                       Email = u.Email,
                                       NameCustomer = c.NameCustomer,
                                       IdRole = u.IdRole,
                                       IdCustomer = c.Id
                                   }).FirstOrDefault();
            return data;
        }
    }
}
