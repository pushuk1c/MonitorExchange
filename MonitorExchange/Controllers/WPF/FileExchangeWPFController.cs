using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonitorExchange.Dtos.FileExchange;
using MonitorExchange.Models;
using MonitorExchange.Services.FileExchangeService;
using System.Xml;
using System.Xml.Linq;

namespace MonitorExchange.Controllers.WPF
{

    [ApiController]
    [Route("api/wpf/[controller]")]
    public class FileExchangeWPFController : ControllerBase
    {
        private readonly IFileExchangeServiceWPF _fileExchangeService;

        public FileExchangeWPFController(IFileExchangeServiceWPF fileExchangeService)
        {
            _fileExchangeService = fileExchangeService;
        }
       
        [HttpPost("GetFilesExchange")]
        public async Task<ActionResult<ServiceResponse<List<GetFileExchangeDto>>>> GetFileExchange([FromBody] GetFileExchangeRequestWPFDto request)
        {
            return Ok(await _fileExchangeService.GetFileExchange(request));
        }

       
    }
}
