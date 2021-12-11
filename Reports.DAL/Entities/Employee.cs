using System;

namespace Reports.DAL.Entities
{
    public class Employee
    {
        public Employee(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
        
        private Employee() { }
        public Guid Id { get; private init; }
        public string Name { get; private init; }
        public Guid LeadId { get; set; }

    }
}