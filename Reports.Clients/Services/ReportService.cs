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
    public class ReportService : IReportService
    {
        public void CreateReport(Guid employeeId, Guid finalReportId)
        {
            var request = WebRequest.Create($"https://localhost:5001/reports/Create/?employeeId={employeeId}&&finalReportId={finalReportId}");
            request.Method = WebRequestMethods.Http.Post;
            WebResponse response = request.GetResponse();
            
            Stream responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            string responseString = readStream.ReadToEnd();
            
            Report report = JsonConvert.DeserializeObject<Report>(responseString);

            Console.WriteLine("Created Report:");
            Console.WriteLine($"Id: {report.Id}");
            Console.WriteLine($"Content: {report.Content}");
            Console.WriteLine($"Status: {report.Status}");
            Console.WriteLine($"CreationTime: {report.CreationTime}");
            Console.WriteLine($"Id: {report.EmployeeId}");
            Console.WriteLine($"LastCommitDate: {report.LastCommitDate}");
        }
        
        public void Delete(Guid id)
        {
            WebRequest request = HttpWebRequest.Create($"https://localhost:5001/reports/Delete/?id={id}");
            request.Method = WebRequestMethods.Http.Get;
            WebResponse response = request.GetResponse();
            
            Stream responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            string responseString = readStream.ReadToEnd();

            Console.WriteLine("Deleted");
        }
        
        public void FindReportById(Guid id)
        {
            var request = WebRequest.Create($"https://localhost:5001/reports/GetById/?id={id}");
            request.Method = WebRequestMethods.Http.Get;

            try
            {
                WebResponse response = request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                string responseString = readStream.ReadToEnd();

                Report report = JsonConvert.DeserializeObject<Report>(responseString);
                
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
                Console.WriteLine("report was not found");
                Console.Error.WriteLine(e.Message);
            }
        }
        
        public void GetReportStatus(Guid id)
        {
            WebRequest request = HttpWebRequest.Create($"https://localhost:5001/reports/Status/?id={id}");
            request.Method = WebRequestMethods.Http.Get;

            try
            {
                WebResponse response = request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                string responseString = readStream.ReadToEnd();

                ReportStatus reportStatus = JsonConvert.DeserializeObject<ReportStatus>(responseString);
                
                Console.WriteLine($"ReportStatus: {reportStatus}");;
            }
            catch (WebException e)
            {
                Console.WriteLine("Employee was not found");
                Console.Error.WriteLine(e.Message);
            }
        }
        
        public void GetAllReports()
        {
            var request = WebRequest.Create($"https://localhost:5001/reports/GetAll");
            request.Method = WebRequestMethods.Http.Get;

            try
            {
                WebResponse response = request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                string responseString = readStream.ReadToEnd();

                List<Report> reports = JsonConvert.DeserializeObject<List<Report>>(responseString);

                foreach (Report report in reports)
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
                Console.WriteLine("report was not found");
                Console.Error.WriteLine(e.Message);
            }
        }
        
        public void UpdateContent(Guid id, string content)
        {
            var request = WebRequest.Create($"https://localhost:5001/UpdateContent/?id={id}&&content={content}");
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
                Console.WriteLine("report was not found");
                Console.Error.WriteLine(e.Message);
            }
        }
        
        public void UpdateStatus(Guid id, ReportStatus status)
        {
            var request = WebRequest.Create($"https://localhost:5001/UpdateStatus/?id={id}&&status={status}");
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
                Console.WriteLine("report was not found");
                Console.Error.WriteLine(e.Message);
            }
        }
    }
}