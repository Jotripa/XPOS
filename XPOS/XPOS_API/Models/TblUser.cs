using System;
using System.Collections.Generic;

namespace XPOS_API.Models
{
    public partial class TblUser
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int IdRole { get; set; }
        public bool IsDelete { get; set; }
        public int CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
