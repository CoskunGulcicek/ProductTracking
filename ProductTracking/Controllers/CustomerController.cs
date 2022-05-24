using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Entities.Concrete;
using Tracking.Entities.Dtos.Customer;
using Tractking.Business.Interfaces;

namespace ProductTracking.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(IMapper mapper, ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _mapper = mapper;
            _logger = logger;
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CustomerAddDto customerAddDto)
        {
            await _customerService.AddAsync(_mapper.Map<Customer>(customerAddDto));
            return Created("", customerAddDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CustomerUpdateDto customerUpdateDto)
        {
            var currentCustomer = await _customerService.GetByIdAsync(customerUpdateDto.Id);
            if (currentCustomer != null)
            {
                await _customerService.UpdateAsync(_mapper.Map<Customer>(customerUpdateDto));
                return Ok();
            }
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _customerService.RemoveAsync(new Customer() { Id = id });
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int Id)
        {
            var customer = await _customerService.GetByIdAsync(Id);
            return Ok(customer);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var customers = await _customerService.GetAllAsync();
            return Ok(customers);
        }

    }
}
