using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MonitorExchange.Data;
using MonitorExchange.Dtos.FEImport;
using MonitorExchange.Dtos.FileExchange;
using MonitorExchange.Models;
using MonitorExchange.Services.GoodsService;
using MonitorExchange.Services.GoodsSizeService;
using System.Xml.Linq;

namespace MonitorExchange.Services.FileExchangeService
{
    public class FileExchangeIEService : IFileExchangeIEService
    {
        private readonly IMapper _mapper;

        private readonly DataContext _context;

        private readonly IFileExchangeService _fileExchangeService;

        public FileExchangeIEService(IMapper mapper, IFileExchangeService fileExchangeService, DataContext contextl)
        {
            _mapper = mapper;
            _fileExchangeService = fileExchangeService;
            _context = contextl;
        }

        public async Task<ServiceResponse<GetFileExchangeDto>> AddXMLImport(string strId, int item, int allIn, XElement? xml)
        {
            var serviceResponse = new ServiceResponse<GetFileExchangeDto>();

            if (!Directory.Exists("FilesStorage"))
                Directory.CreateDirectory("FilesStorage");
            
            try
            {
                xml.Save($"FilesStorage\\import_{item}_{allIn}_{strId}.xml");

                FileExchange fileExchanges = new FileExchange
                {
                    StrId = strId,
                    Item = item,
                    InAll = allIn,
                    Type = "Import",
                    DataCreate = DateTime.Now,
                    IsUpload = false,
                };

                var _fileExchange = await _fileExchangeService.SaveFileExchangeToBaseAsync(fileExchanges);

                serviceResponse.Data = _mapper.Map<GetFileExchangeDto>(_fileExchange);                                   
                        
            }
            catch (Exception ex)
            {
                serviceResponse.Saccess = false;
                serviceResponse.Message = ex.Message;
            }
            
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetFileExchangeDto>> AddXMLOffers(string strId, int item, int allIn, XElement? xml)
        {
            var serviceResponse = new ServiceResponse<GetFileExchangeDto>();
                   
            if (!Directory.Exists("FilesStorage"))
                Directory.CreateDirectory("FilesStorage");

            try
            {
                xml.Save($"FilesStorage\\offers_{item}_{allIn}_{strId}.xml");

                FileExchange fileExchanges = new FileExchange
                {
                    StrId = strId,
                    Item = item,
                    InAll = allIn,
                    Type = "Offers",
                    DataCreate = DateTime.Now,
                    IsUpload = false,
                };
                
                var _fileExchange = await _fileExchangeService.SaveFileExchangeToBaseAsync(fileExchanges);

                serviceResponse.Data = _mapper.Map<GetFileExchangeDto>(_fileExchange);

            }
            catch (Exception ex)
            {
                serviceResponse.Saccess = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
                        
        }

        public async Task<ServiceResponse<List<GetFEImportsDto>>> GetFEImports(int page, int pageSize)
        {
            var serviceResponse = new ServiceResponse<List<GetFEImportsDto>>();

            if (page <= 0 || pageSize <= 0)
            {
                serviceResponse.Message = "Page and PageSize must be greater than 0.";
                return serviceResponse;
            }

            int offset = (page - 1) * pageSize;
            int totalItems = await _context.FEImports.CountAsync();

            var dbFEImports = await _context.FEImports
                .Include(f => f.FileExchange)
                .Include(f => f.Goods)
                .Include(f => f.GoodsSize)
                .OrderByDescending(i => i.Id)
                .Skip(offset)
                .Take(pageSize)
                .ToListAsync();

            serviceResponse.Data = dbFEImports.Select(f => _mapper.Map<GetFEImportsDto>(f)).ToList();
            serviceResponse.Meta = new
            {
                page
                ,
                pageSize
                ,
                totalItems
            };

            return serviceResponse;
        }
    }    
}
