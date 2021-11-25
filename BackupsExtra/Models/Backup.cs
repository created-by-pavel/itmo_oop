using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using BackupsExtra.Tools;
using Serilog;

namespace BackupsExtra.Models
{
    public class Backup
    {
        private readonly DirectoryInfo _repository;
        private List<BackupJob> _backupJobs;
        private List<CleanJob> _cleanJobs;
        private List<RecoveryJob> _recoveryJobs;
        public Backup(string repositoryPath)
        {
            _repository = new DirectoryInfo(repositoryPath);
            _backupJobs = new List<BackupJob>();
            _cleanJobs = new List<CleanJob>();
            _recoveryJobs = new List<RecoveryJob>();
        }

        public string RepositoryPath => _repository.FullName;
        public List<BackupJob> GetBackupJobs() => _backupJobs;
        public List<CleanJob> GetCleanJobs() => _cleanJobs;
        public List<RecoveryJob> GetRecoveryJobs() => _recoveryJobs;

        public BackupJob AddBackupJob(JobObject jobObject, string name, LogsVariation logsVariation)
        {
            var backupJob = new BackupJob(jobObject, name, RepositoryPath, logsVariation);
            _backupJobs.Add(backupJob);
            return backupJob;
        }

        public CleanJob AddCleanJob(BackupJob backupJob)
        {
            var cleanJob = new CleanJob(backupJob);
            _cleanJobs.Add(cleanJob);
            return cleanJob;
        }

        public RecoveryJob AddRecoveryJob(RestorePoint restorePoint, string dir = null)
        {
            BackupJob backupJob = _backupJobs.FirstOrDefault(b => b.RestorePoints.Contains(restorePoint));
            if (backupJob == null) throw new BackupsExtraException("cant find RestorePoint");
            var recoveryJob = new RecoveryJob(restorePoint, backupJob, dir);
            _recoveryJobs.Add(recoveryJob);
            return recoveryJob;
        }

        public void Safe()
        {
            string jsonRepositoryString = JsonSerializer.Serialize(RepositoryPath);
            string jsonBackupJobsString = JsonSerializer.Serialize(_backupJobs);
            string jsonCleanJobsString = JsonSerializer.Serialize(_cleanJobs);
            string jsonRecoveryJobsString = JsonSerializer.Serialize(_recoveryJobs);

            File.WriteAllText("/Users/pavel/Desktop/jsonBackups/jsonRepository.json", jsonRepositoryString);
            File.WriteAllText("/Users/pavel/Desktop/jsonBackups/jsonBackupJobs.json", jsonBackupJobsString);
            File.WriteAllText("/Users/pavel/Desktop/jsonBackups/jsonCleanJobs.json", jsonCleanJobsString);
            File.WriteAllText("/Users/pavel/Desktop/jsonBackups/jsonRecoveryJobs.json", jsonRecoveryJobsString);
        }

        public void Load()
        {
            string jsonRepositoryString = File.ReadAllText("/Users/pavel/Desktop/jsonBackups/jsonRepository.json");
            string jsonBackupJobsString = File.ReadAllText("/Users/pavel/Desktop/jsonBackups/jsonBackupJobs.json");
            string jsonCleanJobsString = File.ReadAllText("/Users/pavel/Desktop/jsonBackups/jsonCleanJobs.json");
            string jsonRecoveryJobsString = File.ReadAllText("/Users/pavel/Desktop/jsonBackups/jsonRecoveryJobs.json");

            // _repository = JsonSerializer.Deserialize<string>(jsonRepositoryString);
            _backupJobs = JsonSerializer.Deserialize<List<BackupJob>>(jsonBackupJobsString);
            _cleanJobs = JsonSerializer.Deserialize<List<CleanJob>>(jsonCleanJobsString);
            _recoveryJobs = JsonSerializer.Deserialize<List<RecoveryJob>>(jsonRecoveryJobsString);
        }
    }
}