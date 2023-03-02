using System;
using System.Collections.Generic;
using Backups.Services;

namespace Backups.Interfaces
{
    public interface IStorage
    {
        void CreateStorage(DateTime date, List<JobObject> jobObjects, IRepository repository);
    }
}