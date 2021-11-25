using System.Collections.Generic;
using System.IO;
using BackupsExtra.Models;

namespace BackupsExtra.Strategies
{
    public interface IRecoveryAlgorithm
    {
        public void Recovery(RestorePoint restorePoint, List<DirectoryInfo> directoriesToRecover);
    }
}