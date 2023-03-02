using System;
using System.Collections.Generic;
using System.Linq;
using IsuExtra.Tools;

namespace IsuExtra.Services
{
    public class IsuExtraService
    {
        private Dictionary<OgnpGroup, int> _listNumberStudentInOgnpGroup = new Dictionary<OgnpGroup, int>();
        private IIsuService _isuService = new IsuService();
        private OgnpService _ognpService = new OgnpService();

        public IIsuService IsuService()
        {
            _isuService = new IsuService();
            return _isuService;
        }

        public OgnpService OgnpService()
        {
            _ognpService = new OgnpService();
            return _ognpService;
        }

        public Student AddStudentInOgnpGroup(Student student, OgnpGroup ognpGroup)
        {
            if (!_listNumberStudentInOgnpGroup.ContainsKey(ognpGroup))
            {
                _listNumberStudentInOgnpGroup.Add(ognpGroup, 0);
            }

            if (_listNumberStudentInOgnpGroup.Count >= ognpGroup.MaxNumberOfStudent)
            {
                throw new IsuExtraException("There are not enough places in the OGNP group.");
            }

            if (student.StudentFaculty == ognpGroup.Ognp.FacultyName)
            {
                throw new IsuExtraException("Enrollment of a student in his faculty.");
            }

            if (student.StudentOgnpGroupList.Count < student.MaxNumberOgnpGroup && CheckingLessonsAtOneTime(student, ognpGroup))
            {
                student.StudentOgnpGroupList.Add(ognpGroup);
            }
            else
            {
                throw new IsuExtraException("The student does not have free places for the OGNP.");
            }

            return student;
        }

        public Student RemoveStudentFromOgnp(Student student, OgnpGroup opnpGroup)
        {
            if (student.StudentOgnpGroupList.Find(item => item == opnpGroup) == null)
            {
                throw new IsuExtraException("The student is not enrolled in this OGNP.");
            }
            else
            {
                for (int i = 0; i < student.StudentOgnpGroupList.Count; i++)
                {
                    if (student.StudentOgnpGroupList[i] == opnpGroup)
                    {
                        List<OgnpGroup> newList = student.StudentOgnpGroupList;
                        newList[i] = null;
                        student.ChangeStudentOgnpGroupList(newList);
                    }
                }
            }

            return student;
        }

        public List<OgnpGroup> FindOgnpGroups(Ognp ognp)
        {
            return _ognpService.GetOgnpGroup().Where(student => Equals(student.Ognp, ognp)).ToList();
        }

        public List<Student> GetListStudentsInOgnpGroup(List<Student> students, OgnpGroup ognpGroup)
        {
            var studentList = new List<Student>();
            foreach (Student student in students)
            {
                foreach (OgnpGroup studentOgnp in student.StudentOgnpGroupList)
                {
                    if (ognpGroup.Equals(studentOgnp))
                    {
                        studentList.Add(student);
                    }
                }
            }

            return studentList;
        }

        public List<Student> FindStudentsThatDidNotRegister(GroupName groupName)
        {
            return _isuService.FindStudents(groupName).Where(student => student.StudentOgnpGroupList.Count < 1).ToList();
        }

        private bool CheckingLessonsAtOneTime(Student student, OgnpGroup ognpGroup)
        {
            List<BasicLesson> newBasicLessonsList = _isuService.FindGroup(student.GroupName).BasicLessonsList;
            foreach (BasicLesson basicLesson in newBasicLessonsList)
            {
                foreach (OgnpLesson ognpLesson in ognpGroup.OgnpLessonsList)
                {
                    if (basicLesson.ClassDay == ognpLesson.ClassDay &&
                        basicLesson.ClassNumber == ognpLesson.ClassNumber)
                    {
                        return false;
                    }
                }
            }

            for (int i = 0; i < student.StudentOgnpGroupList.Count; i++)
            {
                for (int j = 0; j < student.StudentOgnpGroupList[i].OgnpLessonsList.Count; j++)
                {
                    if (ognpGroup.OgnpLessonsList[i].ClassDay == student.StudentOgnpGroupList[i].OgnpLessonsList[j].ClassDay &&
                        ognpGroup.OgnpLessonsList[i].ClassNumber == student.StudentOgnpGroupList[i].OgnpLessonsList[j].ClassNumber)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}