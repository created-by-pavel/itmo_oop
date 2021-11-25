using System.IO;
using System.IO.Compression;
using System.Linq;
using BackupsExtra.Models;

namespace BackupsExtra.Strategies
{
    public class SingleStorage : IAlgorithm
    {
        public RestorePoint CreateBackUp(JobObject jobObject, DirectoryInfo backupJobDirectory, int count)
        {
            DirectoryInfo restorePointDirectory = backupJobDirectory.CreateSubdirectory("RestorePoint_" + count);
            DirectoryInfo tmpDirectory = restorePointDirectory.CreateSubdirectory("tmp");
            foreach (var file in jobObject.FilesToBackup)
            {
               File.Copy(file.FullName, tmpDirectory + "/" + file.Name, true);
            }

            string sourceFolder = tmpDirectory.FullName;
            string targetFolder = restorePointDirectory.FullName + "/zipFile.zip";
            ZipFile.CreateFromDirectory(sourceFolder, targetFolder);
            Directory.Delete(tmpDirectory.FullName, true);
            var restorePoint = new RestorePoint(
                restorePointDirectory.GetFiles().
                    Select(f => f.FullName).ToList(), restorePointDirectory.Name);

            return restorePoint;
        }
    }
}