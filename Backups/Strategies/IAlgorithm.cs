using System.IO;
using Backups.Models;

namespace Backups.Strategies
{
    public interface IAlgorithm
    {
        RestorePoint CreateBackUp(JobObject jobObject, DirectoryInfo backupJobDirectory, int count);
    }
}