using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.Json.Serialization;
using BackupsExtra.Strategies;
using BackupsExtra.Tools;
using Serilog;

namespace BackupsExtra.Models
{
    public class BackupJob
    {
        private readonly int defaultCountLimit = 10;
        private readonly DateTime _defaultDateTimeLimit = new DateTime(2021, 11, 25);
        private IAlgorithm _algorithm = new SingleStorage(); // default
        private Logs _logs;
        public BackupJob(
            JobObject jobObject,
            string name,
            string repositoryPath,
            LogsVariation logsVariation,
            List<RestorePoint> restorePoints = null)
        {
            RepositoryPath = repositoryPath;
            Name = name;
            RestorePoints = restorePoints ?? new List<RestorePoint>();
            JobObject = jobObject;
            LogsVariation = logsVariation;
            _logs = new Logs(LogsVariation);
            var repository = new DirectoryInfo(RepositoryPath);
            try
            {
                if (!repository.Exists)
                    repository.Create();
            }
            catch (Exception e)
            {
                Console.WriteLine("cant create", e.ToString());
            }

            if (repository.GetDirectories().FirstOrDefault(dir => dir.Name == Name) == null)
            {
                BackupJobDirectory = repository.CreateSubdirectory(Name);
            }
        }

        public JobObject JobObject { get; }
        public string Name { get; }
        public string RepositoryPath { get; }

        [JsonIgnore]
        public DirectoryInfo BackupJobDirectory { get; }
        public List<RestorePoint> RestorePoints { get; }

        public LogsVariation LogsVariation { get; }

        public void AddFile(FileInfo fileToAdd)
        {
            if (!fileToAdd.Exists) throw new BackupsExtraException("this file doesnt exist");
            if (JobObject.FilesToBackup.FirstOrDefault(f => f.FullName == fileToAdd.FullName) != null)
                throw new BackupsExtraException("file already exists");

            JobObject.AddFile(fileToAdd);
            Log.Information("add File to jobObject");
        }

        public void DeleteFile(FileInfo fileToDelete)
        {
            if (!fileToDelete.Exists) throw new BackupsExtraException("this file doesnt exist");
            if (JobObject.FilesToBackup.FirstOrDefault(f => f.FullName == fileToDelete.FullName) == null)
                throw new BackupsExtraException("file already exists");

            JobObject.DeleteFile(fileToDelete);
            Log.Information("delete file fromm jobObject");
        }

        public void SetAlgorithm(IAlgorithm algorithm)
        {
            _algorithm = algorithm;
            Log.Information("set algorithm");
        }

        public RestorePoint CreateRestorePoint()
        {
            if (RestorePoints.Count >= defaultCountLimit || BackupJobDirectory.GetDirectories().
                    Any(s => Directory.GetCreationTime(s.FullName) > _defaultDateTimeLimit))
                GodsMerge();

            RestorePoint restorePoint = _algorithm.CreateBackUp(JobObject, BackupJobDirectory, BackupJobDirectory.GetDirectories().Length);
            RestorePoints.Add(restorePoint);
            Log.Information("create new RestorePoint");
            return restorePoint;
        }

        private void GodsMerge()
        {
            Log.Information("add File to jobObject");
            var newZipFiles = RestorePoints.First().ZipFiles;
            for (int i = 1; i < RestorePoints.Count; i++)
            {
                if (RestorePoints[i].ZipFiles[0].Name == "zipFile.zip") continue;
                foreach (var zipFile in RestorePoints[i].ZipFiles)
                {
                    if (newZipFiles.Any(z => z.Name == zipFile.Name))
                        newZipFiles.RemoveAll(z => z.Name == zipFile.Name);
                    newZipFiles.Add(zipFile);
                }
            }

            if (newZipFiles.Count == 0) return;

            var mergedDirectory = BackupJobDirectory.CreateSubdirectory("RestorePoint_" + RestorePoints.Count + "(Merged)");
            RestorePoints.Clear();
            newZipFiles.ForEach(z => File.Copy(z.FullName, mergedDirectory + "/" + z.Name));

            foreach (var dir in BackupJobDirectory.GetDirectories())
            {
                if (dir.Name == mergedDirectory.Name) continue;
                Directory.Delete(dir.FullName, true);
            }

            var mergedRestorePoint = new RestorePoint(newZipFiles.Select(z => z.FullName).ToList(), "mergedRestorePoint");
            RestorePoints.Add(mergedRestorePoint);
        }
    }
}