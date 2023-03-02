using System.Collections.Generic;

namespace Backups.Services
{
    public class Archive
    {
        private string _archiveName;

        public Archive(string name)
        {
            _archiveName = name;
        }

        public List<string> FileNameList { get; } = new List<string>();

        public void AddFile(string fileName)
        {
            FileNameList.Add(fileName);
        }
    }
}