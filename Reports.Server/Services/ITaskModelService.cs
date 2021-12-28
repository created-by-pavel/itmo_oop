using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.Entities;
using Reports.DAL.Statuses;

namespace Reports.Server.Services
{
    public interface ITaskModelService
    {
        public Task<Employee> GetUser(string employeeId);
        Task<TaskModel> CreateTask(string name, Guid employeeId,  Guid finalReportId);
        public Task<List<TaskModel>> GetAll(Guid employeeId, Guid finalReportId);
        public Task<TaskModel> FindById(Guid id);
        public Task<TaskModel> GetTaskByTime(DateTime time);
        public Task<string> GetLastChangeTimeByTaskId(Guid id);
        public Task<List<TaskModel>> GetAllTasksByEmployeeId(Guid employeeId);
        public Task UpdateTaskStatus(Guid id, TaskModelStatus newStatus);
        public Task UpdateTaskComment(Guid id, string newComment);
        public Task UpdateTaskEmployee(Guid id, Guid newEmployeeId);
        public Task<List<TaskModel>> GetAllTasksByLead(Guid leadId);
        
        public Task Delete(Guid id);
    }
}