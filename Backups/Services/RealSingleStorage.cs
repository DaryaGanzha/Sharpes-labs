using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Interfaces;

namespace Backups.Services
{
    public class RealSingleStorage : IStorage
    {
        public void CreateStorage(DateTime date, List<JobObject> jobObjectList, IRepository repository)
        {
            string archiveName = "backup" + date.ToString("yyyy_MM_dd_HH_mm_ss_") + date.Millisecond + ".zip";
            var newZip = new FileStream(Path.Combine(repository.Path(), archiveName), FileMode.Create);
            var zip = new ZipArchive(newZip, ZipArchiveMode.Create);
            foreach (JobObject jobObject in jobObjectList)
            {
                zip.CreateEntryFromFile(jobObject.Path, jobObject.Name);
            }

            zip.Dispose();
        }
    }
}