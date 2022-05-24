using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Entities.Concrete;
using Tracking.Entities.Dtos.Product;
using Tractking.Business.Interfaces;

namespace ProductTracking.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IMapper mapper, ILogger<ProductController> logger, IProductService productService)
        {
            _mapper = mapper;
            _logger = logger;
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductAddDto productAddDto)
        {
            await _productService.AddAsync(_mapper.Map<Product>(productAddDto));
            return Created("", productAddDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            var currentProduct = await _productService.GetByIdAsync(productUpdateDto.Id);
            if (currentProduct != null)
            {
                await _productService.UpdateAsync(_mapper.Map<Product>(productUpdateDto));
                return Ok();
            }
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.RemoveAsync(new Product() { Id = id });
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int Id)
        {
            var product = await _productService.GetByIdAsync(Id);
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

    }
}
