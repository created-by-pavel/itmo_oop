using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reports.DAL.Entities;
using Reports.DAL.Statuses;
using Reports.Server.Database;

namespace Reports.Server.Services
{
    public class TaskModelService : ITaskModelService
    {
        private readonly ReportsDatabaseContext _context;

        public TaskModelService(ReportsDatabaseContext context) {
            _context = context;
        }
        public Task<Employee> GetUser(string employeeId)
        {
            var employee = _context.Employees.SingleAsync(x => x.Id == Guid.Parse(employeeId));
            return employee;
        }

        public async Task<TaskModel> CreateTask(string name, Guid employeeId, Guid finalReportId)
        {
            var task = new TaskModel(Guid.NewGuid(), name, employeeId, finalReportId);
            var taskFromDb = await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;   
        }

        public async Task<List<TaskModel>> GetAll(Guid employeeId, Guid finalReportId)
        {
            return await _context.Tasks.Where(task => task.EmployeeId == employeeId && task.FinalReportId == finalReportId).ToListAsync();
        }

        public async Task<TaskModel> FindById(Guid id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<TaskModel> GetTaskByTime(DateTime time)
        {
            return await _context.Tasks.SingleOrDefaultAsync(t => t.CreationTime == time);
        }

        public async Task<string> GetLastChangeTimeByTaskId(Guid id)
        {
            var taskFromDb = await _context.Tasks.FindAsync(id);
            return taskFromDb.LastChangeTime.ToString(CultureInfo.CurrentCulture);
        }

        public async Task<List<TaskModel>> GetAllTasksByEmployeeId(Guid employeeId)
        {
            var employeeFromDb = await _context.Employees.FindAsync(employeeId);
            if (employeeId == null) throw new NullReferenceException();
            return await _context.Tasks.Where(task => task.EmployeeId == employeeId).ToListAsync();
        }

        public async Task UpdateTaskStatus(Guid id, TaskModelStatus newStatus)
        {
            var taskFromDb = await _context.Tasks.FindAsync(id);
            taskFromDb.LastChangeTime = DateTime.Now;
            taskFromDb.Status = newStatus;
            await _context.SaveChangesAsync();
        }

        public async Task<List<TaskModel>> GetAllTasksByLead(Guid leadId)
        {
            var leadFromDb = await _context.Employees.FindAsync(leadId);
            if (leadFromDb == null) throw new ApplicationException();
            var employeesFromDb = await _context.Employees.Where(employee => employee.LeadId == leadId).ToListAsync();
            if (employeesFromDb.Count == 0) throw new ApplicationException();
            return (from employee in employeesFromDb from task in _context.Tasks where task.EmployeeId == employee.Id select task).ToList();
        }

        public async Task UpdateTaskComment(Guid id, string newComment)
        {
            var taskFromDb = await _context.Tasks.FindAsync(id);
            if (taskFromDb.Status == TaskModelStatus.Resolved) throw new ApplicationException();
            taskFromDb.LastChangeTime = DateTime.Now;
            taskFromDb.Comment = newComment;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskEmployee(Guid id, Guid newEmployeeId)
        {
            var taskFromDb = await _context.Tasks.FindAsync(id);
            if (taskFromDb.Status == TaskModelStatus.Resolved) throw new ApplicationException();
            taskFromDb.LastChangeTime = DateTime.Now;
            taskFromDb.EmployeeId = newEmployeeId;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var taskToDelete = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(taskToDelete);
            await _context.SaveChangesAsync();
        }
    }
}