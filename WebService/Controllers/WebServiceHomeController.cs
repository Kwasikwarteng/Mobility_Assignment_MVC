using Microsoft.AspNetCore.Mvc;
using Mobility_Assignment_MVC.Models;
using WebService.Services.Interfaces;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebServiceHomeController : ControllerBase
    {
        private readonly IWebService _webService;
        public WebServiceHomeController(IWebService webService)
        {
            _webService = webService;
        }

        // POST: api/WebServiceHome/AddRecord
        [HttpPost("AddRecord")]
        public IActionResult AddRecord([FromBody] Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _webService.AddAsync(person);
            return Ok();
        }

        // DELETE: api/WebServiceHome/DeleteRecord/{name}
        [HttpDelete("DeleteRecord/{id}")]
        public IActionResult DeleteRecord(int id)
        {
            _webService.DeleteAsync(id);
            return Ok();
        }

        // GET: api/WebServiceHome/ListRecords
        [HttpGet("ListRecords")]
        public IActionResult ListRecords()
        {
            var records = _webService.GetPeopleAsync();
            return Ok(records);
        }

        // GET: api/WebServiceHome/SearchRecord/{name}
        [HttpGet("SearchRecord/{name}")]
        public IActionResult SearchRecord(string name)
        {
            var record = _webService.SearchAsync(name);
            if (record == null)
            {
                return NotFound();
            }

            return Ok(record);
        }
    }
}
