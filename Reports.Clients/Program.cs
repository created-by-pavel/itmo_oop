using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Reports.DAL.Entities;
using Reports.Clients.Interfaces;

namespace Reports.Clients
{
    class Program
    {
        private static void Main(string[] args)
        {
            IEmployeeService _employeeService = new EmployeeService();
            IFinalReportService _finalReportService = new FinalReportService();
            IReportService _reportService = new ReportService();
            ITaskService _taskService = new TaskService();
        
            Console.WriteLine("hello, it is my web application/n");
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Add Methods");
                
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("click 0 - to create Employer");
            Console.WriteLine("click 1 - to create FinalReport");
            Console.WriteLine("click 2 - to create Report");
            Console.WriteLine("click 3 - to create Task");

            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine("Methods for Employee");
            Console.WriteLine("click 4 - to find Employee Id");
            Console.WriteLine("click 5 - to delete Employee");
            Console.WriteLine("click 6 - to update lead for Employee");
            Console.WriteLine("click 7 - to get all Employees");
                
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Methods for Report");
            Console.WriteLine("click 8 - to Delete");
            Console.WriteLine("click 9 - to Find Report By Id");
            Console.WriteLine("click q- to Get Report Status");
            Console.WriteLine("click w - to Get All Reports");
            Console.WriteLine("click e - to Update Content");
            Console.WriteLine("click r - to Update Status");
                
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Methods for Task");
            Console.WriteLine("click t - to Delete");
            Console.WriteLine("click y - to Find Task By Id");
            Console.WriteLine("click u- to Get Task by Time");
            Console.WriteLine("click i - to Get All Tasks");
            Console.WriteLine("click o - to Get Last ChangeTime");
            Console.WriteLine("click p - to Update Comment");
            Console.WriteLine("click a - to Update Status");
            Console.WriteLine("click s - to Update Employee");
            Console.WriteLine("click d - to Get All Tasks By Employee");
            Console.WriteLine("click f - to Get all Tasks By Lead");
            while (true)
            {
                var arg = Console.ReadLine();
                switch (arg)
                {
                    case "0":
                        _employeeService.CreateEmployee(Console.ReadLine());
                        break;
                    case "1":
                        _finalReportService.CreateReport(new Guid(Console.ReadLine()));
                        break;
                    case "2":
                        _reportService.CreateReport(new Guid(Console.ReadLine()), new Guid(Console.ReadLine()));
                        break;
                    case "3":
                        _taskService.CreateTask(Console.ReadLine(),new Guid(Console.ReadLine()), new Guid(Console.ReadLine()));
                        break;
                    case "5":
                }
            }
        }
    }
}