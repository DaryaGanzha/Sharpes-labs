using System;
using System.Collections.Generic;
using Backups.Interfaces;

namespace Backups.Services
{
    public class FakeRepository : IRepository
    {
        private readonly string _path = string.Empty;
        private readonly List<Archive> _archiveList = new List<Archive>();

        public void AddStorage(Archive zipArchive)
        {
            _archiveList.Add(zipArchive);
        }

        public string Path() => _path;
        public List<Archive> ArchiveList() => _archiveList;
    }
}