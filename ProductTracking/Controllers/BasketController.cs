using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Entities.Concrete;
using Tracking.Entities.Dtos.Basket;
using Tractking.Business.Interfaces;

namespace ProductTracking.Controllers
{
    public class BasketController : Controller
    {
        private readonly ILogger<BasketController> _logger;
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;

        public BasketController(IMapper mapper, ILogger<BasketController> logger, IBasketService basketService)
        {
            _mapper = mapper;
            _logger = logger;
            _basketService = basketService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(BasketAddDto basketAddDto)
        {
            await _basketService.AddAsync(_mapper.Map<Basket>(basketAddDto));
            return Created("", basketAddDto);
        }

        [HttpPut]
        public async Task<IActionResult> Update(BasketUpdateDto basketUpdateDto)
        {
            var currentBasket = await _basketService.GetByIdAsync(basketUpdateDto.Id);
            if (currentBasket != null)
            {
                await _basketService.UpdateAsync(_mapper.Map<Basket>(basketUpdateDto));
                return Ok();
            }
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _basketService.RemoveAsync(new Basket() { Id = id });
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int Id)
        {
            var basket = await _basketService.GetByIdAsync(Id);
            return Ok(basket);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var baskets = await _basketService.GetAllAsync();
            return Ok(baskets);
        }


    }
}
