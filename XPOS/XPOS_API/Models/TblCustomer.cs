using System;
using System.Collections.Generic;

namespace XPOS_API.Models
{
    public partial class TblCustomer
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string NameCustomer { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public bool IsDelete { get; set; }
        public int CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
