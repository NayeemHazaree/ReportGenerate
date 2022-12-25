using Microsoft.AspNetCore.Mvc.Rendering;
using ReportGenerate.Models.Models;
using ReportGenerate.Models.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerate.DataAccess.Repository.IRepository
{
    public interface IOrderRepository
    {
        public Task<IEnumerable<SelectListItem>> ProductDropDownList();
        public Task<Product> GetProductById(Guid Id);
        public Task<List<string>> SaveOrderList(List<string> ProductId);
        public Task<ProdOrderVM> GetOrderListById(Guid id);
    }
}
