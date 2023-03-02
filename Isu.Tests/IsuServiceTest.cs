using Isu.Services;
using Isu.Tools;
using NUnit.Framework;
using Isu.Services;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            //TODO: implement
            _isuService = new IsuService();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            var m3209 = new GroupName("M3209");
            Group group = _isuService.AddGroup(m3209, 20);
            Student student = _isuService.AddStudent(group, "Ivan");
            Assert.AreEqual(1, student.GetId());
            Assert.AreEqual(m3209, student.GetGroupName());
            Assert.AreEqual("Ivan", student.GetStudentName());
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            var m3209 = new GroupName("M3209");
            Group group = _isuService.AddGroup(m3209, 15);
            for (int i = 0; i < group.GetMaxStudentInGroup(); i++)
            {
                Student student = _isuService.AddStudent(group, "Ivan");
            }
            Assert.Catch<IsuException>(() =>
            {
                Student student = _isuService.AddStudent(group, "Ivan");
            });
        }

        [TestCase("N3209")]
        [TestCase("M4209")]
        [TestCase("M3509")]
        [TestCase("M33A9")]
        public void CreateGroupWithInvalidName_ThrowException(string name)
        {
            Assert.Catch<IsuException>(() =>
            {
                var groupName = new GroupName(name);
                Group group = _isuService.AddGroup(groupName, 20);
            });
        }
        
        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            var m3209 = new GroupName("M3209");
            var m3202 = new GroupName("M3202");
            Group group = _isuService.AddGroup(m3209, 20);
            Group groupNew = _isuService.AddGroup(m3202, 20);
            Student student = _isuService.AddStudent(group,"Ivan");
            
            Assert.AreEqual(m3209, student.GetGroupName());
            
            student = _isuService.ChangeStudentGroup(student, groupNew);
            
            Assert.AreEqual(m3202, student.GetGroupName());
            
            var m3302 = new GroupName("M3302");
            Group group1 = _isuService.AddGroup(m3302, 20);
            student = _isuService.ChangeStudentGroup(student, group1);
            Assert.AreEqual(3, student.GetGroupName().GetCourseName());

        }
    }
}