using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reports.DAL.Entities;
using Reports.Server.Services;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpPost("/employees/Create")]
        public async Task<Employee> Create([FromQuery] string name)
        {
            var userId = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = _service.GetUser(userId);
            return await _service.CreateEmployee(name);
        }
        
        [HttpDelete("/employees/Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = _service.GetUser(userId);
            await _service.Delete(id);
            return Ok();
        }
        
        [HttpGet("/employees/GetById")]
        public IActionResult Find([FromQuery] Guid id)
        {
            var userId = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = _service.GetUser(userId);
            if (id == Guid.Empty) return StatusCode((int) HttpStatusCode.BadRequest);
            {
                var result = _service.FindById(id);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }
        }
        
        [HttpPatch("/employees/NewLead")]
        public async Task<IActionResult> UpdateLead([FromQuery] Guid id, [FromQuery] Guid employeeId)
        {
            var userId = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = _service.GetUser(userId);
            await _service.UpdateLead(id, employeeId);
            return Ok();
        }

        [HttpPost("/employees/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = _service.GetUser(userId);
            var result = await _service.GetAll();
            return Ok(result);
        }
    }
}