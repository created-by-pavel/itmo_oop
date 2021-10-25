using System;
using System.IO;
using System.IO.Compression;
using Backups.Models;

namespace Backups.Strategies
{
    public class SplitStorages : IAlgorithm
    {
        public RestorePoint CreateBackUp(JobObject jobObject, DirectoryInfo backupJobDirectory, int count)
        {
            DirectoryInfo restorePointDirectory = backupJobDirectory.CreateSubdirectory("RestorePoint_" + count);
            string targetFolder = restorePointDirectory.FullName + "/zipFile.zip";
            foreach (var file in jobObject.GetFiles())
            {
                DirectoryInfo tmpDirectory = restorePointDirectory.CreateSubdirectory("tmp");
                File.Copy(file.FullName, tmpDirectory + "/" + file.Name, true);
                string fileName = file.Name[..^4];
                ZipFile.CreateFromDirectory(tmpDirectory.FullName, restorePointDirectory.FullName + "/" + fileName + ".zip");
                Directory.Delete(tmpDirectory.FullName, true);
            }

            var restorePoint = new RestorePoint(restorePointDirectory.GetFiles());
            return restorePoint;
        }
    }
}