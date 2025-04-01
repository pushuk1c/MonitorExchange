using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MonitorExchange.Data;
using MonitorExchange.Dtos.FEImport;
using MonitorExchange.Dtos.FileExchange;
using MonitorExchange.Models;

namespace MonitorExchange.Services.FileExchangeService
{
    public class FileExchangeIEServiceWPF : IFileExchangeIEServiceWPF
    {
        private readonly IMapper _mapper;

        private readonly DataContext _context;

        private readonly IFileExchangeService _fileExchangeService;

        public FileExchangeIEServiceWPF(IMapper mapper, IFileExchangeService fileExchangeService, DataContext contextl)
        {
            _mapper = mapper;
            _fileExchangeService = fileExchangeService;
            _context = contextl;
        }
        
        public async Task<ServiceResponse<List<GetFEImportsDto>>> GetFEImports(GetFileExchangeRequestWPFDto fileExchangeRequest)
        {
            var serviceResponse = new ServiceResponse<List<GetFEImportsDto>>();

            if (fileExchangeRequest.Page <= 0 || fileExchangeRequest.PageSize <= 0)
            {
                serviceResponse.Message = "Page and PageSize must be greater than 0.";
                return serviceResponse;
            }

            var tmpQuery = _context.FEImports
                .Include(f => f.FileExchange)
                .Include(f => f.Goods)
                .Include(f => f.GoodsSize)
                .AsQueryable();

            var query = InitializationFilters(tmpQuery, fileExchangeRequest.Filters);

            int totalItems = await query.CountAsync();

            var dbFEImports = await query                
                .OrderByDescending(i => i.Id)
                .Skip((fileExchangeRequest.Page - 1) * fileExchangeRequest.PageSize)
                .Take(fileExchangeRequest.PageSize)
                .ToListAsync();

            serviceResponse.Data = dbFEImports.Select(f => _mapper.Map<GetFEImportsDto>(f)).ToList();
            serviceResponse.Meta = new
            {
                fileExchangeRequest.Page
                ,
                fileExchangeRequest.PageSize
                ,
                totalItems
            };

            return serviceResponse;
        }

        private IQueryable<FEImport> InitializationFilters(IQueryable<FEImport> query, Dictionary<string,string> filters)
        {
            // 🔍 filters
            foreach (var filter in filters)
            {
                string key = filter.Key.ToLower();
                string value = filter.Value;

                if (string.IsNullOrWhiteSpace(value))
                    continue;

                switch (key)
                {
                    case "strid":
                        query = query.Where(f => f.FileExchange.StrId.Contains(value));
                        break;

                    case "type":
                        query = query.Where(f => f.FileExchange.Type.Contains(value));
                        break;

                    case "code":
                        query = query.Where(f => f.Goods.Code.Contains(value));
                        break;

                    case "artikul":
                        query = query.Where(f => f.Goods.Artikul.Contains(value));
                        break;

                    case "name":
                        query = query.Where(f => f.Goods.Name.Contains(value));
                        break;

                    case "size":
                        query = query.Where(f => f.GoodsSize.Name.Contains(value));
                        break;

                    case "view":
                        query = query.Where(f => f.Goods.View.Contains(value));
                        break;

                    case "manufacturer":
                        query = query.Where(f => f.Goods.Manufacturer.Contains(value));
                        break;

                    case "country":
                        query = query.Where(f => f.Goods.Country.Contains(value));
                        break;

                    case "material":
                        query = query.Where(f => f.Goods.Material.Contains(value));
                        break;

                    case "season":
                        query = query.Where(f => f.Goods.Season.Contains(value));
                        break;

                    case "color":
                        query = query.Where(f => f.Goods.Color.Contains(value));
                        break;

                    case "categoria":
                        query = query.Where(f => f.Goods.Categoria.Contains(value));
                        break;

                    case "marke":
                        query = query.Where(f => f.Goods.Marke.Contains(value));
                        break;

                    case "brend":
                        query = query.Where(f => f.Goods.Brend.Contains(value));
                        break;

                    case "sex":
                        query = query.Where(f => f.Goods.Sex.Contains(value));
                        break;

                }
            }

            return query;
        }
    }    
}
