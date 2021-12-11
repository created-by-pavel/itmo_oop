using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Reports.Clients.Interfaces;
using Reports.DAL.Entities;
using Reports.DAL.Statuses;

namespace Reports.Clients
{
    public class TaskService : ITaskService
    {
        public void CreateTask(string name, Guid employeeId, Guid finalReportId)
        {
            var request = WebRequest.Create($"https://localhost:5001/tasks/Create/?name={name}&&employeeId={employeeId}&&finalReportId={finalReportId}");
            request.Method = WebRequestMethods.Http.Post;
            WebResponse response = request.GetResponse();
            
            Stream responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            string responseString = readStream.ReadToEnd();
            
            TaskModel task = JsonConvert.DeserializeObject<TaskModel>(responseString);

            Console.WriteLine("Task Created:");
            Console.WriteLine($"Id: {task.Id}");
            Console.WriteLine($"Content: {task.Name}");
            Console.WriteLine($"Status: {task.Status}");
            Console.WriteLine($"CreationTime: {task.CreationTime}");
            Console.WriteLine($"Id: {task.FinishDate}");
            Console.WriteLine($"Id: {task.LastChangeTime}");
            Console.WriteLine($"Id: {task.EmployeeId}");
            Console.WriteLine($"LastCommitDate: {task.FinalReportId}");
        }
        
        public void Delete(Guid id)
        {
            var request = WebRequest.Create($"https://localhost:5001/tasks/Delete/?id={id}");
            request.Method = WebRequestMethods.Http.Get;
            
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            string responseString = readStream.ReadToEnd();

            Console.WriteLine("Deleted");
        }
        
        public void FindTaskById(Guid id)
        {
            var request = WebRequest.Create($"https://localhost:5001/tasks/GetTaskById/?id={id}");
            request.Method = WebRequestMethods.Http.Get;

            try
            {
                WebResponse response = request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                string responseString = readStream.ReadToEnd();

                TaskModel task = JsonConvert.DeserializeObject<TaskModel>(responseString);
                
                Console.WriteLine("Task Found:");
                Console.WriteLine($"Id: {task.Id}");
                Console.WriteLine($"Content: {task.Name}");
                Console.WriteLine($"Status: {task.Status}");
                Console.WriteLine($"CreationTime: {task.CreationTime}");
                Console.WriteLine($"Id: {task.FinishDate}");
                Console.WriteLine($"Id: {task.LastChangeTime}");
                Console.WriteLine($"Id: {task.EmployeeId}");
                Console.WriteLine($"LastCommitDate: {task.FinalReportId}");
            }
            catch (WebException e)
            {
                Console.WriteLine("Task was not found");
                Console.Error.WriteLine(e.Message);
            }
        }
        
        public void GetTaskByTime(DateTime time)
        {
            var request = WebRequest.Create($"https://localhost:5001/tasks/GetTasksByTime/?time={time}");
            request.Method = WebRequestMethods.Http.Get;

            try
            {
                WebResponse response = request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                string responseString = readStream.ReadToEnd();

                TaskModel task = JsonConvert.DeserializeObject<TaskModel>(responseString);
                
                Console.WriteLine("Task:");
                Console.WriteLine($"Id: {task.Id}");
                Console.WriteLine($"Content: {task.Name}");
                Console.WriteLine($"Status: {task.Status}");
                Console.WriteLine($"CreationTime: {task.CreationTime}");
                Console.WriteLine($"Id: {task.FinishDate}");
                Console.WriteLine($"Id: {task.LastChangeTime}");
                Console.WriteLine($"Id: {task.EmployeeId}");
                Console.WriteLine($"LastCommitDate: {task.FinalReportId}");
                
            }
            catch (WebException e)
            {
                Console.WriteLine("Task was not found");
                Console.Error.WriteLine(e.Message);
            }
        }
        
        public void GetAllTasks()
        {
            var request = WebRequest.Create($"https://localhost:5001/tasks/GetAll");
            request.Method = WebRequestMethods.Http.Get;

            try
            {
                WebResponse response = request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                string responseString = readStream.ReadToEnd();

                List<TaskModel> tasks = JsonConvert.DeserializeObject<List<TaskModel>>(responseString);

                foreach (var task in tasks)
                {
                    Console.WriteLine("Created Report:");
                    Console.WriteLine($"Id: {task.Id}");
                    Console.WriteLine($"Content: {task.Name}");
                    Console.WriteLine($"Status: {task.Status}");
                    Console.WriteLine($"CreationTime: {task.CreationTime}");
                    Console.WriteLine($"Id: {task.FinishDate}");
                    Console.WriteLine($"Id: {task.LastChangeTime}");
                    Console.WriteLine($"Id: {task.EmployeeId}");
                    Console.WriteLine($"LastCommitDate: {task.FinalReportId}");
                }
            }
            catch (WebException e)
            {
                Console.WriteLine("Tasks was not found");
                Console.Error.WriteLine(e.Message);
            }
        }
        
        public void GetLastChangeTime(Guid id)
        {
            var request = WebRequest.Create($"https://localhost:5001/tasks/GetLastChangeTime/?id={id}");
            request.Method = WebRequestMethods.Http.Get;

            try
            {
                WebResponse response = request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                string responseString = readStream.ReadToEnd();

                DateTime time = JsonConvert.DeserializeObject<DateTime>(responseString);
                
                Console.WriteLine($"LastChangeTime: {time}");;
            }
            catch (WebException e)
            {
                Console.WriteLine("Task was not found");
                Console.Error.WriteLine(e.Message);
            }
        }
        
        public void UpdateComment(Guid id, string newComment)
        {
            var request = WebRequest.Create($"https://localhost:5001/tasks/UpdateComment/?id={id}&&newComment={newComment}");
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
                Console.WriteLine("Task was not found");
                Console.Error.WriteLine(e.Message);
            }
        }
        
        public void UpdateStatus(Guid id, ReportStatus status)
        {
            var request = WebRequest.Create($"https://localhost:5001/tasks/UpdateStatus/?if={id}&&status={status}");
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
                Console.WriteLine("Task was not found");
                Console.Error.WriteLine(e.Message);
            }
        }
        
        public void UpdateEmployee(Guid id, Guid newEmployeeId)
        {
            var request = WebRequest.Create($"https://localhost:5001/tasks/UpdateEmployee/?id={id}&&status={newEmployeeId}");
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
                Console.WriteLine("Task was not found");
                Console.Error.WriteLine(e.Message);
            }
        }
        
        public void GetAllTasksByEmployee(Guid employeeId)
        {
            var request = WebRequest.Create($"https://localhost:5001/tasks/GetAllTasksByEmployee/?employeeId={employeeId}");
            request.Method = WebRequestMethods.Http.Get;

            try
            {
                WebResponse response = request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                string responseString = readStream.ReadToEnd();

                List<TaskModel> tasks = JsonConvert.DeserializeObject<List<TaskModel>>(responseString);

                foreach (var task in tasks)
                {
                    Console.WriteLine("Task:");
                    Console.WriteLine($"Id: {task.Id}");
                    Console.WriteLine($"Content: {task.Name}");
                    Console.WriteLine($"Status: {task.Status}");
                    Console.WriteLine($"CreationTime: {task.CreationTime}");
                    Console.WriteLine($"Id: {task.FinishDate}");
                    Console.WriteLine($"Id: {task.LastChangeTime}");
                    Console.WriteLine($"Id: {task.EmployeeId}");
                    Console.WriteLine($"LastCommitDate: {task.FinalReportId}/n");
                }
            }
            catch (WebException e)
            {
                Console.WriteLine("Tasks was not found");
                Console.Error.WriteLine(e.Message);
            }
        }
        
        public void GetAllTasksByLead(Guid leadId)
        {
            var request = WebRequest.Create($"https://localhost:5001/tasks/GetAllTasksByLead/?leadId={leadId}");
            request.Method = WebRequestMethods.Http.Get;

            try
            {
                WebResponse response = request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                string responseString = readStream.ReadToEnd();

                List<TaskModel> tasks = JsonConvert.DeserializeObject<List<TaskModel>>(responseString);

                foreach (var task in tasks)
                {
                    Console.WriteLine("Task:");
                    Console.WriteLine($"Id: {task.Id}");
                    Console.WriteLine($"Content: {task.Name}");
                    Console.WriteLine($"Status: {task.Status}");
                    Console.WriteLine($"CreationTime: {task.CreationTime}");
                    Console.WriteLine($"Id: {task.FinishDate}");
                    Console.WriteLine($"Id: {task.LastChangeTime}");
                    Console.WriteLine($"Id: {task.EmployeeId}");
                    Console.WriteLine($"LastCommitDate: {task.FinalReportId}/n");
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