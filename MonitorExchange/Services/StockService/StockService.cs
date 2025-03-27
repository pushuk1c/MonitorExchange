using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MonitorExchange.Data;
using MonitorExchange.Dtos.Stock;
using MonitorExchange.Models;

namespace MonitorExchange.Services.StockService
{
    public class StockService : IStockService
    {
        private readonly IMapper _mapper;

        private readonly DataContext _context;

        public StockService(IMapper mapper, DataContext context) { _mapper = mapper; _context = context; }

        public async Task<ServiceResponse<List<GetStockDto>>> AddStock(AddStockDto newStock)
        {
            var serviceResponse = new ServiceResponse<List<GetStockDto>>();
            var stock = _mapper.Map<Stock>(newStock);
           
            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.Stocks.Select(s => _mapper.Map<GetStockDto>(s)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetStockDto>>> GetAllStock()
        {
            var serviceResponse = new ServiceResponse<List<GetStockDto>>();

            var dbStocks = await _context.Stocks.ToListAsync();

            serviceResponse.Data = dbStocks.Select(s => _mapper.Map<GetStockDto>(s)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetStockDto>> GetSingleStockById(string strId)
        {
            var serviceResponse = new ServiceResponse<GetStockDto>();
            var dbStock = await _context.Stocks.FirstOrDefaultAsync(s => s.StrId == strId);

            serviceResponse.Data = _mapper.Map<GetStockDto>(dbStock);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetStockDto>> UpdateStock(UpdateStockDto updateStock)
        {
            var serviceResponse = new ServiceResponse<GetStockDto>();

            try { 
                
                var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.StrId == updateStock.StrId);

                if (stock is null)
                    throw new Exception($"Stock with id '{updateStock.StrId}' not found.");

                _mapper.Map(updateStock, stock);

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetStockDto>(stock);
                
            }
            catch(Exception ex)
            {
                serviceResponse.Saccess = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetStockDto>>> DeleteStock(string strId)
        {
            var serviceResponse = new ServiceResponse<List<GetStockDto>>();

            try
            {

                var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.StrId == strId);

                if (stock is null)
                    throw new Exception($"Stock with id '{strId}' not found.");

                _context.Stocks.Remove(stock);
                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Stocks.Select(s => _mapper.Map<GetStockDto>(s)).ToListAsync();

            }
            catch (Exception ex)
            {
                serviceResponse.Saccess = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<List<Stock>> SaveStocksToBaseAsync(List<Stock> stocks) 
        {
            var newIds = stocks.Select(s => s.StrId).ToList();

            var existingIds = await _context.Stocks
                                   .Where(e => newIds.Contains(e.StrId))
                                   .Select(e => e.StrId)
                                   .ToListAsync();

            var entitiesToInsert = stocks.Where(e => !existingIds.Contains(e.StrId)).ToList();

            if (entitiesToInsert.Any())
            {
                _context.AddRange(entitiesToInsert);
                await _context.SaveChangesAsync();

            }

            return await _context.Stocks
                        .Where(e => newIds.Contains(e.StrId))
                        .Select(e => e)
                        .ToListAsync();
        }
    }
}
