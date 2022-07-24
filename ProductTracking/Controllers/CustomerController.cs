using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductTracking.Models;
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
        private readonly IListService _listService;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(IMapper mapper, ILogger<CustomerController> logger, IListService listService, ICustomerService customerService)
        {
            _mapper = mapper;
            _logger = logger;
            _customerService = customerService;
            _listService = listService;
        }

        public async Task<IActionResult> Index()
        {
            ListAndCustomerModel listAndCustomerModel = new ListAndCustomerModel();
            listAndCustomerModel.lists = await _listService.GetAllAsync();
            listAndCustomerModel.customers= await _customerService.GetAllAsync();
            return View(listAndCustomerModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(CustomerAddDto customerAddDto)
        {
            await _customerService.AddAsync(_mapper.Map<Customer>(customerAddDto));
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Add(CustomerAddDto customerAddDto)
        {
            await _customerService.AddAsync(_mapper.Map<Customer>(customerAddDto));
            return Created("", customerAddDto);
        }

        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Lists = await _listService.GetAllAsync();
            var currentCustomer = await _customerService.GetByIdAsync(id);
            if (currentCustomer != null)
            {
                return View(currentCustomer);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(CustomerUpdateDto customerUpdateDto)
        {

            ViewBag.Lists = await _listService.GetAllAsync();
            var currentCustomer = await _customerService.GetByIdAsync(customerUpdateDto.Id);
            if (currentCustomer != null)
            {
                await _customerService.UpdateAsync(_mapper.Map<Customer>(customerUpdateDto));
                return RedirectToAction("Index");
            }
            return NoContent();
        }



        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _customerService.RemoveAsync(new Customer() { Id = id });
            return Json("ok");
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

        [HttpGet]
        public async Task<IActionResult> GetAllCustomersJsonAsync()
        {
            var customers = await _customerService.GetAllAsync();
            return Json(new { data = customers });
        }

/*        [HttpGet]
        public async Task<IActionResult> GetByListId(int Id)
        {
            var customers = await _customerService.GetByListId(Id);
            return Json(customers);
        }*/

        [HttpGet]
        public async Task<IActionResult> GetByListId(int Id)
        {
            var customers = await _customerService.GetByLstCusIds(Id);
            return Json(customers);
        }
    }
}
