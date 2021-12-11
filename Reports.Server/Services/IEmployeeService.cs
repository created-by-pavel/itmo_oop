using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface IEmployeeService
    {
        public Task<Employee> CreateEmployee(string name);
        public Task<Employee> GetUser(string employeeId);
        public Task<Employee> FindById(Guid id);
        public Task Delete(Guid id);
        public Task<List<Employee>> GetAll();
        public Task UpdateLead(Guid id, Guid leadId);
        
    }
}