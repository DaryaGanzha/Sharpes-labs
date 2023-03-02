using System;
using System.Collections.Generic;
using Backups.Interfaces;

namespace Backups.Services
{
    public class FakeSingleStorage : IStorage
    {
        public void CreateStorage(DateTime date, List<JobObject> jobObjectList, IRepository repository)
        {
            string archiveName = "backup" + date.ToString("yy_MM_dd_HH_mm_ss_") + date.Millisecond + ".zip";
            var newZipArchive = new Archive(archiveName);
            foreach (JobObject jobObject in jobObjectList)
            {
                newZipArchive.AddFile(jobObject.Name);
            }

            repository.AddStorage(newZipArchive);
        }
    }
}