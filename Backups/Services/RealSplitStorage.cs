using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Interfaces;

namespace Backups.Services
{
    public class RealSplitStorage : IStorage
    {
        public void CreateStorage(DateTime date, List<JobObject> jobObjectList, IRepository repository)
        {
            string archiveName = "backup" + date.ToString("yyyy_MM_dd_HH_mm_ss_") + date.Millisecond + ".zip";
            var zipToCreate = new FileStream(Path.Combine(repository.Path(), archiveName), FileMode.Create);
            var zip = new ZipArchive(zipToCreate, ZipArchiveMode.Create);
            foreach (JobObject jobObject in jobObjectList)
            {
                string file = jobObject.ShortName + ".zip";
                var zipFileToCreate = new FileStream(Path.Combine(repository.Path(), file), FileMode.Create);
                var fileZip = new ZipArchive(zipFileToCreate, ZipArchiveMode.Create);
                fileZip.CreateEntryFromFile(jobObject.Path, jobObject.Name);
                fileZip.Dispose();
                zip.CreateEntryFromFile(Path.Combine(repository.Path(), file), file);
            }

            zip.Dispose();
        }
    }
}