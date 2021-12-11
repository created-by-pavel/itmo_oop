using System;
using Reports.DAL.Statuses;

namespace Reports.Clients.Interfaces
{
    public interface IReportService
    {
        public void CreateReport(Guid employeeId, Guid finalReportId);
        public void Delete(Guid id);
        public void FindReportById(Guid id);
        public void GetReportStatus(Guid id);
        public void GetAllReports();
        public void UpdateContent(Guid id, string content);
        public void UpdateStatus(Guid id, ReportStatus status);
    }
}