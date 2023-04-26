using System;
using System.Collections.Generic;

namespace XPOS_API.Models
{
    public partial class TblVariant
    {
        public int Id { get; set; }
        public int IdCategory { get; set; }
        public string NameVariant { get; set; } = null!;
        public string? Description { get; set; }
        public bool? IsDelete { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
