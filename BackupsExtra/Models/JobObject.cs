using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using BackupsExtra.Tools;
namespace BackupsExtra.Models
{
    public class JobObject
    {
        public JobObject(List<string> filesToBackupPaths)
        {
            FilesToBackupPaths = filesToBackupPaths;
            FilesToBackup = new List<FileInfo>();
            filesToBackupPaths.ForEach(f => FilesToBackup.Add(new FileInfo(f)));
        }

        [JsonIgnore]
        public List<FileInfo> FilesToBackup { get; }

        public List<string> FilesToBackupPaths { get; }

        internal void DeleteFile(FileInfo fileToDelete)
        {
            FileInfo file = FilesToBackup.First(f => f.FullName == fileToDelete.FullName);
            FilesToBackup.Remove(file);
        }

        internal void AddFile(FileInfo file)
        {
            FilesToBackup.Add(file);
        }
    }
}