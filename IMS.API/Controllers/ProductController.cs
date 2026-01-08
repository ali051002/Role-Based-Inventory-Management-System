using IMS.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService _iProductService) : ControllerBase
    {
        [HttpGet("GetProductList")]
        public async Task<IActionResult> GetProductList()
        {
            try
            {
                var result = await _iProductService.GetAllProducts();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
    }
}
