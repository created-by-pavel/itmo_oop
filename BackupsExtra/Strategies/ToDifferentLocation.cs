using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using BackupsExtra.Models;

namespace BackupsExtra.Strategies
{
    public class ToDifferentLocation : IRecoveryAlgorithm
    {
        public void Recovery(RestorePoint restorePoint, List<DirectoryInfo> directoriesToRecover)
        {
            foreach (var zipFile in restorePoint.ZipFiles)
            {
                ZipFile.ExtractToDirectory(zipFile.FullName, directoriesToRecover.First().FullName);
            }
        }
    }
}