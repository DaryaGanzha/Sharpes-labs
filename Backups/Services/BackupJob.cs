using System.Collections.Generic;
using Backups.Interfaces;

namespace Backups.Services
{
    public class BackupJob
    {
        private IRepository _repository;
        private IStorage _storage;
        private Backup _backup = new Backup();
        private List<JobObject> _jobObjectList = new List<JobObject>();

        public BackupJob(IRepository repository, IStorage storage)
        {
            _repository = repository;
            _storage = storage;
        }

        public IRepository Repository => _repository;

        public RestorePoint CreateRestorePoint()
        {
            RestorePoint restorePoint = _backup.CreateRestorePoint(_jobObjectList, _repository, _storage);
            return restorePoint;
        }

        public void AddJobObject(JobObject jobObject)
        {
            if (!_jobObjectList.Contains(jobObject))
            {
                _jobObjectList.Add(jobObject);
            }
        }

        public void DeleteJobObject(JobObject jobObject)
        {
            if (_jobObjectList.Contains(jobObject))
            {
                _jobObjectList.Remove(jobObject);
            }
        }
    }
}