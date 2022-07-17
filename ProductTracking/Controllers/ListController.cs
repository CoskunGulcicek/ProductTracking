using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductTracking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Entities.Concrete;
using Tracking.Entities.Dtos.List;
using Tractking.Business.Interfaces;

namespace ProductTracking.Controllers
{
    public class ListController : Controller
    {
        private readonly ILogger<ListController> _logger;
        private readonly IListService _listService;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public ListController(IMapper mapper, ILogger<ListController> logger, IListService listService, ICustomerService customerService)
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
            listAndCustomerModel.customers = await _customerService.GetAllAsync();
            return View(listAndCustomerModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(List listAdd)
        {
            await _listService.AddAsync(listAdd);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Add(ListAddDto listAddDto)
        {
            await _listService.AddAsync(_mapper.Map<List>(listAddDto));
            return Created("", listAddDto);
        }

        public async Task<IActionResult> Update(int id)
        {
            var currentList = await _listService.GetByIdAsync(id);
            if (currentList != null)
            {
                return View(currentList);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(ListUpdateDto listUpdateDto)
        {
            var currentList = await _listService.GetByIdAsync(listUpdateDto.Id);
            if (currentList != null)
            {
                await _listService.UpdateAsync(_mapper.Map<List>(listUpdateDto));
                return RedirectToAction("Index");
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _listService.RemoveAsync(new List() { Id = id });
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int Id)
        {
            var list = await _listService.GetByIdAsync(Id);
            return Ok(list);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var lists = await _listService.GetAllAsync();
            return Ok(lists);
        }

        public async Task<IActionResult> GetAllListsJsonAsync()
        {
            var lists = await _listService.GetAllAsync();
            return Json(lists);
        }

    }
}
