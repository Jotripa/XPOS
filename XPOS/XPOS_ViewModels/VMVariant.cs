using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPOS_ViewModels
{
    public class VMVariant
    {
        public int Id { get; set; }
        public int IdCategory { get; set; }
        public string NameCategory { get; set; }
        public string NameVariant { get; set; } = null!;
        public string? Description { get; set; }
        public bool? IsDelete { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
