using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.Entities;
using Reports.DAL.Statuses;

namespace Reports.Server.Services
{
    public interface IReportService
    {
        public Task<Employee> GetUser(string employeeId);
        public  Task<Report> CreateReport(Guid employeeId, Guid finalReportId);
        public  Task<List<Report>> GetAll();
        public  Task<Report> FindById(Guid id);
        public Task UpdateReportStatus(Guid id, ReportStatus newStatus);
        public  Task<ReportStatus> GetReportStatus(Guid id);
        public Task UpdateReportContent(Guid id, string newContent);
        public Task Delete(Guid id);

    }
}