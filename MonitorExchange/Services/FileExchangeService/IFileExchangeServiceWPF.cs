using MonitorExchange.Dtos.FileExchange;
using MonitorExchange.Models;


namespace MonitorExchange.Services.FileExchangeService
{
    public interface IFileExchangeServiceWPF
    {        
        Task<ServiceResponse<List<GetFileExchangeDto>>> GetFileExchange(GetFileExchangeRequestWPFDto fileExchangeRequest);
        
    }
}
