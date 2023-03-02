using System.Collections.Generic;
using Backups.Services;
using NUnit.Framework;

namespace Backups.Tools
{
    public class BackupTest
    {
        private Manager _manager;

        [SetUp]
        public void Setup()
        {
            _manager = new Manager();
        }

        [Test]
        public void FakeTestCase()
        {
            var file1 = new JobObject(@"../../../Files/File1");
            var file2 = new JobObject(@"../../../Files/File2");

            _manager.CreateBackupJob(new FakeRepository(), new FakeSplitStorage());
            _manager.AddJobObject(file1);
            _manager.AddJobObject(file2);

            RestorePoint restorePoint1 = _manager.CreateRestorePoint();
            _manager.DeleteJobObject(file2);
            RestorePoint restorePoint2 = _manager.CreateRestorePoint();
            List<Archive> list;
            list = _manager.GetRepository().ArchiveList();
            Assert.AreEqual(2, list.Count);

            var fileList = new List<string>();
            foreach (Archive file in list)
            {
                fileList.AddRange(file.FileNameList);
            }

            Assert.AreEqual(3, fileList.Count);
        }
    }
}