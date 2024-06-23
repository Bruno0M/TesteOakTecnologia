using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TesteOakTecnologia.Application.DTOs.UserDTOs;
using TesteOakTecnologia.Infrastructure.Helpers;
using TesteOakTecnologia.Infrastructure.Interfaces;

namespace TesteOakTecnologia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRequest userRequest)
        {
            var response = await _userRepository.CreateUserAsync(userRequest);

            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginRequest userLoginRequest)
        {
            var response = await _userRepository.LoginUserAsync(userLoginRequest);

            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserAsync()
        {
           var userId = UserIdentityHelper.GetCurrentUserId(User);

            var response = await _userRepository.GetUserAsync(userId);

            return Ok(response);
        }
    }
}
