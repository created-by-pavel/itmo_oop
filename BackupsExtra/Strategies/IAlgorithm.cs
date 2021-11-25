using System.IO;
using BackupsExtra.Models;

namespace BackupsExtra.Strategies
{
    public interface IAlgorithm
    {
        RestorePoint CreateBackUp(JobObject jobObject, DirectoryInfo backupJobDirectory, int count);
    }
}