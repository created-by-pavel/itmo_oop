using System;
using Reports.DAL.Statuses;

namespace Reports.Clients.Interfaces
{
    public interface ITaskService
    {
        public void CreateTask(string name, Guid employeeId, Guid finalReportId);
        public void Delete(Guid id);
        public void FindTaskById(Guid id);
        public void GetTaskByTime(DateTime time);
        public void GetAllTasks();
        public void GetLastChangeTime(Guid id);
        public void UpdateComment(Guid id, string newComment);
        public void UpdateStatus(Guid id, ReportStatus status);
        public void UpdateEmployee(Guid id, Guid newEmployeeId);
        public void GetAllTasksByEmployee(Guid employeeId);
        public void GetAllTasksByLead(Guid leadId);
    }
}