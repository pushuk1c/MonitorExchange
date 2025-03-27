using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonitorExchange.Dtos.FileExchange;
using MonitorExchange.Models;
using MonitorExchange.Services.FileExchangeService;
using System.Xml;
using System.Xml.Linq;

namespace MonitorExchange.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class FileExchangeController : ControllerBase
    {
        private readonly IFileExchangeService _fileExchangeService;

        public FileExchangeController(IFileExchangeService fileExchangeService)
        {
            _fileExchangeService = fileExchangeService;
        }

        [HttpGet("All")]
        public async Task<ActionResult<ServiceResponse<List<GetFileExchangeDto>>>> GetAll()
        {
            return Ok( await _fileExchangeService.GetAllFileExchange());

        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetFileExchangeDto>>>> GetFileExchange([FromQuery] int page, [FromQuery] int pageSize)
        {
            return Ok(await _fileExchangeService.GetFileExchange(page, pageSize));
        }

        [HttpGet("{strId}")]
        public async Task<ActionResult<ServiceResponse<GetFileExchangeDto>>> GetSingleById(string strId)
        {
            return Ok(await _fileExchangeService.GetSingleFileExchangeById(strId));

        }    

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetFileExchangeDto>>>> AddFileExchange(AddFileExchangeDto newFileExchange)
        {                 
            return Ok(await _fileExchangeService.AddFileExchange(newFileExchange));        
        }
        
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetFileExchangeDto>>> UpdateFileExchange(UpdateFileExchangeDto updateFileExchange)
        {
            return Ok(await _fileExchangeService.UpdateFileExchange(updateFileExchange));
        }

        [HttpDelete("{strId}")]
        public async Task<ActionResult<ServiceResponse<List<GetFileExchangeDto>>>> DeleteFileExchange(string strId)
        {
            return Ok(await _fileExchangeService.DeleteFileExchange(strId));
        }
    }
}
