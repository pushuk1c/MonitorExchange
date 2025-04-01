using Microsoft.AspNetCore.Mvc;
using MonitorExchange.Dtos.FEImport;
using MonitorExchange.Dtos.FileExchange;
using MonitorExchange.Models;
using MonitorExchange.Services.FileExchangeService;
using System.Xml.Linq;

namespace MonitorExchange.Controllers.WPF
{
    [ApiController]
    [Route("api/wpf/[controller]")]
    public class FileExchangeIEWPFController : ControllerBase
    {
        private readonly IFileExchangeIEServiceWPF _fileExchangeIEService;

        public FileExchangeIEWPFController(IFileExchangeIEServiceWPF fileExchangeIEService)
        {
            _fileExchangeIEService = fileExchangeIEService;
        }

        [HttpPost("GetFEImports")]
        public async Task<ActionResult<ServiceResponse<List<GetFEImportsDto>>>> GetFEImports(GetFileExchangeRequestWPFDto request)
        {
            return Ok(await _fileExchangeIEService.GetFEImports(request));
        }
              
    }
}
