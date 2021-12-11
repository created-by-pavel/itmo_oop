using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reports.DAL.Entities;
using Reports.DAL.Statuses;
using Reports.Server.Database;

namespace Reports.Server.Services
{
    public class FinalReportService : IFinalReportService
    {
        private readonly ReportsDatabaseContext _context;

        public FinalReportService(ReportsDatabaseContext context) {
            _context = context;
        }

        public async Task<FinalReport> CreateFinalReport(Guid teamLeadId)
        {
            var finalReport = new FinalReport(Guid.NewGuid(), teamLeadId);
            await _context.FinalReports.AddAsync(finalReport);
            await _context.SaveChangesAsync();
            return finalReport;
        }
        
        public Task<Employee> GetUser(string employeeId)
        {
            var employee = _context.Employees.SingleAsync(x => x.Id == Guid.Parse(employeeId));
            return employee;
        }
        
        public async Task<List<FinalReport>> GetAll()
        {
            return await _context.FinalReports.ToListAsync();
        }

        public async Task<FinalReport> GetFinalReport(Guid id)
        {
            return await _context.FinalReports.FindAsync(id);
        }

        public async Task<List<Report>> DoFinalReport(Guid id)
        {
            return _context.Reports.Where(report => report.FinalReportId == id).ToList();
        }
    }
}