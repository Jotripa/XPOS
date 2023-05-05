using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPOS_ViewModels
{
    public class VMSearchPage
    {
       public string sortOrder { get; set; }
       public string searchString { get; set; }
       public string CurrentFilter { get; set; }
       public int? pageNumber { get; set; }
       public int? pageSize { get; set; }
       public decimal? minPrice { get; set; }
       public decimal? maxPrice { get; set;}
    }
}
