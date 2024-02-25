using Microsoft.AspNetCore.Mvc;
using Mobility_Assignment_MVC.Models;
using WebService.Services.IServices;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebServiceHomeController : ControllerBase
    {
        private readonly IPersonService _webService;
        public WebServiceHomeController(IPersonService webService)
        {
            _webService = webService;
        }

        // POST: api/WebServiceHome/AddRecord
        [HttpPost("AddRecord")]
        public async Task<IActionResult> AddRecord([FromBody] Person person)
        {
            await _webService.AddAsync(person);
            return Ok();
        }

        // DELETE: api/WebServiceHome/DeleteRecord/{name}
        [HttpDelete("DeleteRecord/{id}")]
        public async Task<IActionResult> DeleteRecord(int id)
        {
            try
            {
                await _webService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/WebServiceHome/ListRecords
        [HttpGet("ListRecords")]
        public async Task<IActionResult> ListRecords()
        {
            var records = await _webService.GetPeopleAsync();
            return Ok(records);
        }

        // GET: api/WebServiceHome/SearchRecord/{name}
        [HttpGet("SearchRecord/{name}")]
        public async Task<IActionResult> SearchRecord(string firstName, string lastName)
        {
            var record = await _webService.SearchAsync(firstName, lastName);
            if (record == null)
            {
                return NotFound();
            }

            return Ok(record);
        }
    }
}
