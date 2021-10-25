using System;
using System.Collections.Generic;
using System.IO;
using Backups.Models;
using Backups.Strategies;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            var repository = new DirectoryInfo("/Users/pavel/Desktop/backups");
            var file1 = new FileInfo("/Users/pavel/Desktop/text.txt");
            var file2 = new FileInfo("/Users/pavel/Desktop/text2.txt");
            var files = new List<FileInfo>() { file1, file2 };
            JobObject job = new (files);
            var backupJob = new BackupJob(job, "test", repository);
            backupJob.SetAlgorithm(new SingleStorage());
            RestorePoint restorePoint = backupJob.CreateRestorePoint();
            backupJob.DeleteFile(file2);
            RestorePoint restorePoint2 = backupJob.CreateRestorePoint();
            backupJob.SetAlgorithm(new SplitStorages());
            backupJob.AddFile(file2);
            RestorePoint restorePoint3 = backupJob.CreateRestorePoint();
            var backupJob2 = new BackupJob(job, "test2", repository);
            RestorePoint anotherRestorePoint = backupJob2.CreateRestorePoint();
        }
    }
}
