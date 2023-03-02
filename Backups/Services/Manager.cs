using System.Collections.Generic;
using Backups.Interfaces;

namespace Backups.Services
{
    public class Manager
    {
        private BackupJob _backupJob;

        public void CreateBackupJob(IRepository repository, IStorage storage)
        {
            _backupJob = new BackupJob(repository, storage);
        }

        public RestorePoint CreateRestorePoint()
        {
            return _backupJob.CreateRestorePoint();
        }

        public void AddJobObject(JobObject jobObject)
        {
            _backupJob.AddJobObject(jobObject);
        }

        public void DeleteJobObject(JobObject jobObject)
        {
            _backupJob.DeleteJobObject(jobObject);
        }

        public IRepository GetRepository()
        {
            return _backupJob.Repository;
        }
    }
}