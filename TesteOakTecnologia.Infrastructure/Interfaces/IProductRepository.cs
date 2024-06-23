using TesteOakTecnologia.Application.DTOs.ProductDTOs;
using TesteOakTecnologia.Domain.Entities;

namespace TesteOakTecnologia.Infrastructure.Interfaces
{
    public interface IProductRepository
    {
        Task<ServiceResponse<ProductResponse>> CreateProductAsync(ProductRequest productRequest);

        Task<ServiceResponse<List<ProductResponse>>> GetProductsByUserIdAsync(int userId);
    }
}
