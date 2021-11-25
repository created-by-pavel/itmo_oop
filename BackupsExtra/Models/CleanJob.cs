using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using BackupsExtra.Strategies;
using BackupsExtra.Tools;

namespace BackupsExtra.Models
{
    public class CleanJob
    {
        public CleanJob(BackupJob backupJob)
        {
            BackupJob = backupJob;
            BackupJobDirectory = backupJob.BackupJobDirectory;
        }

        [JsonIgnore]
        public DirectoryInfo BackupJobDirectory { get; }
        public BackupJob BackupJob { get; }

        public void DateTimeLimitCleaning(DateTime dateTimeLimit)
        {
            if (BackupJob.RestorePoints.Count(r => r.DateTime < dateTimeLimit) == BackupJob.RestorePoints.Count)
                throw new BackupsExtraException("all restore points will be deleted");

            foreach (var subDirectory in BackupJobDirectory.GetDirectories())
            {
                if (Directory.GetCreationTime(subDirectory.FullName) < dateTimeLimit)
                {
                    subDirectory.Delete(true);
                }
            }

            BackupJob.RestorePoints.RemoveAll(r => r.DateTime < dateTimeLimit);
        }

        public void CountLimitCleaning(int countLimit)
        {
            if (countLimit == BackupJob.RestorePoints.Count)
                throw new BackupsExtraException("all restorePoints will be deleted");

            int subdirectoriesLength = BackupJobDirectory.GetDirectories().Length - countLimit - 1;
            for (int i = BackupJobDirectory.GetDirectories().Length - 1; i >= subdirectoriesLength; i--)
            {
                BackupJobDirectory.GetDirectories()[i].Delete(true);
            }

            for (int i = 0; i < subdirectoriesLength; i++)
            {
                BackupJob.RestorePoints.Remove(BackupJob.RestorePoints[0]);
            }
        }

        public void HybridCleaning(HybridVariation variation, int countLimit, DateTime dateTimeLimit)
        {
            int subdirectoriesCountToDelete = BackupJobDirectory.GetDirectories().Length - countLimit;
            int subdirectoryIndex;

            if (variation is HybridVariation.AllLimits)
            {
                if (BackupJob.RestorePoints.Count(r => int.Parse(Regex.Match(r.Name, @"\d+").Value)
                    < subdirectoriesCountToDelete || r.DateTime < dateTimeLimit) ==
                    BackupJob.RestorePoints.Count)
                    throw new BackupsExtraException("all restorePoints will be deleted");
            }

            if (variation is HybridVariation.OneLimit)
            {
                if (BackupJob.RestorePoints.Count(r => int.Parse(Regex.Match(r.Name, @"\d+").Value)
                        < subdirectoriesCountToDelete && r.DateTime < dateTimeLimit) ==
                    BackupJob.RestorePoints.Count)
                    throw new BackupsExtraException("all restorePoints will be deleted");
            }

            switch (variation)
            {
                case HybridVariation.OneLimit:
                    foreach (var subdirectory in BackupJobDirectory.GetDirectories())
                    {
                        subdirectoryIndex = int.Parse(Regex.Match(subdirectory.Name, @"\d+").Value);

                        if (subdirectoryIndex < subdirectoriesCountToDelete
                            || Directory.GetCreationTime(subdirectory.FullName) < dateTimeLimit)
                            subdirectory.Delete(true);
                    }

                    BackupJob.RestorePoints.RemoveAll(r => int.Parse(Regex.Match(r.Name, @"\d+").Value)
                        < subdirectoriesCountToDelete || r.DateTime < dateTimeLimit);
                    return;

                case HybridVariation.AllLimits:
                    foreach (var subdirectory in BackupJobDirectory.GetDirectories())
                    {
                        subdirectoryIndex = int.Parse(Regex.Match(subdirectory.Name, @"\d+").Value);

                        if (subdirectoryIndex < subdirectoriesCountToDelete && Directory.GetCreationTime(subdirectory.FullName)
                            < dateTimeLimit)
                            subdirectory.Delete(true);
                    }

                    BackupJob.RestorePoints.RemoveAll(r => int.Parse(Regex.Match(r.Name, @"\d+").Value)
                        < subdirectoriesCountToDelete && r.DateTime < dateTimeLimit);
                    return;
            }
        }
    }
}