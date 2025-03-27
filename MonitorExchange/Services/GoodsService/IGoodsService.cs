using MonitorExchange.Dtos.Goods;
using MonitorExchange.Models;

namespace MonitorExchange.Services.GoodsService
{
    public interface IGoodsService
    {
        Task<ServiceResponse<List<GetGoodsDto>>> GetAllGoods();

        Task<ServiceResponse<GetGoodsDto>> GetSingleGoodsById(string strId);

        Task<ServiceResponse<List<GetGoodsDto>>> AddGoods(AddGoodsDto newGoods);

        Task<ServiceResponse<GetGoodsDto>> UpdateGoods(UpdateGoodsDto updateGoods);

        Task<ServiceResponse<List<GetGoodsDto>>> DeleteGoods(string strId);

        Task<List<Goods>> SaveGoodsesToBaseAsync(List<Goods> goodses);

        Task<List<Goods>> GetGoodsesByIds(List<string> ids);

    }
}
