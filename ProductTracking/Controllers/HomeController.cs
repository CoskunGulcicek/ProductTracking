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
using Tracking.DataAccess.Concrete.EntityFrameworkCore.Context;
using Tracking.Entities.Concrete;
using Tractking.Business.Interfaces;

namespace ProductTracking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;
        private readonly IListService _listService;
        private readonly ICustomerProductService _customerProductService;
        public HomeController(ILogger<HomeController> logger, IProductService productService, ICustomerService customerService, IListService listService, ICustomerProductService customerProductService)
        {
            _logger = logger;
            _productService = productService;
            _customerService = customerService;
            _listService = listService;
            _customerProductService = customerProductService;
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
                if (currentName == loginModel.UserName && currentPassword == loginModel.Password)
                {
                    HttpContext.Session.SetString("username", loginModel.UserName);
                    return RedirectToAction("Index");
                }
                return RedirectToAction("LogIn");
            }
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            var customers = await _customerService.GetAllAsync();
            var lists = await _listService.GetAllAsync();

            ViewBag.Products = products.OrderBy(x => x.Name).ToList();
            ViewBag.Customers = customers.OrderBy(x => x.Name).ToList();
            ViewBag.Lists = lists.OrderBy(x => x.Name).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] List<CalculationModel> calculationModel)
        {
            using var Context = new TrackingContext();
            if (calculationModel.Count > 0)
            {
                foreach (var currentCal in calculationModel)
                {
                    int deletedId = Context.CustomerProducts.Where(x => x.CustomerId == currentCal.cusId && x.ProductId == currentCal.prodId).Select(x => x.Id).FirstOrDefault();
                    if (deletedId != null && deletedId != 0)
                    {
                        await _customerProductService.RemoveAsync(new CustomerProduct() { Id = deletedId });
                    }
                }
                foreach (var currentCal in calculationModel)
                {
                    CustomerProduct goToDb = new CustomerProduct();
                    goToDb.CustomerId = currentCal.cusId;
                    goToDb.ProductId = currentCal.prodId;
                    goToDb.Quantity = currentCal.Quantity;
                    await _customerProductService.AddAsync(goToDb);
                }
            }


            //burada ekrana basmak için göndereceği jsonu hazırlıyor
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


        
        public IActionResult ToPrintCalculate([FromBody] List<CalculationModel> calculationModel)
        {
            using var Context = new TrackingContext();
            if (calculationModel.Count > 0)
            {
                foreach (var currentCal in calculationModel)
                {
                    int deletedId = Context.CustomerProducts.Where(x => x.CustomerId == currentCal.cusId && x.ProductId == currentCal.prodId).Select(x => x.Id).FirstOrDefault();
                    if (deletedId != null && deletedId != 0)
                    {
                         _customerProductService.RemoveAsync(new CustomerProduct() { Id = deletedId });
                    }
                }
                foreach (var currentCal in calculationModel)
                {
                    CustomerProduct goToDb = new CustomerProduct();
                    goToDb.CustomerId = currentCal.cusId;
                    goToDb.ProductId = currentCal.prodId;
                    goToDb.Quantity = currentCal.Quantity;
                     _customerProductService.AddAsync(goToDb);
                }
            }


            //burada ekrana basmak için göndereceği jsonu hazırlıyor
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
            var element = newList.Where(x => x.Quantity > 0).OrderBy(x => x.Name).ToList();
            CalculateModelStatic.userChatNotifications.Clear();
            foreach (var ii in element)
            {
                CalculateModelStatic.userChatNotifications.Add(ii);
            }
            return Ok();
        }

        public IActionResult ToPrintCalculates()
        {
            var abc = CalculateModelStatic.userChatNotifications;
            return View(abc);
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
