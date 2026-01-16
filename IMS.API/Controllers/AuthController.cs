using IMS.Application.Interfaces.Services;
using IMS.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            try
            {
                var response = await authService.Login(request);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred during login : {ex.Message}" });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(SignUpRequestDto request)
        {
            try
            {
                await authService.Register(request);
                return Ok(new { message = "Registration successful." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred during registration : {ex.Message}" });
            }
        }

        [HttpPost("insertRole")]
        public async Task<IActionResult> InsertRole(string role)
        {
            try
            {
                await authService.InsertRole(role);
                return Ok(new { message = "Role inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred while inserting role : {ex.Message}" });
            }
        }
    }
}
