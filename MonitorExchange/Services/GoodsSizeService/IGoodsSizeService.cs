using MonitorExchange.Dtos.GoodsSize;
using MonitorExchange.Models;
using System.Threading.Tasks;

namespace MonitorExchange.Services.GoodsSizeService
{
    public interface IGoodsSizeService
    {
        Task<ServiceResponse<List<GetGoodsSizeDto>>> GetAllGoodsSize();

        Task<ServiceResponse<GetGoodsSizeDto>> GetSingleGoodsSizeById(string strId);

        Task<ServiceResponse<List<GetGoodsSizeDto>>> AddGoodsSize(AddGoodsSizeDto newGoodsSize);

        Task<ServiceResponse<GetGoodsSizeDto>> UpdateGoodsSize(UpdateGoodsSizeDto updateGoodsSize);

        Task<ServiceResponse<List<GetGoodsSizeDto>>> DeleteGoodsSize(string strId);

        Task<List<GoodsSize>> SaveGoodsSizesToBaseAsync(List<GoodsSize> goodsSizes);

        Task<List<GoodsSize>> GetGoodsSizesByIds(List<string> goodsIds, List<string> goodsSizeIds);
    }
}
