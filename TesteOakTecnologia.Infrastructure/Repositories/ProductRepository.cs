using Microsoft.EntityFrameworkCore;
using System.Net;
using TesteOakTecnologia.Application.DTOs.ProductDTOs;
using TesteOakTecnologia.Domain.Entities;
using TesteOakTecnologia.Infrastructure.Data;
using TesteOakTecnologia.Infrastructure.Interfaces;

namespace TesteOakTecnologia.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<ProductResponse>> CreateProductAsync(ProductRequest productRequest)
        {
            ServiceResponse<ProductResponse> response = new();
            try
            {
                Product product = new Product()
                {
                    UserId = productRequest.UserId,
                    Name = productRequest.Name,
                    Description = productRequest.Description,
                    Amount = productRequest.Amount,
                    AvailableSale = productRequest.AvailableSale
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                ProductResponse productResponse = new ProductResponse(
                    product.Id,
                    product.UserId,
                    product.Name,
                    product.Description,
                    product.Amount,
                    product.AvailableSale);

                response.Data = productResponse;
                response.Message = "The product was successfully registered!";
                response.Status = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<ProductResponse>>> GetProductsByUserIdAsync(int userId)
        {
            ServiceResponse<List<ProductResponse>> response = new();
            try
            {
                IQueryable<Product> products = _context.Products.Where(p => p.UserId == userId).AsQueryable();

                var productResponse = await products.Select( p => new ProductResponse(
                    p.Id,
                    p.UserId,
                    p.Name,
                    p.Description,
                    p.Amount,
                    p.AvailableSale)).ToListAsync();

                response.Data = productResponse;
                response.Status = HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                response.Message= ex.Message;
            }

            return response;
        }
    }
}
