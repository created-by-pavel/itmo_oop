using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Reports.DAL.Entities;
using Reports.Server.Controllers;
using Reports.Server.Database;

namespace Reports.Server.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ReportsDatabaseContext _context;

        public EmployeeService(ReportsDatabaseContext context) {
            _context = context;
        }

        public Task<Employee> GetUser(string employeeId)
        {
            var employee = _context.Employees.SingleAsync(x => x.Id == Guid.Parse(employeeId));
            return employee;
        }

        public async Task<Employee> CreateEmployee(string name)
        {
            var employee = new Employee(Guid.NewGuid(), name);
            var employeeFromDb = await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> FindById(Guid id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<List<Employee>> GetAll()
        {

            return await _context.Employees.ToListAsync();
        }

        public async Task Delete(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLead(Guid id, Guid leadId)
        {
            var employeeFromDb = await _context.Employees.FindAsync(id);
            employeeFromDb.LeadId = leadId;
            await _context.SaveChangesAsync();
        }
    }
}