using jsreport.AspNetCore;
using jsreport.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NuGet.Protocol;
using ReportGenerate.DataAccess.Data;
using ReportGenerate.DataAccess.Repository.IRepository;
using ReportGenerate.Models.Models;
using ReportGenerate.Models.Models.ViewModel;
using System.Linq.Expressions;

namespace ReportGenerate.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        public IJsReportMVCService JsReportMVCService { get; }
        HttpClient _client;
        public OrderController( IOrderRepository orderRepository, IJsReportMVCService jsReportMVCService)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:44380/Order/");
            _orderRepository = orderRepository;
            JsReportMVCService = jsReportMVCService;
        }
        public async Task<IActionResult> Index()
        {
            Order order = new Order();
            order.Prodcucts = await _orderRepository.ProductDropDownList();
            return View(order);
        }

        #region Get Product By Id
        [HttpPost]
        public async Task<IActionResult> GetProductById(Guid Id)
        {
            Product product = await _orderRepository.GetProductById(Id);
            return Json(new { product });
        }
        #endregion

        #region Save Order
        [HttpPost]
        public async Task<IActionResult> SaveOrder(List<string> ProdId)
        {
            List<string> result = new List<string>();
            if (ProdId != null)
            {
                result = await _orderRepository.SaveOrderList(ProdId);
            }
            return Json(new { result });
        }
        #endregion

        #region Generate Report
        [MiddlewareFilter(typeof(JsReportPipeline))]
        public async Task<IActionResult> GenerateReport(Guid OrderID)
        {
            var httpResponse = await _client.GetAsync("Report?OrderId=" + OrderID);
            if (!httpResponse.IsSuccessStatusCode)
            {
                return NotFound();
            }
            try
            {
                var content = await httpResponse.Content.ReadAsStringAsync();
                content.Normalize();
                ProdOrderVM obj = JsonConvert.DeserializeObject<ProdOrderVM>(content);
                HttpContext.JsReportFeature().Recipe(Recipe.ChromePdf).Configure((r) =>
                {
                    r.Template.Chrome = new Chrome { Landscape = true, MarginRight = "10", MarginLeft = "10" };
                }).OnAfterRender((r) => HttpContext.Response.Headers["Content-Disposition"] = "filename=\"Report.pdf\"");
                return PartialView("_GenerateReport", obj);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        #endregion

        #region
        public async Task<ProdOrderVM> Report(Guid OrderId)
        {
            
            var orderItem = await _orderRepository.GetOrderListById(OrderId);
            
            return orderItem;
        }
        #endregion
    }
}
