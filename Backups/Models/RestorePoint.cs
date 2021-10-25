using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Backups.Models
{
    public class RestorePoint
    {
        private readonly DateTime _dateTime;
        private readonly FileInfo[] _zipFiles;
        public RestorePoint(FileInfo[] getFiles)
        {
            _zipFiles = getFiles;
            _dateTime = DateTime.Now;
        }

        public FileInfo[] GetZipFiles() => _zipFiles;
        public long GetSize() => _zipFiles.Sum(file => file.Length);
        public string GetDateTime() => _dateTime.ToString(CultureInfo.InvariantCulture);
    }
}