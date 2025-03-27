using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MonitorExchange.Data;
using MonitorExchange.Dtos.GoodsSize;
using MonitorExchange.Models;

namespace MonitorExchange.Services.GoodsSizeService
{
    public class GoodsSizeService : IGoodsSizeService
    {
        private readonly IMapper _mapper;

        private readonly DataContext _context;

        public GoodsSizeService(IMapper mapper, DataContext context) { _mapper = mapper; _context = context; }

        public async Task<ServiceResponse<List<GetGoodsSizeDto>>> AddGoodsSize(AddGoodsSizeDto newGoodsSize)
        {
            var serviceResponse = new ServiceResponse<List<GetGoodsSizeDto>>();
            var goodSize = _mapper.Map<GoodsSize>(newGoodsSize);

            _context.GoodsSizes.Add(goodSize);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.GoodsSizes.Select(g => _mapper.Map<GetGoodsSizeDto>(g)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetGoodsSizeDto>>> GetAllGoodsSize()
        {
            var serviceResponse = new ServiceResponse<List<GetGoodsSizeDto>>();

            var dbGoodsSizes = await _context.GoodsSizes.ToListAsync();

            serviceResponse.Data = dbGoodsSizes.Select(g => _mapper.Map<GetGoodsSizeDto>(g)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetGoodsSizeDto>> GetSingleGoodsSizeById(string strId)
        {
            var serviceResponse = new ServiceResponse<GetGoodsSizeDto>();
          
            var dbGoodsSize = await _context.GoodsSizes.FirstOrDefaultAsync(g => g.strId == strId);

            serviceResponse.Data = _mapper.Map<GetGoodsSizeDto>(dbGoodsSize);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetGoodsSizeDto>> UpdateGoodsSize(UpdateGoodsSizeDto updateGoodsSize)
        {
            var serviceResponse = new ServiceResponse<GetGoodsSizeDto>();
            
            try {

                var goodsSize = await _context.GoodsSizes.FirstOrDefaultAsync(g => g.strId == updateGoodsSize.strId);

                if (goodsSize is null)
                    throw new Exception($"GoodsSize with id '{updateGoodsSize.strId}' not found.");

                _mapper.Map(updateGoodsSize, goodsSize);

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetGoodsSizeDto>(goodsSize);
               
            }
            catch (Exception ex)
            {
                serviceResponse.Saccess = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetGoodsSizeDto>>> DeleteGoodsSize(string strId)
        {
            var serviceResponse = new ServiceResponse<List<GetGoodsSizeDto>>();

            try
            {
                var goodsSize = await _context.GoodsSizes.FirstOrDefaultAsync(g => g.strId == strId);

                if (goodsSize is null)
                    throw new Exception($"GoodsSize with id '{strId}' not found.");

                _context.GoodsSizes.Remove(goodsSize);
                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.GoodsSizes.Select(g => _mapper.Map<GetGoodsSizeDto>(g)).ToListAsync();

            }
            catch (Exception ex)
            {
                serviceResponse.Saccess = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
        
        public async Task<List<GoodsSize>> SaveGoodsSizesToBaseAsync(List<GoodsSize> goodsSizes)
        {
            var newIds = goodsSizes.Select(gs => gs.strId).ToList();

            var existingIds = await _context.GoodsSizes
                                   .Where(e => newIds.Contains(e.strId))
                                   .Select(e => e.strId)
                                   .ToListAsync();

            var entitiesToInsert = goodsSizes.Where(e => !existingIds.Contains(e.strId)).ToList();

            if (entitiesToInsert.Any())
            {
                _context.AddRange(entitiesToInsert);
                await _context.SaveChangesAsync();

            }

            return await _context.GoodsSizes
                        .Where(e => newIds.Contains(e.strId))
                        .Select(e => e)
                        .ToListAsync();
        }

        public async Task<List<GoodsSize>> GetGoodsSizesByIds(List<string> goodsIds, List<string> goodsSizeIds)
        {
            return await _context.GoodsSizes.Where(g => goodsIds.Contains(g.Goods.strId) && goodsSizeIds.Contains(g.strId)).ToListAsync();
        }
    }
}
