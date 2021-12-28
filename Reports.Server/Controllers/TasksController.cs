using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reports.DAL.Entities;
using Reports.DAL.Statuses;
using Reports.Server.Database;
using Reports.Server.Services;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskModelService _service;

        public TasksController(ITaskModelService service)
        {
            _service = service;
        }

        [HttpPost("/tasks/Create")]
        public async Task<TaskModel> Create([FromQuery] string name, [FromQuery] Guid employeeId,
            [FromQuery] Guid finalReportId)
        {
            var userId = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = _service.GetUser(userId);
            return await _service.CreateTask(name, employeeId, finalReportId);
        }

        [HttpDelete("/tasks/Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = _service.GetUser(userId);
            await _service.Delete(id);
            return Ok();
        }

        [HttpGet("/tasks/GetTaskById")]
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

        [HttpGet("/tasks/GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] Guid employeeId, Guid finalReportId)
        {
            var userId = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = _service.GetUser(userId);
            var result = await _service.GetAll(employeeId, finalReportId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("/tasks/GetTaskByTime")]
        public async Task<IActionResult> GetTaskByTime([FromQuery] DateTime time)
        {
            var userId = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = _service.GetUser(userId);
            var result = await _service.GetTaskByTime(time);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("/tasks/GetLastChangeTime")]
        public async Task<IActionResult> GetLastChangeTimeByTaskId([FromQuery] Guid id)
        {
            var userId = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = _service.GetUser(userId);
            var result = await _service.GetLastChangeTimeByTaskId(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPatch("/tasks/UpdateStatus")]
        public async Task<IActionResult> UpdateTaskStatus([FromQuery] Guid id, [FromQuery] TaskModelStatus status)
        {
            var userId = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = _service.GetUser(userId);
            await _service.UpdateTaskStatus(id, status);
            return Ok();
        }

        [HttpPatch("/tasks/UpdateComment")]
        public async Task<IActionResult> UpdateTaskComment([FromQuery] Guid id, [FromQuery] string newComment)
        {
            var userId = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = _service.GetUser(userId);
            await _service.UpdateTaskComment(id, newComment);
            return Ok();
        }

        [HttpPatch("/tasks/UpdateEmployee")]
        public async Task<IActionResult> UpdateTaskEmployee([FromQuery] Guid id, [FromQuery] Guid newEmployeeId)
        {
            var userId = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = _service.GetUser(userId);
            await _service.UpdateTaskEmployee(id, newEmployeeId);
            return Ok();
        }

        [HttpGet("/tasks/GetAllTasksByEmployee")]
        public async Task<IActionResult> GetAllTasksByEmployeeId([FromQuery] Guid employeeId)
        {
            var userId = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = _service.GetUser(userId);
            var result = await _service.GetAllTasksByEmployeeId(employeeId);
            return Ok(result);
        }

        [HttpGet("/tasks/GetAllTasksByLead")]
        public async Task<IActionResult> GetAllTasksByLead(Guid leadId)
        {
            var userId = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = _service.GetUser(userId);
            var result = _service.GetAllTasksByLead(leadId);
            return Ok(result);
        }
    }
}