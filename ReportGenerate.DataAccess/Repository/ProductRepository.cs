using Microsoft.EntityFrameworkCore;
using ReportGenerate.DataAccess.Data;
using ReportGenerate.DataAccess.Repository.IRepository;
using ReportGenerate.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerate.DataAccess.Repository
{
    public class ProductRepository : IProductReposotory
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<string> UpsertProduct(Product product)
        {
            string res = "";
            if(product.Id == Guid.Empty)
            {
                await _db.Product.AddAsync(product);
                await _db.SaveChangesAsync();
                res = "Product Added";
            }
            else
            {
                _db.Product.Update(product);
                await _db.SaveChangesAsync();
                res = "Product Udated";
            }
            return res;
        }

        public async Task<IEnumerable<Product>> GetAllProductList()
        {
            return await _db.Product.ToListAsync();
        }
    }
}
