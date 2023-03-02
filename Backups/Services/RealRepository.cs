using System.Collections.Generic;
using Backups.Interfaces;

namespace Backups.Services
{
    public class RealRepository : IRepository
    {
        private readonly List<Archive> _archiveList = new List<Archive>();
        private readonly string _path = @"D:\lab3";

        public void AddStorage(Archive zipArchive)
        {
            _archiveList.Add(zipArchive);
        }

        public string Path() => _path;
        public List<Archive> ArchiveList() => _archiveList;
    }
}