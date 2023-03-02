using System.Collections.Generic;
using IsuExtra.Services;
using IsuExtra.Tools;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    public class IsuExtraTests
    {
        private IsuExtraService _extraService;

        [SetUp]
        public void Setup()
        {
            _extraService = new IsuExtraService();
        }

        [Test]
        public void AddStudentToOgnpGroup_StudentWasAddedInGroup()
        {
            Services.IIsuService isuService = _extraService.IsuService();
            var m3209 = new GroupName(MegaFaculty.BioTech, 3, 1, "01");
            Group group = isuService.AddGroup(m3209, 20);
            Student student = isuService.AddStudent(group, "Socolov Socol", 2);

            OgnpService ognpService = _extraService.OgnpService();
            Ognp ognp1 = ognpService.AddOgnp("Prog", MegaFaculty.Entrepreneurship);
            OgnpGroup ognpGroup1 = ognpService.AddOgnpGroup("ognpGroup1", ognp1, 20);
            Ognp ognp2 = ognpService.AddOgnp("Math", MegaFaculty.Mathematic);
            OgnpGroup ognpGroup2 = ognpService.AddOgnpGroup("ognpGroup2", ognp2, 25);

            var classList1 = new List<OgnpLesson>();
            var class1 = new OgnpLesson(ognpGroup1, 101, "prep1", 1, 2);
            classList1.Add(class1);
            var classList2 = new List<OgnpLesson>();
            var class2 = new OgnpLesson(ognpGroup2, 101, "prep1", 2, 2);
            classList2.Add(class2);

            ognpGroup1.AddLessonList(classList1);
            ognpGroup2.AddLessonList(classList2);

            student = _extraService.AddStudentInOgnpGroup(student, ognpGroup1);
            student = _extraService.AddStudentInOgnpGroup(student, ognpGroup2);

            Assert.AreEqual(m3209, student.GroupName);
            Assert.AreEqual("Socolov Socol", student.StudentName);
            Assert.AreEqual(ognpGroup1, student.GetStudentOgnp(0));
            Assert.AreEqual(ognpGroup2, student.GetStudentOgnp(1));
        }

        [Test]
        public void AddStudentToThreeOgnp_ThrowExceprion()
        {
            Services.IIsuService isuService = _extraService.IsuService();
            var m3209 = new GroupName(MegaFaculty.BioTech, 3, 1, "01");
            Group group = isuService.AddGroup(m3209, 20);
            Student student = isuService.AddStudent(group, "Socolov Socol", 2);

            OgnpService ognpService = _extraService.OgnpService();
            Ognp ognp1 = ognpService.AddOgnp("ognpProg", MegaFaculty.Entrepreneurship);
            OgnpGroup ognpGroup1 = ognpService.AddOgnpGroup("ognpGroup1", ognp1, 15);
            Ognp ognp2 = ognpService.AddOgnp("ognpMath", MegaFaculty.Mathematic);
            OgnpGroup ognpGroup2 = ognpService.AddOgnpGroup("ognpGroup2", ognp2, 20);
            Ognp ognp3 = ognpService.AddOgnp("ognpComp", MegaFaculty.CompTech);
            OgnpGroup ognpGroup3 = ognpService.AddOgnpGroup("ognpGroup3", ognp3, 25);

            var classList1 = new List<OgnpLesson>();
            var class1 = new OgnpLesson(ognpGroup1, 101, "prep1", 1, 2);
            classList1.Add(class1);
            var classList2 = new List<OgnpLesson>();
            var class2 = new OgnpLesson(ognpGroup1, 101, "prep1", 2, 2);
            classList2.Add(class2);
            var classList3 = new List<OgnpLesson>();
            var class3 = new OgnpLesson(ognpGroup1, 101, "prep1", 3, 2);
            classList3.Add(class3);

            ognpGroup1.AddLessonList(classList1);
            ognpGroup2.AddLessonList(classList2);
            ognpGroup3.AddLessonList(classList3);

            student = _extraService.AddStudentInOgnpGroup(student, ognpGroup1);
            student = _extraService.AddStudentInOgnpGroup(student, ognpGroup2);

            Assert.Catch<IsuExtraException>(() =>
            {
                student = _extraService.AddStudentInOgnpGroup(student, ognpGroup3);
            });
        }

        [Test]
        public void RemoveStudentFromOgnp_OgnpIsNull()
        {
            Services.IIsuService isuService = _extraService.IsuService();
            var m3209 = new GroupName(MegaFaculty.BioTech, 3, 1, "01");
            Group group = isuService.AddGroup(m3209, 20);
            Student student = isuService.AddStudent(group, "Socolov Socol", 2);

            OgnpService ognpService = _extraService.OgnpService();
            Ognp ognp1 = ognpService.AddOgnp("ognpProg", MegaFaculty.Entrepreneurship);
            OgnpGroup ognpGroup1 = ognpService.AddOgnpGroup("ognpGroup1", ognp1, 15);
            Ognp ognp2 = ognpService.AddOgnp("ognpMath", MegaFaculty.Mathematic);
            OgnpGroup ognpGroup2 = ognpService.AddOgnpGroup("ognpGroup2", ognp2, 20);

            var classList1 = new List<OgnpLesson>();
            var class1 = new OgnpLesson(ognpGroup1, 101, "prep1", 1, 2);
            classList1.Add(class1);
            var classList2 = new List<OgnpLesson>();
            var class2 = new OgnpLesson(ognpGroup1, 101, "prep1", 2, 2);
            classList2.Add(class2);

            ognpGroup1.AddLessonList(classList1);
            ognpGroup2.AddLessonList(classList2);

            student = _extraService.AddStudentInOgnpGroup(student, ognpGroup1);
            student = _extraService.AddStudentInOgnpGroup(student, ognpGroup2);

            _extraService.RemoveStudentFromOgnp(student, ognpGroup2);
            Assert.AreEqual(null, student.GetStudentOgnp(1));
        }

        [Test]
        public void FindOgnpGroupByOgnpName_OgnpGroupList()
        {
            Services.IIsuService isuService = _extraService.IsuService();
            var m3209 = new GroupName(MegaFaculty.BioTech, 3, 1, "01");
            Group group = isuService.AddGroup(m3209, 20);
            Student student = isuService.AddStudent(group, "Socolov Socol", 2);

            OgnpService ognpService = _extraService.OgnpService();
            Ognp ognp1 = ognpService.AddOgnp("ognpProg", MegaFaculty.Entrepreneurship);
            OgnpGroup ognpGroup1 = ognpService.AddOgnpGroup("ognpGroup1", ognp1, 15);
            Ognp ognp2 = ognpService.AddOgnp("ognpMath", MegaFaculty.Mathematic);
            OgnpGroup ognpGroup2 = ognpService.AddOgnpGroup("ognpGroup2", ognp2, 20);

            var classList1 = new List<OgnpLesson>();
            var class1 = new OgnpLesson(ognpGroup1, 101, "prep1", 1, 2);
            classList1.Add(class1);
            var classList2 = new List<OgnpLesson>();
            var class2 = new OgnpLesson(ognpGroup1, 101, "prep1", 2, 2);
            classList2.Add(class2);

            ognpGroup1.AddLessonList(classList1);
            ognpGroup2.AddLessonList(classList2);

            student = _extraService.AddStudentInOgnpGroup(student, ognpGroup1);
            student = _extraService.AddStudentInOgnpGroup(student, ognpGroup2);

            List<OgnpGroup> ansList = _extraService.FindOgnpGroups(ognp1);
            var expectedList = new List<OgnpGroup>();
            expectedList.Add(ognpGroup1);

            Assert.AreEqual(expectedList, ansList);
        }

        [Test]
        public void FindStudentsByOgnpGroup_StudentsList()
        {
            Services.IIsuService isuService = _extraService.IsuService();
            var m3209 = new GroupName(MegaFaculty.BioTech, 3, 1, "01");
            Group group = isuService.AddGroup(m3209, 20);
            Student student = isuService.AddStudent(group, "Socolov Socol", 2);

            OgnpService ognpService = _extraService.OgnpService();
            Ognp ognp1 = ognpService.AddOgnp("ognpProg", MegaFaculty.Entrepreneurship);
            OgnpGroup ognpGroup1 = ognpService.AddOgnpGroup("ognpGroup1", ognp1, 15);

            var classList1 = new List<OgnpLesson>();
            var class1 = new OgnpLesson(ognpGroup1, 101, "prep1", 1, 2);
            classList1.Add(class1);

            ognpGroup1.AddLessonList(classList1);

            student = _extraService.AddStudentInOgnpGroup(student, ognpGroup1);

            List<Student> ansList = _extraService.GetListStudentsInOgnpGroup(isuService.GetStudentList(), ognpGroup1);
            var expectedList = new List<Student>();
            expectedList.Add(student);

            Assert.AreEqual(expectedList, ansList);
        }

        [Test]
        public void FindNotRegisteredStudents_StudentsList()
        {
            Services.IIsuService isuService = _extraService.IsuService();
            var m3209 = new GroupName(MegaFaculty.BioTech, 3, 1, "01");
            Group group = isuService.AddGroup(m3209, 20);
            Student student = isuService.AddStudent(group, "Socolov Socol", 2);
            Student student2 = isuService.AddStudent(group, "Socolov Locos", 2);

            OgnpService ognpService = _extraService.OgnpService();
            Ognp ognp1 = ognpService.AddOgnp("ognpProg", MegaFaculty.Entrepreneurship);
            OgnpGroup ognpGroup1 = ognpService.AddOgnpGroup("ognpGroup1", ognp1, 15);
            Ognp ognp2 = ognpService.AddOgnp("ognpMath", MegaFaculty.Mathematic);
            OgnpGroup ognpGroup2 = ognpService.AddOgnpGroup("ognpGroup2", ognp2, 20);

            var classList1 = new List<OgnpLesson>();
            var class1 = new OgnpLesson(ognpGroup1, 101, "prep1", 1, 2);
            classList1.Add(class1);
            var classList2 = new List<OgnpLesson>();
            var class2 = new OgnpLesson(ognpGroup1, 101, "prep1", 2, 2);
            classList2.Add(class2);

            ognpGroup1.AddLessonList(classList1);
            ognpGroup2.AddLessonList(classList2);

            student = _extraService.AddStudentInOgnpGroup(student, ognpGroup1);
            student = _extraService.AddStudentInOgnpGroup(student, ognpGroup2);

            List<Student> answerList = _extraService.FindStudentsThatDidNotRegister(group.GroupName);
            Assert.Contains(student2, answerList);
        }

    }
}