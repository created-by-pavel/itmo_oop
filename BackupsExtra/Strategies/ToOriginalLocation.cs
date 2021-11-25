using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using BackupsExtra.Models;

namespace BackupsExtra.Strategies
{
    public class ToOriginalLocation : IRecoveryAlgorithm
    {
        public void Recovery(RestorePoint restorePoint, List<DirectoryInfo> directoriesToRecover)
        {
            for (int i = 0; i < restorePoint.ZipFiles.Count; i++)
            {
                ZipFile.ExtractToDirectory(
                    restorePoint.ZipFiles[0].FullName,
                    directoriesToRecover[i].FullName,
                    true);
            }
        }
    }
}