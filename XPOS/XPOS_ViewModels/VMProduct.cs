using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPOS_ViewModels
{
    public class VMProduct
    {
        public int Id { get; set; }
        public string NameProduct { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int IdVariant { get; set; }
        public string NameVariant { get; set; }
        public int IdCategory { get; set; }
        public string NameCategory { get; set; }

        public string? Image { get; set; }
        public bool? IsDelete { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
