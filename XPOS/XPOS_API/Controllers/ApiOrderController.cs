﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Text;
using System.Globalization;
using XPOS_API.Models;
using XPOS_ViewModels;

namespace XPOS_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiOrderController : ControllerBase
    {
        private readonly XPOS_315Context db;
        private VMRespons respons = new VMRespons();
        public ApiOrderController(XPOS_315Context _db)
        {
            this.db = _db;
        }
        [HttpGet("GetAllOrder")]
        public List<VMOrderHeader> GetAllOrder()
        {
            List<VMOrderHeader> ListOrder = (from h in db.TblOrderHeaders
                                             where h.IsDelete == false
                                             select new VMOrderHeader
                                             {
                                                 Id = h.Id,
                                                 Amount = h.Amount,
                                                 TotalQty = h.TotalQty,
                                                 IsCheckout= h.IsCheckout,
                                                 CodeTransaction = h.CodeTransaction,
                                                 CreateDate= h.CreateDate

                                             }).ToList();
            return ListOrder;
        }

        [HttpPost("SubmitPayment")]
        public VMRespons SubmitPayment(VMOrderHeader dataHeader)
        {
            TblOrderHeader header = new TblOrderHeader();
            header.TotalQty = dataHeader.TotalQty;
            header.Amount = dataHeader.Amount;
            header.IdCustomer = dataHeader.IdCustomer;
            header.IsCheckout = true;
            header.CodeTransaction = CodeGenerate();

            header.CreateBy = dataHeader.CreateBy;
            header.CreateDate = DateTime.Now;
            header.IsDelete = false;

            try
            {
                db.Add(header);
                db.SaveChanges();
                foreach (VMOrderDetail item in dataHeader.ListDetail)
                {
                    TblOrderDetail detail = new TblOrderDetail();
                    detail.IdHeader = header.Id;
                    detail.IdProduct = item.IdProduct;
                    detail.Qty = item.Qty;
                    detail.SubTotal = item.SubTotal;
                    detail.IsDelete = false;
                    detail.CreateBy = dataHeader.CreateBy;
                    detail.CreateDate = DateTime.Now;

                    try
                    {
                        db.Add(detail);
                        db.SaveChanges();
                        TblProduct prod = db.TblProducts
                            .Where(a => a.Id == item.IdProduct)
                            .FirstOrDefault();
                        if (prod != null)
                        {
                            prod.Stock = prod.Stock - item.Qty;
                            try
                            {
                                db.Update(prod);
                                db.SaveChanges(true);
                            }
                            catch (Exception e)
                            {
                                respons.Success = false;
                                respons.Message = e.Message;
                            }

                        }
                    }
                    catch (Exception e)
                    {
                        respons.Success = false;
                        respons.Message= e.Message;
                    }
                }
            }
            catch (Exception e)
            {
                respons.Success = false;
                respons.Message= e.Message;
            }
            return respons;
        }
        [HttpGet("CodeGenerate")]
        public string CodeGenerate()
        {

            string code = $"XPOS-{DateTime.Now.ToString("ddMMyyyy")}-";
            string digit = "";

            TblOrderHeader dataLast = db.TblOrderHeaders
                .OrderByDescending(a => a.CodeTransaction)
                .FirstOrDefault();

            if (dataLast == null)
            {
                digit = "00001";
            }
            else
            {
                string defaultDigit = "00000";
                string codelast = dataLast.CodeTransaction;
                string subCode = codelast.Substring(14);
                int intCode = int.Parse(subCode);

                intCode++;

                int lenCode = intCode.ToString().Length;
                defaultDigit = defaultDigit + intCode.ToString();
                defaultDigit = defaultDigit.Substring(lenCode, 5);

                digit = defaultDigit;
            }
            return code + digit;
        }
    }
}
