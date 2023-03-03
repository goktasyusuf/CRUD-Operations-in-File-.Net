using Business;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileOperationController : Controller
    {
        IFileOperationService _service;

        public FileOperationController(IFileOperationService service)
        {
            _service = service;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var values = _service.GetAll();
            if (values.Success)
            {
                return Ok(values);
            }
            return BadRequest(values.Message);

        }

        [HttpGet("getoldbrand")]
        public IActionResult GetOldBrand()
        {
            var values = _service.GetOldBrand();
            return Ok(values);
        }

        [HttpGet("getoldipaddress")]
        public IActionResult GetOldIpAddress()
        {
            var values = _service.GetOldIpAddress();
            return Ok(values);
        }

        [HttpPost("update")]
        public IActionResult Update(FileOperation model)
        {
            var result = _service.UpdateBrandNameAndIpAddress(model);
            if(result.Success) 
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
