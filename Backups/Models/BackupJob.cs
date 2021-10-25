using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.Strategies;
using Backups.Tools;

namespace Backups.Models
{
    public class BackupJob
    {
        private readonly List<RestorePoint> _restorePoints = new List<RestorePoint>();
        private readonly JobObject _jobObject;
        private readonly DirectoryInfo _backupJobDirectory;
        private IAlgorithm _algorithm = new SingleStorage(); // default
        public BackupJob(JobObject jobObject, string name, DirectoryInfo repository)
        {
            _jobObject = jobObject;
            if (!repository.Exists) repository.Create();
            if (repository.GetDirectories().FirstOrDefault(dir => dir.Name == name) == null)
            {
                _backupJobDirectory = repository.CreateSubdirectory(name);
            }
        }

        public void AddFile(FileInfo fileToAdd)
        {
            if (!fileToAdd.Exists) throw new BackupsException("this file doesnt exist");
            if (_jobObject.GetFiles().FirstOrDefault(f => f.FullName == fileToAdd.FullName) == null)
            {
                _jobObject.AddFile(fileToAdd);
                return;
            }

            throw new BackupsException("file already exists");
        }

        public void DeleteFile(FileInfo fileToDelete)
        {
            if (!fileToDelete.Exists) throw new BackupsException("this file doesnt exist");
            if (_jobObject.GetFiles().FirstOrDefault(f => f.FullName == fileToDelete.FullName) != null)
            {
                _jobObject.DeleteFile(fileToDelete);
                return;
            }

            throw new BackupsException("file already exists");
        }

        public void SetAlgorithm(IAlgorithm algorithm)
        {
            _algorithm = algorithm;
        }

        public RestorePoint CreateRestorePoint()
        {
            RestorePoint restorePoint = _algorithm.CreateBackUp(_jobObject, _backupJobDirectory, _backupJobDirectory.GetDirectories().Length);
            _restorePoints.Add(restorePoint);
            return restorePoint;
        }

        public DirectoryInfo GetBackupJobDirectory() => _backupJobDirectory;
        public List<RestorePoint> GetRestorePoints() => _restorePoints.ToList();
    }
}