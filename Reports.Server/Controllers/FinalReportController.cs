using System;
using System.Collections.Generic;
using System.Linq;
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
    [Route("/finalReports")]
    public class FinalReportController : ControllerBase
    {
        private readonly IFinalReportService _service;

        public FinalReportController(IFinalReportService service)
        { 
            _service = service;
        }

        [HttpPost("/finalReports/Create")]
        public async Task<FinalReport> Create(Guid teamLeadId) 
        { 
            var userId = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = _service.GetUser(userId);
            return await _service.CreateFinalReport(teamLeadId);
        }

        [HttpGet("/finalReports/Do")]
        public async Task<IActionResult> DoFinalReport(Guid id)
        {
            var userId = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = _service.GetUser(userId);
            var result = await _service.DoFinalReport(id);
            return Ok(result);
        }

        [HttpGet("/finalReports/GetById")]
        public async Task<IActionResult> GetFinalReport(Guid id)
        {
            var userId = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = _service.GetUser(userId);
            var result = await _service.GetFinalReport(id);
            return Ok(result);
        }

        [HttpGet("finalReports/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = _service.GetUser(userId);
            var result = await _service.GetAll();
            return Ok(result);
        }

    }
}