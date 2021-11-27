using System.IO;
using BackupsExtra.Strategies;
using Serilog;
using Serilog.Core;

namespace BackupsExtra.Models
{
    public class Logs
    {
        public Logs(LogsVariation logsVariation)
        {
            if (logsVariation == LogsVariation.Console)
            {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Information()
                    .WriteTo.Console()
                    .CreateLogger();
            }

            if (logsVariation == LogsVariation.File)
            {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Information()
                    .WriteTo.File(
                        "/Users/pavel/Desktop/BackupLogs/logs.txt",
                        rollingInterval: RollingInterval.Day,
                        rollOnFileSizeLimit: true)
                    .CreateLogger();
            }
        }
    }
}