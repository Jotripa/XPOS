using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPOS_ViewModels
{
    public class VMUserCostumer
    {
        //user
        public int IdUser { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int IdRole { get; set; }

        //customer
        public int IdCustomer { get; set; }
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
