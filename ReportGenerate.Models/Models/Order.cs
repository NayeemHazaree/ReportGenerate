using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerate.Models.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        //public List<Order>? orderList;
        [NotMapped]
        public IEnumerable<SelectListItem>? Prodcucts { get; set; }
    }
}
