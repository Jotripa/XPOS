using System;
using System.Collections.Generic;

namespace XPOS_API.Models
{
    public partial class TblRole
    {
        public int Id { get; set; }
        public string? RoleName { get; set; }
        public bool? IsDelete { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
