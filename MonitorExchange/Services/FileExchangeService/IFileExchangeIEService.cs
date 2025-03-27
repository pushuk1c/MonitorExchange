using MonitorExchange.Dtos.FEImport;
using MonitorExchange.Dtos.FileExchange;
using MonitorExchange.Dtos.Goods;
using MonitorExchange.Models;
using System.Xml;
using System.Xml.Linq;

namespace MonitorExchange.Services.FileExchangeService
{
    public interface IFileExchangeIEService
    {        
        Task<ServiceResponse<GetFileExchangeDto>> AddXMLImport(string strId, int item, int allIn, XElement xml);

        Task<ServiceResponse<GetFileExchangeDto>> AddXMLOffers(string strId, int item, int allIn, XElement xml);

        Task<ServiceResponse<List<GetFEImportsDto>>> GetFEImports(int page, int pageSize);
    }
}
