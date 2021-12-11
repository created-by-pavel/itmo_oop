using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Reports.DAL.Entities;
using Reports.Clients.Interfaces;

namespace Reports.Clients
{
    public class EmployeeService : IEmployeeService
    {
        public void CreateEmployee(string name)
        {
            // Запрос к серверу
            var request = WebRequest.Create($"https://localhost:5001/employees/Create/?name={name}");
            request.Method = WebRequestMethods.Http.Post;

            // Чтение ответа
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            string responseString = readStream.ReadToEnd();

            // Десериализация (перевод JSON'a к C# классу)
            Employee employee = JsonConvert.DeserializeObject<Employee>(responseString);

            Console.WriteLine("Created employee:");
            Console.WriteLine($"Id: {employee.Id}");
            Console.WriteLine($"Name: {employee.Name}");
            Console.WriteLine($"Lead: {employee.LeadId}");
        }
        
        public void FindEmployeeById(Guid id)
        {
            var request = WebRequest.Create($"https://localhost:5001/employees/GetById/?id={id}");
            request.Method = WebRequestMethods.Http.Get;
            

            try
            {
                WebResponse response = request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                string responseString = readStream.ReadToEnd();
                
                Employee employee = JsonConvert.DeserializeObject<Employee>(responseString);

                Console.WriteLine("Found employee by id:");
                Console.WriteLine($"Id: {employee.Id}");
                Console.WriteLine($"Name: {employee.Name}");
                Console.WriteLine($"Lead: {employee.LeadId}");
            }
            catch (WebException e)
            {
                Console.WriteLine("Employee was not found");
                Console.Error.WriteLine(e.Message);
            }
        }
        
        public void Delete(Guid id)
        {
            var request = WebRequest.Create($"https://localhost:5001/employees/Delete?={id}");
            request.Method = WebRequestMethods.Http.Put;
            WebResponse response = request.GetResponse();
                
            Stream responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            string responseString = readStream.ReadToEnd();

            Console.WriteLine("employee was deleted");
        }
        
        public void UpdateLead(Guid id, Guid employeeId)
        {
            var request = WebRequest.Create($"https://localhost:5001/employees/NewLead?id={id}&&employeeId={employeeId}");
            request.Method = WebRequestMethods.Http.Put;

            try
            {
                WebResponse response = request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                string responseString = readStream.ReadToEnd();
                
                Console.WriteLine("Updated");
            }
            catch (WebException e)
            {
                Console.WriteLine("Employee was not found");
                Console.Error.WriteLine(e.Message);
            }
        }
        public void GetAll()
        {
            var request = WebRequest.Create($"https://localhost:5001/employees/GetAll");
            request.Method = WebRequestMethods.Http.Get;

            try
            {
                WebResponse response = request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                string responseString = readStream.ReadToEnd();
                
                List<Employee> employees = JsonConvert.DeserializeObject<List<Employee>>(responseString);
                Console.WriteLine("Found employees:");
                
                foreach (var employee in employees)
                {
                    Console.WriteLine($"Id: {employee.Id}");
                    Console.WriteLine($"Name: {employee.Name}");
                    Console.WriteLine($"Lead: {employee.LeadId}/n");
                }
            }
            catch (WebException e)
            {
                Console.WriteLine("Employee was not found");
                Console.Error.WriteLine(e.Message);
            }
        }
    }
}