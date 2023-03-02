using System;
using System.Collections.Generic;
using Backups.Interfaces;

namespace Backups.Services
{
    public class Backup
    {
        private readonly List<RestorePoint> _restorePointList = new List<RestorePoint>();
        public List<RestorePoint> RestorePoints => _restorePointList;

        public RestorePoint CreateRestorePoint(List<JobObject> jobObjects, IRepository repository, IStorage storage)
        {
            var restorePoint = new RestorePoint(DateTime.Now, jobObjects, storage, repository);
            _restorePointList.Add(restorePoint);
            return restorePoint;
        }
    }
}