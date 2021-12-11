using System;
using System.Threading.Tasks;
using Reports.DAL.Statuses;

namespace Reports.DAL.Entities
{
    public class TaskModel
    {
        public TaskModel(Guid id, string name, Guid employeeId, Guid finalReportId)
        {
            Id = id;
            Name = name;
            EmployeeId = employeeId;
            FinalReportId = finalReportId;
            CreationTime = DateTime.Now;
        }

        private TaskModel() { }
        
        public Guid Id { get; private init; }
        public string Name { get; private init; }
        public string Comment { get; set; } = " ";
        public Guid EmployeeId { get; set; }
        public Guid FinalReportId { get; private init; }
        public DateTime LastChangeTime { get; set; }
        public DateTime CreationTime { get; private init; }
        public DateTime FinishDate { get; internal set; }
        public TaskModelStatus Status { get; set; } = TaskModelStatus.Open;
    }
}