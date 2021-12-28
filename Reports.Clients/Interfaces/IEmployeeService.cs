using System;

namespace Reports.Clients.Interfaces
{
    public interface IEmployeeService
    {
        public void CreateEmployee(string name);
        public void FindEmployeeById(Guid id);
        public void Delete(Guid id);
        public void UpdateLead(Guid id, Guid employeeId);
        public void GetAll();
    }
}