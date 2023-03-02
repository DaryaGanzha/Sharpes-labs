using System.Collections.Generic;
using Backups.Services;

namespace Backups.Interfaces
{
    public interface IRepository
    {
        public string Path();
        public void AddStorage(Archive zipArchive);
        public List<Archive> ArchiveList();
    }
}