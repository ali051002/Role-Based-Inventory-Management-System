using IMS.Application.Interfaces.Services;
using IMS.Application.Services;
using IMS.Shared.DTOs.StockTransaction.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController(ITransactionService _iTransactionService) : ControllerBase
    {
        [HttpGet("GetTransactionList")]
        public async Task<IActionResult> GetTransactionList()
        {
            try
            {
                var result = await _iTransactionService.GetAllTransactions();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpPost("StockTransaction")]
        public async Task<IActionResult> StockTransaction(StockTransactionRequestDto request)
        {
            try
            {
                await _iTransactionService.StockTransaction(request);
                return Ok(new { Message = "Stock transaction completed successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
    }
}
