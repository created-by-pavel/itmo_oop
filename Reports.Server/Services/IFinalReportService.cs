using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.Entities;
using Reports.DAL.Statuses;

namespace Reports.Server.Services
{
    public interface IFinalReportService
    {
        public Task<FinalReport> CreateFinalReport(Guid teamLeadId);
        public Task<Employee> GetUser(string employeeId);
        public Task<List<Report>> DoFinalReport(Guid id);
        public Task<FinalReport> GetFinalReport(Guid id);
        public Task<List<FinalReport>> GetAll();
    }
}