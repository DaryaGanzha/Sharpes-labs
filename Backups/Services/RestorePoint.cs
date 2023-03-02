using System;
using System.Collections.Generic;
using Backups.Interfaces;

namespace Backups.Services
{
    public class RestorePoint
    {
        public RestorePoint(DateTime date, List<JobObject> jobObjects, IStorage storage, IRepository repository)
        {
            Data = date;
            JobObjects = jobObjects;
            storage.CreateStorage(date, jobObjects, repository);
        }

        public List<JobObject> JobObjects { get; }
        public DateTime Data { get; }
    }
}