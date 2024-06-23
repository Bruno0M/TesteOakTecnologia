using Microsoft.EntityFrameworkCore;
using System.Net;
using TesteOakTecnologia.Application.DTOs.UserDTOs;
using TesteOakTecnologia.Domain.Entities;
using TesteOakTecnologia.Infrastructure.Data;
using TesteOakTecnologia.Infrastructure.Helpers;
using TesteOakTecnologia.Infrastructure.Interfaces;

namespace TesteOakTecnologia.Infrastructure.Repositories
{
    public class UserRepositoriy : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepositoriy(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<UserResponse>> CreateUserAsync(UserRequest userRequest)
        {
            var serviceResponse = new ServiceResponse<UserResponse>();
            try
            {
                var verifyUserExist = await _context.Users
                    .FirstOrDefaultAsync(user => user.Email == userRequest.Email);

                if (verifyUserExist != null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "This Email is already registered";
                    serviceResponse.Status = HttpStatusCode.BadRequest;
                    return serviceResponse;
                };

                HashingHelper.CreateHashPassword(userRequest.Password, out byte[] passwordHash, out byte[] passwordSalt);

                var user = new User
                {
                    Name = userRequest.Name,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                };

                var userResponse = new UserResponse(
                    user.Name,
                    user.Email);

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                serviceResponse.Data = userResponse;
                serviceResponse.Message = "User successfully created";
                serviceResponse.Status = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }


        public Task<ServiceResponse<UserResponse>> LoginUserAsync(UserLoginRequest loginRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<UserResponse>> GetUserAsync(int userId)
        {
            var serviceResponse = new ServiceResponse<UserResponse>();

            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "User not found";
                    serviceResponse.Status = HttpStatusCode.NotFound;
                    return serviceResponse;
                }

                var userResponse = new UserResponse(
                    user.Name,
                    user.Email);

                serviceResponse.Data = userResponse;
                serviceResponse.Status = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

    }
}
