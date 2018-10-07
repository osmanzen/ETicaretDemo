using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Model.Models.ViewModel
{
    public class VMProduct
    {
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string PicturePath { get; set; }

    }
}
