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
    public class ReportService
    {
        public void CreateReport(Guid teamLeadId)
        {
            // Запрос к серверу
            var request = HttpWebRequest.Create($"https://localhost:5001/re/Create");
            request.Method = WebRequestMethods.Http.Post;
            var response = request.GetResponse();

            // Чтение ответа
            var responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            var responseString = readStream.ReadToEnd();

            // Десериализация (перевод JSON'a к C# классу)
            var report = JsonConvert.DeserializeObject<Report>(responseString);

            Console.WriteLine("Created Report:");
            Console.WriteLine($"Id: {report.Id}");
            Console.WriteLine($"Content: {report.Content}");
            Console.WriteLine($"Status: {report.Status}");
            Console.WriteLine($"CreationTime: {report.CreationTime}");
            Console.WriteLine($"Id: {report.EmployeeId}");
            Console.WriteLine($"LastCommitDate: {report.LastCommitDate}");
        }
        
        public void Deletet(Guid id)
        {
            // Запрос к серверу
            var request = HttpWebRequest.Create($"https://localhost:5001/reports/Delete?{id}");
            request.Method = WebRequestMethods.Http.Get;
            var response = request.GetResponse();

            // Чтение ответа
            var responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            var responseString = readStream.ReadToEnd();

            // Десериализация (перевод JSON'a к C# классу)
            var report = JsonConvert.DeserializeObject<Report>(responseString);

            Console.WriteLine("Deleted");
        }
        
        public void FindReportById(Guid reportId)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/reports/GetById?id={reportId}");
            request.Method = WebRequestMethods.Http.Get;

            try
            {
                var response = request.GetResponse();

                var responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                var responseString = readStream.ReadToEnd();

                var report = JsonConvert.DeserializeObject<Report>(responseString);
                
                Console.WriteLine("Created Report:");
                Console.WriteLine($"Id: {report.Id}");
                Console.WriteLine($"Content: {report.Content}");
                Console.WriteLine($"Status: {report.Status}");
                Console.WriteLine($"CreationTime: {report.CreationTime}");
                Console.WriteLine($"Id: {report.EmployeeId}");
                Console.WriteLine($"LastCommitDate: {report.LastCommitDate}");
            }
            catch (WebException e)
            {
                Console.WriteLine("Employee was not found");
                Console.Error.WriteLine(e.Message);
            }
        }
        
        public void GetReportStatus(Guid reportId)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/reports/GetById?id={reportId}");
            request.Method = WebRequestMethods.Http.Get;

            try
            {
                var response = request.GetResponse();

                var responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                var responseString = readStream.ReadToEnd();

                var reportStatus = JsonConvert.DeserializeObject<ReportStatus>(responseString);
                
                Console.WriteLine($"ReportStatus = : {reportStatus}");;
            }
            catch (WebException e)
            {
                Console.WriteLine("Employee was not found");
                Console.Error.WriteLine(e.Message);
            }
        }
        
        public void GetAllReports()
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/reports/GetAll");
            request.Method = WebRequestMethods.Http.Get;

            try
            {
                var response = request.GetResponse();

                var responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                var responseString = readStream.ReadToEnd();

                var reports = JsonConvert.DeserializeObject<List<Report>>(responseString);

                foreach (var report in reports)
                {
                    Console.WriteLine("Created Report:");
                    Console.WriteLine($"Id: {report.Id}");
                    Console.WriteLine($"Content: {report.Content}");
                    Console.WriteLine($"Status: {report.Status}");
                    Console.WriteLine($"CreationTime: {report.CreationTime}");
                    Console.WriteLine($"Id: {report.EmployeeId}");
                    Console.WriteLine($"LastCommitDate: {report.LastCommitDate}");
                }
            }
            catch (WebException e)
            {
                Console.WriteLine("Employee was not found");
                Console.Error.WriteLine(e.Message);
            }
        }
        
        public void UpdateContent(string content)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/UpdateContent?id={content}");
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
            var request = HttpWebRequest.Create($"https://localhost:5001/UpdateStatus?status={status}");
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
    }
}