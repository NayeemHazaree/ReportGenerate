using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReportGenerate.DataAccess.Data;
using ReportGenerate.DataAccess.Repository.IRepository;
using ReportGenerate.Models.Models;
using ReportGenerate.Models.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerate.DataAccess.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ProdOrderVM> GetOrderListById(Guid id)
        {
            var orderedProd = new ProdOrderVM();
            List<Order> OrderItemList = new();
            var order = await _db.Order.ToListAsync();
            var products = await _db.Product.ToListAsync();
            var InsertProducts = new List<Product>();
            foreach (var item in order)
            {
                if(item.OrderId == id)
                {
                    orderedProd.Order = item;
                    OrderItemList.Add(item);
                }
            }
            foreach (var prodItem in OrderItemList)
            {
                var product = products.FirstOrDefault(x => x.Id == prodItem.ProductId);
                if(product != null)
                {
                    InsertProducts.Add(product);
                }
            }
            orderedProd.Product = InsertProducts;
            return orderedProd;
        }

        public async Task<Product> GetProductById(Guid Id)
        {
            Product product = await _db.Product.FirstOrDefaultAsync(x => x.Id == Id);
            return product;

        }

        public async Task<IEnumerable<SelectListItem>> ProductDropDownList()
        {
            return _db.Product.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public async Task<List<string>> SaveOrderList(List<string> ProductId)
        {
            List<string> list = new List<string>();
            if(ProductId != null)
            {
                var OrderID = Guid.NewGuid();
                Order order = new();
                foreach (var item in ProductId)
                {
                    order.Id = Guid.NewGuid();
                    order.ProductId = new Guid(item);
                    order.OrderId = OrderID;
                    await _db.Order.AddAsync(order);
                    await _db.SaveChangesAsync();
                }
                list.Add("success");
                list.Add(OrderID.ToString());
            }
            else
            {
                list.Add("Failed");
            }
            return list;
        }
    }
}
