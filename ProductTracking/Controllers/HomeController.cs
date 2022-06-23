using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductTracking.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Tractking.Business.Interfaces;

namespace ProductTracking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;
        public HomeController(ILogger<HomeController> logger,IProductService productService, ICustomerService customerService)
        {
            _logger = logger;
            _productService = productService;
            _customerService = customerService;
        }

        public async Task<IActionResult> Index()
        {
            CustomerProductModel customerProductModel = new CustomerProductModel();
            customerProductModel.Products = await _productService.GetAllAsync();
            customerProductModel.Customers = await _customerService.GetAllAsync();
            return View(customerProductModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]List<CalculationModel> calculationModel)
        {
            List<CalculationModel> newList = new List<CalculationModel>();
            foreach (var item in calculationModel)
            {
                var varMı = newList.Where(x => x.Name == item.Name && x.Type == item.Type).FirstOrDefault();
                if (varMı==null)
                {
                    newList.Add(item);
                }
                else
                {
                    var tipeGoreVarMı = newList.Where(x => x.Name == item.Name && x.Type == item.Type).FirstOrDefault();
                    if (tipeGoreVarMı == null)
                    {
                        newList.Add(item);
                    }
                    else
                    {
                        decimal itemQ = item.Quantity == null ? 0 : item.Quantity;
                        newList.Where(x => x.Name == item.Name && x.Type == item.Type).Select(x => x.Quantity = (x.Quantity+itemQ)).ToList();
                    }
                }
            }
            return Json(newList.Where(x=>x.Quantity>0).OrderBy(x => x.Name));
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
