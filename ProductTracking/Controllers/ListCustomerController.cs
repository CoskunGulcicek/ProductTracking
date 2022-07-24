using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.DataAccess.Concrete.EntityFrameworkCore.Context;
using Tracking.Entities.Concrete;
using Tracking.Entities.Dtos.ListCustomer;
using Tractking.Business.Interfaces;

namespace ProductTracking.Controllers
{
    public class ListCustomerController : Controller
    {
        private readonly ILogger<ListCustomerController> _logger;
        private readonly IListCustomerService _listCustomerService;
        private readonly IMapper _mapper;

        public ListCustomerController(IMapper mapper, ILogger<ListCustomerController> logger, IListCustomerService listCustomerService)
        {
            _mapper = mapper;
            _logger = logger;
            _listCustomerService = listCustomerService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] List<ListCustomerAddDto> listCustomerAddDtos)
        {
            if (listCustomerAddDtos.Count > 0)
            {
                foreach (var item in listCustomerAddDtos)
                {
                    if ((item.CustomerId != 0 && item.CustomerId > 0) && (item.ListId != 0 && item.ListId > 0))
                    {
                        ListCustomerAddDto cp = new ListCustomerAddDto();
                        cp.CustomerId = item.CustomerId;
                        cp.ListId = item.ListId;
                        await _listCustomerService.AddAsync(_mapper.Map<ListCustomer>(cp));
                    }
                }
            }
            return Created("", listCustomerAddDtos);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _listCustomerService.RemoveAsync(new ListCustomer() { Id = id });
            return Json("ok");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFromlist(int id,int listId)
        {
            using var Context = new TrackingContext();
            int listcustomerId = Context.ListCustomers.Where(x => x.CustomerId == id && x.ListId == listId).Select(x => x.Id).FirstOrDefault();
            if (listcustomerId.ToString() != null)
            {
                await _listCustomerService.RemoveAsync(new ListCustomer() { Id = listcustomerId });
            }
            return Json("ok");
        }
    }
}
