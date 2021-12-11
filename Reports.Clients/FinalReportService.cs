using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Reports.DAL.Entities;

namespace Reports.Clients
{
    public class FinalReportService
    {
        public void CreateReport(Guid teamLeadId)
        {
            // Запрос к серверу
            var request = HttpWebRequest.Create($"https://localhost:5001/finalReports/Create?{teamLeadId}");
            request.Method = WebRequestMethods.Http.Post;
            var response = request.GetResponse();

            // Чтение ответа
            var responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            var responseString = readStream.ReadToEnd();

            // Десериализация (перевод JSON'a к C# классу)
            var finalReport = JsonConvert.DeserializeObject<FinalReport>(responseString);

            Console.WriteLine("Created finalReport:");
            Console.WriteLine($"Id: {finalReport.Id}");
            Console.WriteLine($"CreationTime: {finalReport.CreationTime}");
            Console.WriteLine($"TeamLead: {finalReport.TeamLeadId}");
        }

        public void DoFinalReport(Guid finalReportId)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/finalReports/Do?{finalReportId}");
            request.Method = WebRequestMethods.Http.Get;
            var response = request.GetResponse();

            // Чтение ответа
            var responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            var responseString = readStream.ReadToEnd();

            // Десериализация (перевод JSON'a к C# классу)
            var reports = JsonConvert.DeserializeObject<List<Report>>(responseString);

            foreach (var report in reports)
            {
                Console.WriteLine("reports:");
                Console.WriteLine($"Id: {report.Id}");
                Console.WriteLine($"Content: {report.Content}");
                Console.WriteLine($"Status: {report.Status}");
                Console.WriteLine($"CreationTime: {report.CreationTime}");
                Console.WriteLine($"Id: {report.EmployeeId}");
                Console.WriteLine($"LastCommitDate: {report.LastCommitDate}");
            }
        }

        public void FindFinalReportById(Guid finalReportId)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/finalReports/GetById?id={finalReportId}");
            request.Method = WebRequestMethods.Http.Get;

            try
            {
                var response = request.GetResponse();

                var responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                var responseString = readStream.ReadToEnd();

                var finalReport = JsonConvert.DeserializeObject<FinalReport>(responseString);

                Console.WriteLine("Created finalReport:");
                Console.WriteLine($"Id: {finalReport.Id}");
                Console.WriteLine($"CreationTime: {finalReport.CreationTime}");
                Console.WriteLine($"TeamLead: {finalReport.TeamLeadId}");
            }
            catch (WebException e)
            {
                Console.WriteLine("Employee was not found");
                Console.Error.WriteLine(e.Message);
            }
        }
        
        public void GetAll()
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/finalReports/GetAll");
            request.Method = WebRequestMethods.Http.Get;

            try
            {
                var response = request.GetResponse();
                var responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                var responseString = readStream.ReadToEnd();
                var finalReports = JsonConvert.DeserializeObject<List<FinalReport>>(responseString);
                Console.WriteLine("Found employees:");
                
                foreach (var finalReport in finalReports)
                {
                    Console.WriteLine("Created finalReport:");
                    Console.WriteLine($"Id: {finalReport.Id}");
                    Console.WriteLine($"CreationTime: {finalReport.CreationTime}");
                    Console.WriteLine($"TeamLead: {finalReport.TeamLeadId}");
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