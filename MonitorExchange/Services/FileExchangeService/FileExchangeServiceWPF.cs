using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MonitorExchange.Data;
using MonitorExchange.Dtos.FileExchange;
using MonitorExchange.Models;

namespace MonitorExchange.Services.FileExchangeService
{
    public class FileExchangeServiceWPF : IFileExchangeServiceWPF
    {
        private readonly IMapper _mapper;

        private readonly DataContext _context;

        public FileExchangeServiceWPF(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
       
        public async Task<ServiceResponse<List<GetFileExchangeDto>>> GetFileExchange(GetFileExchangeRequestWPFDto fileExchangeRequest)
        {
            var serviceResponsse = new ServiceResponse<List<GetFileExchangeDto>>();

            if (fileExchangeRequest.Page <= 0 || fileExchangeRequest.PageSize <= 0)
            {
                serviceResponsse.Message = "Page and PageSize must be greater than 0.";
                return serviceResponsse;
            }


            var query = _context.FileExchanges.AsQueryable();

            // 🔍 filters
            foreach (var filter in fileExchangeRequest.Filters)
            {
                string key = filter.Key.ToLower();
                string value = filter.Value;

                if (string.IsNullOrWhiteSpace(value))
                    continue;

                switch (key)
                {
                    case "strid":
                        query = query.Where(f => f.StrId.Contains(value));
                        break;

                    case "type":
                        query = query.Where(f => f.Type.Contains(value));
                        break;

                    case "item":
                        if (int.TryParse(value, out var valueIntItem))
                            query = query.Where(f => f.Item == valueIntItem);
                        break;

                    case "inall":
                        if (int.TryParse(value, out var valueIntInAll))
                            query = query.Where(f => f.InAll == valueIntInAll);
                        break;

                    case "isupload":
                        if (bool.TryParse(value, out var valueIsUpload))
                            query = query.Where(f => f.IsUpload == valueIsUpload);
                        break;

                    /*
                    case "datefrom":
                        if (DateTime.TryParse(value, out var dateFrom))
                            query = query.Where(f => f.DataCreate >= dateFrom);
                        break;

                    case "dateto":
                        if (DateTime.TryParse(value, out var dateTo))
                            query = query.Where(f => f.DataCreate <= dateTo);
                        break;
                    */
                    
                }
            }

            int totalItems = await query.CountAsync();

            var dbFilesExchange = await query
                .OrderByDescending(i => i.DataCreate)
                .ThenByDescending(i => i.Id)
                .Skip((fileExchangeRequest.Page - 1) * fileExchangeRequest.PageSize)
                .Take(fileExchangeRequest.PageSize)
                .ToListAsync();

            serviceResponsse.Data = dbFilesExchange.Select(f => _mapper.Map<GetFileExchangeDto>(f)).ToList();
            serviceResponsse.Meta = new { fileExchangeRequest.Page
                ,
                fileExchangeRequest.PageSize
                , totalItems};


            return serviceResponsse;
        }
             

    }
}
