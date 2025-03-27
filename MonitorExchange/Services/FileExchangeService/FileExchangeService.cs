using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MonitorExchange.Data;
using MonitorExchange.Dtos.FileExchange;
using MonitorExchange.Models;

namespace MonitorExchange.Services.FileExchangeService
{
    public class FileExchangeService : IFileExchangeService
    {
        private readonly IMapper _mapper;

        private readonly DataContext _context;

        public FileExchangeService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetFileExchangeDto>>> AddFileExchange(AddFileExchangeDto newFileExchange)
        {
            var serviceResponsse = new ServiceResponse<List<GetFileExchangeDto>>();
            var fileExchange = _mapper.Map<FileExchange>(newFileExchange);

            _context.FileExchanges.Add(fileExchange);
            await _context.SaveChangesAsync();

            serviceResponsse.Data = await _context.FileExchanges.Select(f => _mapper.Map<GetFileExchangeDto>(f)).ToListAsync();

            return serviceResponsse;
        }

        public async Task<ServiceResponse<List<GetFileExchangeDto>>> DeleteFileExchange(string strId)
        {
            var serviceResponse = new ServiceResponse<List<GetFileExchangeDto>>();

            try
            {
                var fileExchange = await _context.FileExchanges.FirstOrDefaultAsync(f => f.StrId == strId);

                if (fileExchange is null)
                    throw new Exception($"FileExchange with id '{strId}' not found.");

                _context.FileExchanges.Remove(fileExchange);
                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.FileExchanges.Select(f => _mapper.Map<GetFileExchangeDto>(f)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Saccess = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetFileExchangeDto>>> GetAllFileExchange()
        {
            var serviceResponsse = new ServiceResponse<List<GetFileExchangeDto>>();

            var dbFilesExchange = await _context.FileExchanges.ToListAsync();

            serviceResponsse.Data = dbFilesExchange.Select(f => _mapper.Map<GetFileExchangeDto>(f)).ToList();

            return serviceResponsse;
        }

        public async Task<ServiceResponse<GetFileExchangeDto>> GetSingleFileExchangeById(string strId)
        {
            var serviceResponsse = new ServiceResponse<GetFileExchangeDto>();

            var dbFilesExchange = await _context.FileExchanges.FirstOrDefaultAsync(f => f.StrId == strId);

            serviceResponsse.Data = _mapper.Map<GetFileExchangeDto>(dbFilesExchange);

            return serviceResponsse;
        }

        public async Task<ServiceResponse<List<GetFileExchangeDto>>> GetFileExchange(int page, int pageSize)
        {
            var serviceResponsse = new ServiceResponse<List<GetFileExchangeDto>>();

            if (page <= 0 || pageSize <= 0)
            {
                serviceResponsse.Message = "Page and PageSize must be greater than 0.";
                return serviceResponsse;
            }

            int offset = (page - 1) * pageSize;
            int totalItems = await _context.FileExchanges.CountAsync();

            var dbFilesExchange = await _context.FileExchanges
                .OrderByDescending(i => i.DataCreate)
                .ThenByDescending(i => i.Id)
                .Skip(offset)
                .Take(pageSize)
                .ToListAsync();

            serviceResponsse.Data = dbFilesExchange.Select(f => _mapper.Map<GetFileExchangeDto>(f)).ToList();
            serviceResponsse.Meta = new { page
                , pageSize
                , totalItems};


            return serviceResponsse;
        }

        public async Task<ServiceResponse<GetFileExchangeDto>> UpdateFileExchange(UpdateFileExchangeDto updateFileExchange)
        {
            var serviceResponse = new ServiceResponse<GetFileExchangeDto>();

            try
            {
                var fileExchange = await _context.FileExchanges.FirstOrDefaultAsync(f => f.StrId == updateFileExchange.StrId);

                if (fileExchange is null)
                    throw new Exception($"FileExchange with id '{updateFileExchange.StrId}' not found.");

                _mapper.Map(updateFileExchange, fileExchange);

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetFileExchangeDto>(fileExchange);

            }
            catch (Exception ex)
            {
                serviceResponse.Saccess = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<FileExchange> SaveFileExchangeToBaseAsync(FileExchange fileExchanges)
        {
            var fileExch = await _context.FileExchanges.FirstOrDefaultAsync(f =>
                                f.StrId == fileExchanges.StrId
                                && f.Item == fileExchanges.Item
                                && f.InAll == fileExchanges.InAll
                                && f.Type == fileExchanges.Type);

            if (fileExch is null)
            {
                _context.FileExchanges.Add(fileExchanges);
                await _context.SaveChangesAsync();
            }

            return await _context.FileExchanges.FirstOrDefaultAsync(f =>
                                f.StrId == fileExchanges.StrId
                                && f.Item == fileExchanges.Item
                                && f.InAll == fileExchanges.InAll
                                && f.Type == fileExchanges.Type);
        }

        public async Task<FileExchange> UpdateFileExchangeToBaseAsync(FileExchange updateFileExchange)
        {
            try
            {
                var fileExchange = await _context.FileExchanges
                                .FirstOrDefaultAsync(f => f.StrId == updateFileExchange.StrId &&
                                            f.Type == updateFileExchange.Type &&
                                            f.InAll == updateFileExchange.InAll &&
                                            f.Item == updateFileExchange.Item);

                if (fileExchange is null)
                    throw new Exception($"FileExchange with id '{updateFileExchange.StrId}' not found.");

                fileExchange.IsUpload = updateFileExchange.IsUpload;

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                
            }

            return await _context.FileExchanges
                                .FirstOrDefaultAsync(f => f.StrId == updateFileExchange.StrId &&
                                            f.Type == updateFileExchange.Type &&
                                            f.InAll == updateFileExchange.InAll &&
                                            f.Item == updateFileExchange.Item); 

        }

        public async Task<List<FEImport>> SaveFileExchangeImportToBaseAsync(List<FEImport> imports)
        {
            var FEStrIds = imports.Select(i => i.FileExchange.StrId).Distinct().ToList();
            var GStrIds = imports.Select(i => i.Goods.strId).Distinct().ToList();
            var GSStrIds = imports.Select(i => i.GoodsSize.strId).Distinct().ToList();
                        
            var filterFileExchangeIds = _context.FileExchanges.Where(f => FEStrIds.Contains(f.StrId)).Select(f => f.Id).ToList();
            var filterGoodsIds = _context.Goodses.Where(g => GStrIds.Contains(g.strId)).Select(g => g.Id).ToList();
            var filterGoodsSizeIds = _context.GoodsSizes.Where(gs => GSStrIds.Contains(gs.strId)).Select(gs => gs.Id).ToList();

            var existingIds = await _context.FEImports
                .Join(filterFileExchangeIds, fe => fe.FileExchange.Id, feq => feq, (fe, feq) => fe)
                .Join(filterGoodsIds, fe => fe.Goods.Id, gq => gq, (fe, gq) => fe)
                .Join(filterGoodsSizeIds, fe => fe.GoodsSize.Id, gsq => gsq, (fe, gsq) => new
                        {
                            StrIdF = fe.FileExchange.StrId,
                            StrIdG = fe.Goods.strId,
                            StrIdGS = fe.GoodsSize.strId
                        })
                .ToListAsync();

            var existingIdsSet = existingIds
                .Select(e => $"{e.StrIdF}-{e.StrIdG}-{e.StrIdGS}")
                .ToHashSet();

            var entitiesToInsert = imports
                .Where(e => !existingIdsSet.Contains($"{e.FileExchange.StrId}-{e.Goods.strId}-{e.GoodsSize.strId}"))
                .ToList();

            if (entitiesToInsert.Any())
            {
                _context.AddRange(entitiesToInsert);
                await _context.SaveChangesAsync();
            }

            return await _context.FEImports
                        .Where(i => FEStrIds.Contains(i.FileExchange.StrId))
                        .ToListAsync();
        }

        public async Task<List<FEOffers>> SaveFileExchangeOffersToBaseAsync(List<FEOffers> offers)
        {
            var FEStrIds = offers.Select(o => o.FileExchange.StrId).Distinct().ToList();
            var GStrIds = offers.Select(o => o.Goods.strId).Distinct().ToList();
            var GSStrIds = offers.Select(o => o.GoodsSize.strId).Distinct().ToList();
            var SStrIds = offers.Select(o => o.Stock.StrId).Distinct().ToList();

            var filterFileExchangeIds = _context.FileExchanges.Where(f => FEStrIds.Contains(f.StrId)).Select(f => f.Id).ToList();
            var filterGoodsIds = _context.Goodses.Where(g => GStrIds.Contains(g.strId)).Select(g => g.Id).ToList();
            var filterGoodsSizeIds = _context.GoodsSizes.Where(gs => GSStrIds.Contains(gs.strId)).Select(gs => gs.Id).ToList();
            var filterStockIds = _context.Stocks.Where(s => SStrIds.Contains(s.StrId)).Select(s => s.Id).ToList();

            var existingIds = await _context.FEOffers
                    .Join(filterFileExchangeIds, fe => fe.FileExchange.Id, feq => feq, (fe, feq) => fe)
                    .Join(filterGoodsIds, fe => fe.Goods.Id, g => g, (fe, g) => fe)
                    .Join(filterGoodsSizeIds, fe => fe.GoodsSize.Id, gs => gs, (fe, gs) => fe)
                    .Join(filterStockIds, fe => fe.Stock.Id, s => s, (fe, s) => new
                    {
                        StrIdF = fe.FileExchange.StrId,
                        StrIdG = fe.Goods.strId,
                        StrIdGs = fe.GoodsSize.strId,
                        StrIdS = fe.Stock.StrId
                    })
                    .ToListAsync();
                       
            var existingIdsSet = existingIds
                .Select(e => $"{e.StrIdF}-{e.StrIdG}-{e.StrIdGs}-{e.StrIdS}")
                .ToHashSet();

            var entitiesToInsert = offers
                .Where(e => !existingIdsSet.Contains($"{e.FileExchange.StrId}-{e.Goods.strId}-{e.GoodsSize.strId}-{e.Stock.StrId}"))
                .ToList();

            if (entitiesToInsert.Any())
            {
                _context.AddRange(entitiesToInsert);
                await _context.SaveChangesAsync();
            }

            return await _context.FEOffers
                        .Where(i => FEStrIds.Contains(i.FileExchange.StrId))
                        .ToListAsync();
        }

    }
}
