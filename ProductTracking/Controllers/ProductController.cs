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

        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(Product productAdd)
        {
            await _productService.AddAsync(productAdd);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductAddDto productAddDto)
        {
            await _productService.AddAsync(_mapper.Map<Product>(productAddDto));
            return Created("", productAddDto);
        }

        public async Task<IActionResult> Update(int id)
        {
            var currentProduct = await _productService.GetByIdAsync(id);
            if (currentProduct != null)
            {
                return View(currentProduct);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            var currentProduct = await _productService.GetByIdAsync(productUpdateDto.Id);
            if (currentProduct != null)
            {
                await _productService.UpdateAsync(_mapper.Map<Product>(productUpdateDto));
                return RedirectToAction("Index");
            }
            return NoContent();
        }

        [HttpPost]
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

        public async Task<IActionResult> GetAllProductsJsonAsync()
        {
            var products = await _productService.GetAllAsync();
            return Json(products);
        }

    }
}
