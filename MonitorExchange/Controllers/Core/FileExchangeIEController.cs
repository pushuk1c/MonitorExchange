using Microsoft.AspNetCore.Mvc;
using MonitorExchange.Dtos.FEImport;
using MonitorExchange.Dtos.FileExchange;
using MonitorExchange.Models;
using MonitorExchange.Services.FileExchangeService;
using System.Xml.Linq;

namespace MonitorExchange.Controllers.Core
{
    [ApiController]
    [Route("api/core/[controller]")]
    public class FileExchangeIEController : ControllerBase
    {
        private readonly IFileExchangeIEService _fileExchangeIEService;

        public FileExchangeIEController(IFileExchangeIEService fileExchangeIEService)
        {
            _fileExchangeIEService = fileExchangeIEService;
        }

        [HttpPost("FEImports")]
        public async Task<ActionResult<ServiceResponse<List<GetFEImportsDto>>>> GetFEImports([FromBody] int page, [FromQuery] int pageSize)
        {
            return Ok(await _fileExchangeIEService.GetFEImports(page, pageSize));
        }

        [HttpPost("XMLImport")]
        [Consumes("application/xml")]
        public async Task<ActionResult<ServiceResponse<GetFileExchangeDto>>> AddXMLImport(string strId, int item, int allIn, [FromBody] XElement xml)
        {
            return Ok(await _fileExchangeIEService.AddXMLImport(strId, item, allIn, xml));
        }

        [HttpPost("XMLOffers")]
        [Consumes("application/xml")]
        public async Task<ActionResult<ServiceResponse<GetFileExchangeDto>>> AddXMLOffers(string strId, int item, int allIn, [FromBody] XElement xml)
        {
            return Ok(await _fileExchangeIEService.AddXMLOffers(strId, item, allIn, xml));
        }

    }
}
