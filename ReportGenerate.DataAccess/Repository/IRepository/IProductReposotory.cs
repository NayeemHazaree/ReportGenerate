using ReportGenerate.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerate.DataAccess.Repository.IRepository
{
    public interface IProductReposotory
    {
        public Task<IEnumerable<Product>> GetAllProductList();
        public Task<string> UpsertProduct(Product product);
    }
}
