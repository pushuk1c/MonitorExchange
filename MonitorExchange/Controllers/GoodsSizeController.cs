using Microsoft.AspNetCore.Mvc;
using MonitorExchange.Dtos.GoodsSize;
using MonitorExchange.Models;
using MonitorExchange.Services.GoodsSizeService;

namespace MonitorExchange.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class GoodsSizeController : ControllerBase
    {

        private readonly IGoodsSizeService _goodsSizeService;

        public GoodsSizeController(IGoodsSizeService goodsSizeService)
        {
            _goodsSizeService = goodsSizeService;
        }

        [HttpGet("All")]
        public async Task<ActionResult<ServiceResponse<List<GetGoodsSizeDto>>>> GetAll()
        {
            return Ok(await _goodsSizeService.GetAllGoodsSize());
        }

        [HttpGet("{strId}")]
        public async Task<ActionResult<ServiceResponse<GetGoodsSizeDto>>> GetSingleGoodsSizeById(string strId)
        {
            return Ok(await _goodsSizeService.GetSingleGoodsSizeById(strId));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetGoodsSizeDto>>>> AddGoodsSize(AddGoodsSizeDto newGoodsSize)
        {
            return Ok(await _goodsSizeService.AddGoodsSize(newGoodsSize));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetGoodsSizeDto>>> UpdateGoodsSize(UpdateGoodsSizeDto updateGoodsSize)
        {
            return Ok(await _goodsSizeService.UpdateGoodsSize(updateGoodsSize));
        }

        [HttpDelete("{strId}")]
        public async Task<ActionResult<ServiceResponse<List<GetGoodsSizeDto>>>> DeleteGoodsSize(string strId)
        {
            return Ok(await _goodsSizeService.DeleteGoodsSize(strId));
        }
    }
}
