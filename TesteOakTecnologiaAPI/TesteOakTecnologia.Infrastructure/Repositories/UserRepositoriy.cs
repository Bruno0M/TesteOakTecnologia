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
        private readonly ITokenRepository _tokenRepository;

        public UserRepositoriy(AppDbContext context, ITokenRepository tokenRepository)
        {
            _context = context;
            _tokenRepository = tokenRepository;
        }

        public async Task<ServiceResponse<UserResponse>> CreateUserAsync(UserRequest userRequest)
        {
            ServiceResponse<UserResponse> response = new();
            try
            {
                bool userExists = await _context.Users
                    .AnyAsync(u => u.Email == userRequest.Email);

                if (userExists)
                {
                    response.Data = null;
                    response.Message = "This Email is already registered";
                    response.Status = HttpStatusCode.BadRequest;
                    return response;
                };

                HashingHelper.CreateHashPassword(userRequest.Password, out byte[] passwordHash, out byte[] passwordSalt);

                var user = new User
                {
                    Name = userRequest.Name,
                    Email = userRequest.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                };


                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var userResponse = new UserResponse(
                    user.Name,
                    user.Email);

                response.Data = userResponse;
                response.Message = "User successfully created";
                response.Status = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }


        public async Task<ServiceResponse<UserResponse>> LoginUserAsync(UserLoginRequest loginRequest)
        {
            ServiceResponse<UserResponse> response = new();
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginRequest.Email);

                if (user == null || !HashingHelper.VerifyPassword(loginRequest.Password, user.PasswordHash, user.PasswordSalt))
                {
                    response.Data = null;
                    response.Message = "Invalid Credentials";
                    response.Status = HttpStatusCode.BadRequest;
                    return response;
                }

                string token = _tokenRepository.GenerateToken(user);

                var userResponse = new UserResponse(
                    user.Name,
                    user.Email,
                    token);

                response.Data = userResponse;
                response.Message = "Login successful";
                response.Status = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<UserResponse>>> GetUserAsync(int userId)
        {
            ServiceResponse<List<UserResponse>> response = new();

            try
            {
                IQueryable<User> user = _context.Users.Where(u => u.Id == userId).AsQueryable();

                if (user == null)
                {
                    response.Data = null;
                    response.Message = "User not found";
                    response.Status = HttpStatusCode.NotFound;
                    return response;
                }

                var userResponse = await user.Select(u => new UserResponse(
                    u.Name,
                    u.Email)).ToListAsync();

                response.Data = userResponse;
                response.Status = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

    }
}
