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
    public class ReportService : IReportService
    {
        private readonly ReportsDatabaseContext _context;

        public ReportService(ReportsDatabaseContext context) {
            _context = context;
        }
        
        public Task<Employee> GetUser(string employeeId)
        {
            var employee = _context.Employees.SingleAsync(x => x.Id == Guid.Parse(employeeId));
            return employee;
        }

        public async Task<Report> CreateReport(Guid employeeId, Guid finalReportId)
        {
            var report = new Report(Guid.NewGuid(), employeeId, finalReportId);
            var reportFromDb = await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();
            return report;   
        }

        public async Task<List<Report>> GetAll()
        {
            return await _context.Reports.ToListAsync();
        }

        public async Task<Report> FindById(Guid id)
        {
            return await _context.Reports.FindAsync(id);
        }

        public async Task UpdateReportStatus(Guid id, ReportStatus newStatus)
        {
            var reportFromDb = await _context.Reports.FindAsync(id);
            reportFromDb.Status = newStatus;
            await _context.SaveChangesAsync();
        }

        public async Task<ReportStatus> GetReportStatus(Guid id)
        {
            var report = await _context.Reports.FindAsync(id);
            return report.Status;
        }

        public async Task UpdateReportContent(Guid id, string newContent)
        {
            var reportFromDb = await _context.Reports.FindAsync(id);
            if (reportFromDb.Status == ReportStatus.NonActive) throw new ApplicationException(); // ?
            reportFromDb.Content = newContent;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var reportToDelete = await _context.Reports.FindAsync(id);
            _context.Reports.Remove(reportToDelete);
            await _context.SaveChangesAsync();
        }
    }
}