using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;

namespace BackupsExtra.Models
{
    public class RestorePoint
    {
        public RestorePoint(List<string> zipFilesPaths, string name, string id = null)
        {
            Id = id ?? Guid.NewGuid().ToString();
            DateTime = DateTime.Now;
            Name = name;
            ZipFilesPaths = zipFilesPaths;
            ZipFiles = new List<FileInfo>();
            ZipFilesPaths.ForEach(z => ZipFiles.Add(new FileInfo(z)));
        }

        public string Id { get; }
        public string Name { get; }
        public DateTime DateTime { get; }
        public List<string> ZipFilesPaths { get; }

        [JsonIgnore]
        public long Size => ZipFiles.Sum(file => file.Length);
        [JsonIgnore]
        public List<FileInfo> ZipFiles { get; }
    }
}