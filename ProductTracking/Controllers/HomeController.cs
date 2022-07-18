using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly IListService _listService;
        public HomeController(ILogger<HomeController> logger, IProductService productService, ICustomerService customerService, IListService listService)
        {
            _logger = logger;
            _productService = productService;
            _customerService = customerService;
            _listService = listService;
        }


        public async Task<IActionResult> LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginModel loginModel)
        {
            string currentName = "Kenan Baba";
            string currentPassword = "998877";
            if ((loginModel.UserName == null || loginModel.Password == null || loginModel.Password2 == null) && (loginModel.UserName == "" || loginModel.Password == "" || loginModel.Password2 == ""))
            {
                return RedirectToAction("LogIn");

            }
            else
            {
                if(currentName == loginModel.UserName && currentPassword == loginModel.Password)
                {
                    HttpContext.Session.SetString("username", loginModel.UserName);
                    return RedirectToAction("Index");
                }
                return RedirectToAction("LogIn");
            }
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Products = await _productService.GetAllAsync();
            ViewBag.Customers = await _customerService.GetAllAsync();
            ViewBag.Lists = await _listService.GetAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] List<CalculationModel> calculationModel)
        {
            List<CalculationModel> newList = new List<CalculationModel>();
            foreach (var item in calculationModel)
            {
                if (item.Name != null && item.Quantity.ToString() != null && item.Type != null)
                {
                    var varMı = newList.Where(x => x.Name == item.Name && x.Type == item.Type).FirstOrDefault();
                    if (varMı == null)
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
                            decimal itemQ = item.Quantity.ToString() == null ? 0 : item.Quantity;
                            newList.Where(x => x.Name == item.Name && x.Type == item.Type).Select(x => x.Quantity = (x.Quantity + itemQ)).ToList();
                        }
                    }
                }
            }
            return Json(newList.Where(x => x.Quantity > 0).OrderBy(x => x.Name));
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
