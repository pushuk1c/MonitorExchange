using MonitorExchange.Dtos.FEImport;
using MonitorExchange.Dtos.FileExchange;
using MonitorExchange.Dtos.Goods;
using MonitorExchange.Models;
using System.Xml;
using System.Xml.Linq;

namespace MonitorExchange.Services.FileExchangeService
{
    public interface IFileExchangeIEServiceWPF
    {        
        Task<ServiceResponse<List<GetFEImportsDto>>> GetFEImports(GetFileExchangeRequestWPFDto fileExchangeRequestWPFDto);
    }
}
