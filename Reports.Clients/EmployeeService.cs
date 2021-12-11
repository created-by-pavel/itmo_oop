using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Reports.DAL.Entities;

namespace Reports.Clients
{
    public class EmployeeService
    {
        public void CreateEmployee(string name)
        {
            // Запрос к серверу
            var request = HttpWebRequest.Create($"https://localhost:5001/employees/Create/?name={name}");
            request.Method = WebRequestMethods.Http.Post;
            var response = request.GetResponse();

            // Чтение ответа
            var responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            var responseString = readStream.ReadToEnd();

            // Десериализация (перевод JSON'a к C# классу)
            var employee = JsonConvert.DeserializeObject<Employee>(responseString);

            Console.WriteLine("Created employee:");
            Console.WriteLine($"Id: {employee.Id}");
            Console.WriteLine($"Name: {employee.Name}");
            Console.WriteLine($"Lead: {employee.LeadId}");
        }
        
        public void FindEmployeeById(Guid id)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/employees/GetById/?id={id}");
            request.Method = WebRequestMethods.Http.Get;

            try
            {
                var response = request.GetResponse();
                
                var responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                var responseString = readStream.ReadToEnd();
                
                var employee = JsonConvert.DeserializeObject<Employee>(responseString);

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
            var request = HttpWebRequest.Create($"https://localhost:5001/employees/Delete?={id}");
            request.Method = WebRequestMethods.Http.Put;
            var response = request.GetResponse();
                
            var responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            var responseString = readStream.ReadToEnd();

            Console.WriteLine("employee was deleted");
        }
        
        public void UpdateLead(Guid id)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/employees/NewLead?id={id}");
            request.Method = WebRequestMethods.Http.Put;

            try
            {
                var response = request.GetResponse();
                var responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                var responseString = readStream.ReadToEnd();
                
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
            var request = HttpWebRequest.Create($"https://localhost:5001/employees/GetAll");
            request.Method = WebRequestMethods.Http.Get;

            try
            {
                var response = request.GetResponse();
                var responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                var responseString = readStream.ReadToEnd();
                var employees = JsonConvert.DeserializeObject<List<Employee>>(responseString);
                Console.WriteLine("Found employees:");
                
                foreach (var employee in employees)
                {
                    Console.WriteLine($"Id: {employee.Id}");
                    Console.WriteLine($"Name: {employee.Name}");
                    Console.WriteLine($"Lead: {employee.LeadId}");
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