using TesteOakTecnologia.Application.DTOs.UserDTOs;
using TesteOakTecnologia.Domain.Entities;

namespace TesteOakTecnologia.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<ServiceResponse<UserResponse>> CreateUserAsync(UserRequest userRequest);
        Task<ServiceResponse<UserResponse>> LoginUserAsync(UserLoginRequest loginRequest);
        Task<ServiceResponse<List<UserResponse>>> GetUserAsync(int userId);
    }
}
