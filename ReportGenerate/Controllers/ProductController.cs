using Microsoft.AspNetCore.Mvc;
using ReportGenerate.DataAccess.Data;
using ReportGenerate.DataAccess.Repository;
using ReportGenerate.DataAccess.Repository.IRepository;
using ReportGenerate.Models.Models;

namespace ReportGenerate.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductReposotory _productRepository;
        public ProductController(IProductReposotory productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        #region ProductDataTable
        [HttpGet]
        public async Task<IActionResult> GetProductList()
        {
            var products = await _productRepository.GetAllProductList();
            return Json(new { products });
        }
        #endregion

        #region SaveProduct
        [HttpPost]
        public async Task <IActionResult> UpsertProduct(Product product)
        {
            string result = "";
            if(product.Id == Guid.Empty)
            {
                result = await _productRepository.UpsertProduct(product);
            }
            else
            {
                result = await _productRepository.UpsertProduct(product);
            }
            return Json(new { result });
        }
        #endregion
    }
}
