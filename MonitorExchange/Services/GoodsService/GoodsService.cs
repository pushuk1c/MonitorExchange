using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MonitorExchange.Data;
using MonitorExchange.Dtos.Goods;
using MonitorExchange.Models;

namespace MonitorExchange.Services.GoodsService
{
    public class GoodsService : IGoodsService
    {
        private readonly IMapper _mapper;

        private readonly DataContext _context;

        public GoodsService(IMapper mapper, DataContext context) {  _mapper = mapper; _context = context; }

        public async Task<ServiceResponse<List<GetGoodsDto>>> AddGoods(AddGoodsDto newGoods)
        {
            var serviceResponse = new ServiceResponse<List<GetGoodsDto>>();
            var goods = _mapper.Map<Goods>(newGoods);

            _context.Goodses.Add(goods);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.Goodses.Select(g => _mapper.Map<GetGoodsDto>(g)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetGoodsDto>>> GetAllGoods()
        {
            var serviceResponse = new ServiceResponse<List<GetGoodsDto>>();

            var dbGoodses = await _context.Goodses.ToListAsync();

            serviceResponse.Data = dbGoodses.Select(g => _mapper.Map<GetGoodsDto>(g)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetGoodsDto>> GetSingleGoodsById(string strId)
        {
            var serviceResponse = new ServiceResponse<GetGoodsDto>();
           
            var dbGoods = await _context.Goodses.FirstOrDefaultAsync(g => g.strId == strId);

            serviceResponse.Data = _mapper.Map<GetGoodsDto>(dbGoods); 

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetGoodsDto>> UpdateGoods(UpdateGoodsDto updateGoods)
        {
            var serviceResponse = new ServiceResponse<GetGoodsDto>();

            try
            {
                var goods = await _context.Goodses.FirstOrDefaultAsync(g => g.strId == updateGoods.strId);

                if (goods is null)
                    throw new Exception($"Goods with id '{updateGoods.strId}' not found.");

                _mapper.Map(updateGoods, goods);
                
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetGoodsDto>(goods);
                
            }
            catch(Exception ex)
            {
                serviceResponse.Saccess = false;
                serviceResponse.Message = ex.Message;
            }
            
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetGoodsDto>>> DeleteGoods(string strId)
        {
            var serviceResponse = new ServiceResponse<List<GetGoodsDto>>();

            try
            {
                var goods = await _context.Goodses.FirstOrDefaultAsync(g => g.strId == strId);

                if (goods is null)
                    throw new Exception($"Goods with id '{strId}' not found.");
                  
                _context.Goodses.Remove(goods);
                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Goodses.Select(g => _mapper.Map<GetGoodsDto>(g)).ToListAsync();

            }
            catch (Exception ex)
            {
                serviceResponse.Saccess = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<List<Goods>> SaveGoodsesToBaseAsync(List<Goods> goodses) {

            foreach (var g in goodses)
                g.UpdateHash();
                      
            var newHashes = goodses.Select(g => g.Hash).Distinct().ToList();

            var existingHashes = await _context.Goodses
                                   .Where(g => newHashes.Contains(g.Hash))
                                   .Select(g => g.Hash)
                                   .ToListAsync();

            var entitiesToInsert = goodses.Where(e => !existingHashes.Contains(e.Hash)).Distinct().ToList();

            if (entitiesToInsert.Any())
            {
                _context.Goodses.AddRange(entitiesToInsert);
                await _context.SaveChangesAsync();
            }

            return await _context.Goodses
                        .Where(g => newHashes.Contains(g.Hash))
                        .Select(g => g)
                        .ToListAsync();
        }     
    
        public async Task<List<Goods>> GetGoodsesByIds(List<string> ids)
        {
            return await _context.Goodses
                         .Where(g => ids.Contains(g.strId))
                         .GroupBy(g => g.strId)
                         .Select(g => g.OrderByDescending(x => x.Id).FirstOrDefault()) 
                         .ToListAsync();
        }

    }
}
