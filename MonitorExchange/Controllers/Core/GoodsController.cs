using Microsoft.AspNetCore.Mvc;
using MonitorExchange.Dtos.Goods;
using MonitorExchange.Models;
using MonitorExchange.Services.GoodsService;

namespace MonitorExchange.Controllers.Core
{
    [ApiController]
    [Route("api/core/[controller]")]

    public class GoodsController : ControllerBase
    {

        private readonly IGoodsService _goodsService;

        public GoodsController(IGoodsService goodsService)
        {
            _goodsService = goodsService;
        }

        [HttpGet("All")]
        public async Task<ActionResult<ServiceResponse<List<GetGoodsDto>>>> GetAll()
        {
            return Ok(await _goodsService.GetAllGoods());
        }

        [HttpGet("{strId}")]
        public async Task<ActionResult<ServiceResponse<GetGoodsDto>>> GetSingleGoodsById(string strId)
        {
            return Ok(await _goodsService.GetSingleGoodsById(strId));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetGoodsDto>>>> AddGoods(AddGoodsDto newGoods)
        {
            return Ok(await _goodsService.AddGoods(newGoods));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetGoodsDto>>> UpdateGoods(UpdateGoodsDto updateGoods)
        {
            return Ok(await _goodsService.UpdateGoods(updateGoods));
        }

        [HttpDelete("{strId}")]
        public async Task<ActionResult<ServiceResponse<List<GetGoodsDto>>>> DeleteGoods(string strId)
        {
            return Ok(await _goodsService.DeleteGoods(strId));
        }

    }
}
