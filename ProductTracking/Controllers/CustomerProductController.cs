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
    }
}
