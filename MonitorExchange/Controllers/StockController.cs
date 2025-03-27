using Microsoft.AspNetCore.Mvc;
using MonitorExchange.Dtos.Stock;
using MonitorExchange.Models;
using MonitorExchange.Services.StockService;

namespace MonitorExchange.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet("All")]
        public async Task<ActionResult<ServiceResponse<List<GetStockDto>>>> GetAll() 
        {
            return Ok(await _stockService.GetAllStock());       
        }

        [HttpGet("{strId}")]
        public async Task<ActionResult<ServiceResponse<GetStockDto>>> GetSingleStockById(string strId)
        {
            return Ok(await _stockService.GetSingleStockById(strId));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetStockDto>>>> AddStock(AddStockDto newStock)
        {
            return Ok(await _stockService.AddStock(newStock));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetStockDto>>> UpdateStock(UpdateStockDto updateStock)
        {
            return Ok(await _stockService.UpdateStock(updateStock));
        }

        [HttpDelete("{strId}")]
        public async Task<ActionResult<ServiceResponse<List<GetStockDto>>>> DeleteStock(string strId)
        {
            return Ok(await _stockService.DeleteStock(strId));
        }
    }
}
