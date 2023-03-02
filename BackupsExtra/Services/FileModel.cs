using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace BackupsExtra.Services
{
    internal class FileModel
    {
        public string FilePath { get; set; }
        public string Name { get; set; }
        public string NameWithoutExtension { get; set; }
        public string Extension { get; set; }

        public static FileModel GetActiveBackUp()
        {
            var result = new FileModel();

            string activeFileName = @"../../../../BackupsExtra/Services/Active/Active.txt";
            if (File.Exists(activeFileName))
            {
                var fi = new FileInfo(activeFileName);
                result.FilePath = fi.FullName;
                result.Name = fi.Name;
                result.NameWithoutExtension = fi.Name.Replace(fi.Extension, string.Empty);
                result.Extension = fi.Extension;
            }

            return result;
        }

        public static void SetActiveBackUp(FileInfo file)
        {
            if (File.Exists(@"../../../../BackupsExtra/Services/Active/Active.txt"))
            {
                File.Delete(@"../../../../BackupsExtra/Services/Active.txt");
            }

            if (file.DirectoryName != null)
                ZipFile.ExtractToDirectory(file.FullName, file.DirectoryName.Replace("BackUps", "Active"));
        }
    }
}