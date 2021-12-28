using System;

namespace Reports.Clients.Interfaces
{
    public interface IFinalReportService
    {
        public void CreateReport(Guid teamLeadId);
        public void DoFinalReport(Guid id);
        public void FindFinalReportById(Guid id);
        public void GetAll();
    }
}