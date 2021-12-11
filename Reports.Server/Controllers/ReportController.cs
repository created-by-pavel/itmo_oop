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
using Reports.DAL.Statuses;
using Reports.Server.Services;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/reports")]
     public class ReportController : ControllerBase
     {
         private readonly IReportService _service;
         
         public ReportController(IReportService service)
         {
             _service = service;
         }
            
         [HttpPost("/Create")]
         public async Task<Report> Create([FromQuery] Guid employeeId, [FromQuery] Guid finalReportId)
         {
             var userId = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
             var user = _service.GetUser(userId);
             return await _service.CreateReport(employeeId, finalReportId);
         }
        
         [HttpDelete("/reports/Delete")]
         public async Task<IActionResult> Delete(Guid id)
         {
             var userId = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
             var user = _service.GetUser(userId);
             await _service.Delete(id);
             return Ok();
         }
        
         [HttpGet("/reports/GetBy")]
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

         [HttpPatch("/reports/UpdateStatus")]
         public async Task<IActionResult> UpdateStatus(Guid id, ReportStatus newStatus)
         {
             var userId = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
             var user = _service.GetUser(userId);
             await _service.UpdateReportStatus(id, newStatus);
             return Ok();
         }
         
         [HttpPatch("/reports/UpdateContent")]
         public async Task<IActionResult> UpdateReportContent(Guid id, string newContent)
         {
             var userId = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
             var user = _service.GetUser(userId);
             await _service.UpdateReportContent(id, newContent);
             return Ok();
         }
         
         [HttpGet("/reports/Status")]
         public async Task<IActionResult> GetStatus(Guid id)
         {
             var userId = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
             var user = _service.GetUser(userId);
             var result = _service.GetReportStatus(id);
             return Ok(result);
         }

         [HttpGet("/reports/GetAll")]
         public async Task<IActionResult> GetAll()
         {
             var userId = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
             var user = _service.GetUser(userId);
             var result = await _service.GetAll();
             return Ok(result);
         }
     }
}