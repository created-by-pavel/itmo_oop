using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Reports.DAL.Entities;
using Reports.DAL.Statuses;

namespace Reports.Clients
{
    public class TaskService
    {
        public void CreateTask(Guid teamLeadId)
        {
            // Запрос к серверу
            var request = HttpWebRequest.Create($"https://localhost:5001/tasks/Create");
            request.Method = WebRequestMethods.Http.Post;
            var response = request.GetResponse();

            // Чтение ответа
            var responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            var responseString = readStream.ReadToEnd();

            // Десериализация (перевод JSON'a к C# классу)
            var task = JsonConvert.DeserializeObject<TaskModel>(responseString);

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
        
        public void Deletet(Guid id)
        {
            // Запрос к серверу
            var request = HttpWebRequest.Create($"https://localhost:5001/tasks/Delete?{id}");
            request.Method = WebRequestMethods.Http.Get;
            var response = request.GetResponse();

            // Чтение ответа
            var responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            var responseString = readStream.ReadToEnd();

            // Десериализация (перевод JSON'a к C# классу)
            var task = JsonConvert.DeserializeObject<TaskModel>(responseString);

            Console.WriteLine("Deleted");
        }
        
        public void FindTasktById(Guid reportId)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/tasks/GetTaskById?id={reportId}");
            request.Method = WebRequestMethods.Http.Get;

            try
            {
                var response = request.GetResponse();

                var responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                var responseString = readStream.ReadToEnd();

                var task = JsonConvert.DeserializeObject<TaskModel>(responseString);
                
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
            catch (WebException e)
            {
                Console.WriteLine("Employee was not found");
                Console.Error.WriteLine(e.Message);
            }
        }
        
        public void GetTaskByTime(DateTime time)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/tasks/GetTasksByTime?time={time}");
            request.Method = WebRequestMethods.Http.Get;

            try
            {
                var response = request.GetResponse();

                var responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                var responseString = readStream.ReadToEnd();

                var task = JsonConvert.DeserializeObject<TaskModel>(responseString);
                
                Console.WriteLine($"ReportStatus = : {task}");;
            }
            catch (WebException e)
            {
                Console.WriteLine("Employee was not found");
                Console.Error.WriteLine(e.Message);
            }
        }
        
        public void GetAllTasks()
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/tasks/GetAll");
            request.Method = WebRequestMethods.Http.Get;

            try
            {
                var response = request.GetResponse();

                var responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                var responseString = readStream.ReadToEnd();

                var tasks = JsonConvert.DeserializeObject<List<TaskModel>>(responseString);

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
                Console.WriteLine("Employee was not found");
                Console.Error.WriteLine(e.Message);
            }
        }
        
        public void GetLastChangeTime(Guid id)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/tasks/GetLastChangeTime?id={id}");
            request.Method = WebRequestMethods.Http.Get;

            try
            {
                var response = request.GetResponse();

                var responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                var responseString = readStream.ReadToEnd();

                var time = JsonConvert.DeserializeObject<DateTime>(responseString);
                
                Console.WriteLine($"ReportStatus = : {time}");;
            }
            catch (WebException e)
            {
                Console.WriteLine("Employee was not found");
                Console.Error.WriteLine(e.Message);
            }
        }
        
        public void UpdateComment(string newComment)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/tasks/UpdateComment?newComment={newComment}");
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
        
        public void UpdateStatus(ReportStatus status)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/tasks/UpdateStatus?status={status}");
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
        
        public void UpdateEmployee(Guid id)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/tasks/UpdateStatus?status={id}");
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
        
        public void GetAllTasksByEmployee(Guid id)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/tasks/GetAll");
            request.Method = WebRequestMethods.Http.Get;

            try
            {
                var response = request.GetResponse();

                var responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                var responseString = readStream.ReadToEnd();

                var tasks = JsonConvert.DeserializeObject<List<TaskModel>>(responseString);

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
                Console.WriteLine("Employee was not found");
                Console.Error.WriteLine(e.Message);
            }
        }
        
        public void GetAllTasksByLead(Guid id)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/tasks/GetAll");
            request.Method = WebRequestMethods.Http.Get;

            try
            {
                var response = request.GetResponse();

                var responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                var responseString = readStream.ReadToEnd();

                var tasks = JsonConvert.DeserializeObject<List<TaskModel>>(responseString);

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
                Console.WriteLine("Employee was not found");
                Console.Error.WriteLine(e.Message);
            }
        }
        
    }
}