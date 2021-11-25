using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using BackupsExtra.Models;
using BackupsExtra.Strategies;

namespace BackupsExtra
{
    internal class Program
    {
        private static void Main()
        {
            var filesPaths = new List<string>
            {
                "/Users/pavel/Desktop/text.txt",
                "/Users/pavel/Desktop/text2.txt",
            };
            JobObject jobObject = new (filesPaths);
            var backup = new Backup("/Users/pavel/Desktop/backups");

            BackupJob backupJob = backup.AddBackupJob(jobObject, "test", LogsVariation.File);
            backupJob.SetAlgorithm(new SplitStorages());

            RestorePoint restorePoint0 = backupJob.CreateRestorePoint();
            RestorePoint restorePoint1 = backupJob.CreateRestorePoint();
            RestorePoint restorePoint2 = backupJob.CreateRestorePoint();

            var dateTimeLimit = new DateTime(2021, 11, 23);
            var directory = new DirectoryInfo("/Users/pavel/Desktop/tt");
            backup.Safe();

           /* RecoveryJob recoveryJob = backup.AddRecoveryJob(restorePoint0);
            recoveryJob.SetAlgorithm(new ToOriginalLocation());
            recoveryJob.DoRecovery();
            CleanJob cleanJob = backup.AddCleanJob(backupJob);*/

            // cleanJob.CountLimitCleaning(1);

            //
            /*
            var repository = new DirectoryInfo("/Users/pavel/Desktop/backups");
            var backup = new Backup(repository.FullName);
            backup.Load();

            // cleanJob.HybridCleaning(HybridVariation.AllLimits, 1, dateTimeLimit);

            // cleanJob.DateTimeLimitCleaning(dateTimeLimit);*/
        }
    }
}
