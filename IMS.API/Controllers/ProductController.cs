using IMS.Application.Interfaces.Services;
using IMS.Shared.DTOs.Product.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMS.API.Controllers
{
    [Authorize]
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

        [HttpGet("GetProductsByCategory")]
        public async Task<IActionResult> GetProductsByCategory(Guid Id)
        {
            try
            {
                var result = await _iProductService.GetProductsByCategory(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost("SaveProduct")]
        public async Task<IActionResult> SaveProduct(SaveProductRequestDto request)
        {
            try
            {
                await _iProductService.SaveProduct(request);
                return Ok(new { Message = "Product saved successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductById(Guid Id)
        {
            try
            {
                var result = await _iProductService.GetProductById(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost("DeleteProduct/{Id}")]
        public async Task<IActionResult> DeleteProduct(Guid Id)
        {
            try
            {
                await _iProductService.DeleteProduct(Id);
                return Ok(new { Message = "Product deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
    }
}
