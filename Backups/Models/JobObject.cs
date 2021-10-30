using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.Tools;

namespace Backups.Models
{
    public class JobObject
    {
        private readonly List<FileInfo> _filesToBackup;
        public JobObject(List<FileInfo> files)
        {
            if (files.Any(f => !f.Exists)) throw new BackupsException("file doesnt exist");
            _filesToBackup = files;
        }

        public List<FileInfo> GetFiles() => _filesToBackup.ToList();

        internal void DeleteFile(FileInfo fileToDelete)
        {
            foreach (var file in _filesToBackup)
            {
                if (file.FullName == fileToDelete.FullName)
                {
                    _filesToBackup.Remove(file);
                    return;
                }
            }
        }

        internal void AddFile(FileInfo file)
        {
            _filesToBackup.Add(file);
        }
    }
}