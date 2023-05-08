using System;
using System.Collections.Generic;

namespace XPOS_API.Models
{
    public partial class TblOrderDetail
    {
        public int Id { get; set; }
        public int IdHeader { get; set; }
        public int IdProduct { get; set; }
        public int Qty { get; set; }
        public decimal SubTotal { get; set; }
        public bool? IsDelete { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
