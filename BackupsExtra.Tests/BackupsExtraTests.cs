using System;
using System.Collections.Generic;
using BackupsExtra.Services;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    public class BackupsExtraTests
    {
        [Test]
        public void GetAllBackups_BackupsReceived()
        {
            var manager = new BackupsExtraManager();
            List<string> backupsList = manager.GetAllBackups();
            Console.WriteLine(backupsList.Count);
            Assert.AreEqual(1, backupsList.Count);
        }
    }
}