using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace BackupsExtra.Services
{
    public class BackupsExtraManager
    {
        private List<string> _backupsList = new List<string>();
        public BackupsExtraManager()
        {
        }

        public List<string> GetAllBackups()
        {
            LogModel.AddLogInfo("Get all backups");
            var dINfo = new DirectoryInfo(@"../../../../BackupsExtra/Services/BackUps");
            FileInfo[] files = dINfo.GetFiles("*.zip");
            if (files != null || files.Count() > 0)
            {
                _backupsList = new List<string>();
                for (int i = 0; i < files.Count(); i++)
                {
                    _backupsList.Add($"№: {i}. Name: {files[i].FullName} . Date: {files[i].CreationTime}");
                }
            }

            return _backupsList;
        }

        public void CreateBackupOfTheCurrentFile()
        {
            LogModel.AddLogInfo("Create a backup of the current file");
            var dINfo = new DirectoryInfo(@"../../../../BackupsExtra/Services/BackUps");
            FileInfo[] files = dINfo.GetFiles("*.zip");
            if (files != null || files.Count() > 0)
            {
                if (files.Count() > 2)
                {
                    files = files.OrderByDescending(o => o.CreationTime).ToArray();
                    for (int i = 2; i < files.Count(); i++)
                    {
                        System.IO.File.Delete(files[i].FullName);
                    }
                }

                DateTime date = DateTime.Now;
                ZipFile.CreateFromDirectory(
                    @"../../../../BackupsExtra/Services/Active/",
                    @"../../../../BackupsExtra/Services/BackUps" + $"backup{date:yyyy_MM_dd}.zip");
            }
        }

        public void DeleteBackup()
        {
            LogModel.AddLogInfo("Delete Backup");
            var dINfo = new DirectoryInfo(@"../../../../BackupsExtra/Services/BackUps");
            FileInfo[] files = dINfo.GetFiles("*.zip");
            if (files != null || files.Count() > 0)
            {
                for (int i = 0; i < files.Count(); i++)
                {
                    Console.WriteLine($"№: {i}. Name: {files[i].FullName} . Date: {files[i].CreationTime}");
                }

                Console.WriteLine("Select the file number you want to delete!");
                int number = Convert.ToInt32(Console.ReadLine());
                if (number > (files.Count() - 1))
                {
                    Console.WriteLine("This file does not exist.");
                }
                else
                {
                    FileInfo fileToDelete = files[number];
                    File.Delete(fileToDelete.FullName);
                }
            }
            else
            {
                Console.WriteLine("Before deleting the Backup, you need to create it!");
            }
        }
    }
}