using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductTracking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Entities.Concrete;
using Tracking.Entities.Dtos.CustomerProduct;
using Tracking.Entities.Dtos.Product;
using Tractking.Business.Interfaces;

namespace ProductTracking.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        private readonly ICustomerProductService _customerProductService;
        private readonly IMapper _mapper;

        public ProductController(IMapper mapper, ILogger<ProductController> logger, IProductService productService, ICustomerProductService customerProductService)
        {
            _mapper = mapper;
            _logger = logger;
            _productService = productService;
            _customerProductService = customerProductService;
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

        [HttpPost]
        public async Task<IActionResult> AddProductToCustomers([FromBody]List<AddProductToCustomer> customerProduct)
        {
            if(customerProduct.Count > 0)
            {
                foreach (var item in customerProduct)
                {
                    if((item.ProductId != 0 && item.ProductId > 0) && (item.CustomerId != 0 && item.CustomerId>0))
                    {
                        CustomerProductAddDto cp = new CustomerProductAddDto();
                        cp.CustomerId = item.CustomerId;
                        cp.ProductId = item.ProductId;
                        await _customerProductService.AddAsync(_mapper.Map<CustomerProduct>(cp));
                    }
                }
            }
            return Created("", customerProduct);
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
