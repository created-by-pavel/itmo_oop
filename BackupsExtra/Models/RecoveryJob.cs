using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using BackupsExtra.Strategies;
using BackupsExtra.Tools;

namespace BackupsExtra.Models
{
    public class RecoveryJob
    {
        private IRecoveryAlgorithm _algorithm = new ToOriginalLocation(); // default
        public RecoveryJob(RestorePoint restorePoint, BackupJob backupJob, string directoryToRecoverPath)
        {
            RestorePoint = restorePoint;
            BackupJob = backupJob;
            DirectoryToRecoverPath = directoryToRecoverPath;
            if (DirectoryToRecoverPath != null) DirectoryToRecover = new DirectoryInfo(DirectoryToRecoverPath);
        }

        [JsonIgnore]
        public DirectoryInfo DirectoryToRecover { get; }
        public RestorePoint RestorePoint { get; }
        public BackupJob BackupJob { get; }
        public string DirectoryToRecoverPath { get; }

        public void SetAlgorithm(IRecoveryAlgorithm algorithm)
        {
            _algorithm = algorithm;
        }

        public void DoRecovery()
        {
            if (_algorithm is ToDifferentLocation && DirectoryToRecover == null)
                throw new BackupsExtraException("you had to enter locationPath");

            var directoriesToRecover = new List<DirectoryInfo>();
            switch (_algorithm)
            {
                case ToDifferentLocation:
                    directoriesToRecover.Add(DirectoryToRecover);
                    _algorithm.Recovery(RestorePoint, directoriesToRecover);
                    return;
                case ToOriginalLocation:
                    directoriesToRecover.AddRange(BackupJob.JobObject.FilesToBackup.
                        Select(file => new DirectoryInfo(Path.GetDirectoryName(file.FullName))));
                    _algorithm.Recovery(RestorePoint, directoriesToRecover);
                    return;
            }
        }
    }
}