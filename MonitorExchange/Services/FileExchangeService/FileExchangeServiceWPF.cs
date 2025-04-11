using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MonitorExchange.Data;
using MonitorExchange.Dtos.FileExchange;
using MonitorExchange.Models;
using System.Globalization;

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

            var query = InitializationFilters(_context.FileExchanges.AsQueryable(), fileExchangeRequest.Filters);

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

        private IQueryable<FileExchange> InitializationFilters(IQueryable<FileExchange> query, Dictionary<string, string> filters)
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

                    case "daterangetextbox":

                        string[] parts = value.Split('-');
                        if (parts.Length == 2 &&
                            DateTime.TryParseExact(parts[0], "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime from) &&
                            DateTime.TryParseExact(parts[1], "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime to))
                        {
                            query = query.Where(f => f.DataCreate >= from);
                            query = query.Where(f => f.DataCreate <= to);
                        }
                       
                        break;
 
                }
            }

            return query;
        }

    }
}
