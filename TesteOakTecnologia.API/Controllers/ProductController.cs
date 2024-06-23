using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TesteOakTecnologia.Application.DTOs.ProductDTOs;
using TesteOakTecnologia.Infrastructure.Helpers;
using TesteOakTecnologia.Infrastructure.Interfaces;

namespace TesteOakTecnologia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateProductAsync(ProductRequest productRequest)
        {
            var response = await _productRepository.CreateProductAsync(productRequest);
            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var userId = UserIdentityHelper.GetCurrentUserId(User);

            var response = await _productRepository.GetProductsByUserIdAsync(userId);
            return Ok(response);
        }
    }
}
