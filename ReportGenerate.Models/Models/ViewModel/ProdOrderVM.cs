using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerate.Models.Models.ViewModel
{
    public class ProdOrderVM
    {
        public Order? Order { get; set; }
        public List<Product> Product { get; set; }
    }
}
