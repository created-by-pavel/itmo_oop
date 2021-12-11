using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Reports.Clients.Interfaces;
using Reports.DAL.Entities;

namespace Reports.Clients
{
    public class FinalReportService : IFinalReportService
    {
        public void CreateReport(Guid teamLeadId)
        {
            // Запрос к серверу
            var request = WebRequest.Create($"https://localhost:5001/finalReports/Create/?teamLeadId={teamLeadId}");
            request.Method = WebRequestMethods.Http.Post;
            WebResponse response = request.GetResponse();

            // Чтение ответа
            Stream responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            string responseString = readStream.ReadToEnd();

            // Десериализация (перевод JSON'a к C# классу)
            FinalReport finalReport = JsonConvert.DeserializeObject<FinalReport>(responseString);

            Console.WriteLine("Created finalReport:");
            Console.WriteLine($"Id: {finalReport.Id}");
            Console.WriteLine($"CreationTime: {finalReport.CreationTime}");
            Console.WriteLine($"TeamLead: {finalReport.TeamLeadId}");
        }

        public void DoFinalReport(Guid id)
        {
            var request = WebRequest.Create($"https://localhost:5001/finalReports/Do/?tid={id}");
            request.Method = WebRequestMethods.Http.Get;
            WebResponse response = request.GetResponse();

            // Чтение ответа
            Stream responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            string responseString = readStream.ReadToEnd();

            // Десериализация (перевод JSON'a к C# классу)
            List<Report> reports = JsonConvert.DeserializeObject<List<Report>>(responseString);

            foreach (Report report in reports)
            {
                Console.WriteLine("reports:");
                Console.WriteLine($"Id: {report.Id}");
                Console.WriteLine($"Content: {report.Content}");
                Console.WriteLine($"Status: {report.Status}");
                Console.WriteLine($"CreationTime: {report.CreationTime}");
                Console.WriteLine($"Id: {report.EmployeeId}");
                Console.WriteLine($"LastCommitDate: {report.LastCommitDate}/n");
            }
        }

        public void FindFinalReportById(Guid id)
        {
            var request = WebRequest.Create($"https://localhost:5001/finalReports/GetById/?id={id}");
            request.Method = WebRequestMethods.Http.Get;

            try
            {
                WebResponse response = request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                string responseString = readStream.ReadToEnd();

                FinalReport? finalReport = JsonConvert.DeserializeObject<FinalReport>(responseString);

                Console.WriteLine("finalReport was found:");
                Console.WriteLine($"Id: {finalReport.Id}");
                Console.WriteLine($"CreationTime: {finalReport.CreationTime}");
                Console.WriteLine($"TeamLead: {finalReport.TeamLeadId}");
            }
            catch (WebException e)
            {
                Console.WriteLine("finalReport was not found");
                Console.Error.WriteLine(e.Message);
            }
        }
        
        public void GetAll()
        {
            var request = WebRequest.Create($"https://localhost:5001/finalReports/GetAll");
            request.Method = WebRequestMethods.Http.Get;

            try
            {
                WebResponse response = request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                string responseString = readStream.ReadToEnd();
                List<FinalReport> finalReports = JsonConvert.DeserializeObject<List<FinalReport>>(responseString);

                foreach (FinalReport finalReport in finalReports)
                {
                    Console.WriteLine("Created finalReport:");
                    Console.WriteLine($"Id: {finalReport.Id}");
                    Console.WriteLine($"CreationTime: {finalReport.CreationTime}");
                    Console.WriteLine($"TeamLead: {finalReport.TeamLeadId}");
                }
            }
            catch (WebException e)
            {
                Console.WriteLine("finalReports was not found");
                Console.Error.WriteLine(e.Message);
            }
        }
    }
}