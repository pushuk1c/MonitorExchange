using MonitorExchange.Dtos.Stock;
using MonitorExchange.Models;

namespace MonitorExchange.Services.StockService
    {
    public interface IStockService
    {
        Task<ServiceResponse<List<GetStockDto>>> GetAllStock();

        Task<ServiceResponse<GetStockDto>> GetSingleStockById(string strId);

        Task<ServiceResponse<List<GetStockDto>>> AddStock(AddStockDto newStock);

        Task<ServiceResponse<GetStockDto>> UpdateStock(UpdateStockDto updateStock);

        Task<ServiceResponse<List<GetStockDto>>> DeleteStock(string strId);

        Task<List<Stock>> SaveStocksToBaseAsync(List<Stock> stocks);

    }
}
