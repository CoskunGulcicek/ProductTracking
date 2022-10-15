using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Entities.Concrete;
using Tracking.Entities.Dtos.CustomerProduct;
using Tractking.Business.Interfaces;

namespace ProductTracking.Controllers
{
    public class CustomerProductController : Controller
    {
        private readonly ILogger<CustomerProductController> _logger;
        private readonly ICustomerProductService _customerProductService;
        private readonly IMapper _mapper;

        public CustomerProductController(IMapper mapper, ILogger<CustomerProductController> logger, ICustomerProductService customerProductService)
        {
            _mapper = mapper;
            _logger = logger;
            _customerProductService = customerProductService;
        }

        public async Task<IActionResult> Add(CustomerProductAddDto customerProductAddDto)
        {
            await _customerProductService.AddAsync(_mapper.Map<CustomerProduct>(customerProductAddDto));
            return Created("", customerProductAddDto);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _customerProductService.RemoveAsync(new CustomerProduct() { Id = id });
            return Json("ok");
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int Id)
        {
            var customerProduct = await _customerProductService.GetByIdAsync(Id);
            return Ok(customerProduct);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var customerProducts = await _customerProductService.GetAllAsync();
            return Ok(customerProducts);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomersJsonAsync()
        {
            var customerProducts = await _customerProductService.GetAllAsync();
            return Json(new { data = customerProducts});
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAllDoubleProducts()
        {
            var productlar = await _customerProductService.GetAllAsync();
            List<CustomerProduct> distinctPeople = productlar
              .GroupBy(p => new { p.CustomerId, p.ProductId})
              .Select(g => g.First())
              .ToList();

            foreach (var silinecekData in productlar)
            {
                await _customerProductService.RemoveAsync(new CustomerProduct { Id = silinecekData.Id });
            }

            foreach (var eklenecekData in distinctPeople)
            {
                CustomerProduct yeni = new CustomerProduct();
                yeni.CustomerId = eklenecekData.CustomerId;
                yeni.ProductId = eklenecekData.ProductId;
                yeni.ProductName= eklenecekData.ProductName;
                yeni.Quantity = eklenecekData.Quantity;
                await _customerProductService.AddAsync(yeni);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
