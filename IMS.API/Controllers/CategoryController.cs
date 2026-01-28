using IMS.Application.Interfaces.Services;
using IMS.Shared.DTOs.Category.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMS.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService _iCategoryService) : ControllerBase
    {
        [HttpGet("GetCategoryList")]
        public async Task<IActionResult> GetCategoryList()
        {
            try
            {
                var result = await _iCategoryService.GetAllCategories();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpGet("GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(Guid Id)
        {
            try
            {
                var result = await _iCategoryService.GetCategoryById(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost("SaveCategory")]
        public async Task<IActionResult> SaveCategory(SaveCategoryRequestDto request)
        {
            try
            {
                await _iCategoryService.SaveCategory(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost("DeleteCategory/{Id}")]
        public async Task<IActionResult> DeleteCategory(Guid Id)
        {
            try
            {
                await _iCategoryService.DeleteCategory(Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
    }
}
