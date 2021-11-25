using System.IO;
using System.IO.Compression;
using System.Linq;
using BackupsExtra.Models;

namespace BackupsExtra.Strategies
{
    public class SplitStorages : IAlgorithm
    {
        public RestorePoint CreateBackUp(JobObject jobObject, DirectoryInfo backupJobDirectory, int count)
        {
            DirectoryInfo restorePointDirectory = backupJobDirectory.CreateSubdirectory("RestorePoint_" + count);
            string targetFolder = restorePointDirectory.FullName + "/zipFile.zip";
            foreach (var file in jobObject.FilesToBackup)
            {
                DirectoryInfo tmpDirectory = restorePointDirectory.CreateSubdirectory("tmp");
                File.Copy(file.FullName, tmpDirectory + "/" + file.Name, true);
                string fileName = file.Name[..^4];
                ZipFile.CreateFromDirectory(tmpDirectory.FullName, restorePointDirectory.FullName + "/" + fileName + ".zip");
                Directory.Delete(tmpDirectory.FullName, true);
            }

            var restorePoint = new RestorePoint(
                restorePointDirectory.GetFiles().Select(z => z.FullName).ToList(),
                restorePointDirectory.Name);
            return restorePoint;
        }
    }
}