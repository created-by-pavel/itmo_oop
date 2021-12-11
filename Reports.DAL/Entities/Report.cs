using System;
using Reports.DAL.Statuses;

namespace Reports.DAL.Entities
{
    public class Report
    {
        public Report(Guid id, Guid employeeId, Guid finalReportId)
        {
            Id = id;
            FinalReportId = finalReportId;
            EmployeeId = employeeId;
            CreationTime = DateTime.Now;
            Status = ReportStatus.Active;
        }

        private Report() { }
        
        public Guid Id { get; private init; }
        public Guid EmployeeId { get; private init; }
        public Guid FinalReportId { get; private init; }
        public DateTime CreationTime { get; private init; }
        public string Content { get; set; } = " ";
        public DateTime LastCommitDate { get; set; }
        public ReportStatus Status { get; set; }
    }
}