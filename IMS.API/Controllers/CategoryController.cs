using IMS.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMS.API.Controllers
{
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
    }
}
