using System;
using System.Collections.Generic;
using IsuExtra.Tools;

namespace IsuExtra.Services
{
    public class IsuService : IIsuService
    {
        private const int MaxNumberStudents = 20;
        private Dictionary<GroupName, Group> _groupList = new Dictionary<GroupName, Group>();
        private Dictionary<Guid, Student> _studentList = new Dictionary<Guid, Student>();
        private Dictionary<GroupName, int> _groupSize = new Dictionary<GroupName, int>();

        public Group AddGroup(GroupName name, int maxStudentInGroup)
        {
            if (_groupSize.ContainsKey(name)) throw new IsuExtraException("This group already exists.");
            _groupSize.Add(name, 0);
            var newGroup = new Group(name, maxStudentInGroup);
            _groupList.Add(name, newGroup);
            return newGroup;
        }

        public Student AddStudent(Group group, string name, int maxNumberOgnpGroup)
        {
            if (!_groupSize.ContainsKey(group.GroupName))
                throw new IsuExtraException($"Group with name {group.GroupName.GroupNumber} not exist.");
            if (_groupSize[group.GroupName] == group.MaxStudentInGroup)
                throw new IsuExtraException("There are too many students in the group.");
            _groupSize[group.GroupName] = _groupSize[group.GroupName] + 1;
            var newStudent = new Student(group.GroupName, name, maxNumberOgnpGroup);
            _studentList.Add(newStudent.Id, newStudent);
            return newStudent;
        }

        public Student GetStudent(Guid id)
        {
            if (!_studentList.ContainsKey(id)) throw new IsuExtraException($"The student with {id} does not exist.");
            return _studentList[id];
        }

        public Student FindStudent(string name)
        {
            foreach (Student student in _studentList.Values)
            {
                if (student.StudentName == name)
                {
                    return student;
                }
            }

            throw new Exception($"The student with name {name} does not exist.");
        }

        public List<Student> FindStudents(GroupName groupName)
        {
            if (!_groupList.ContainsKey(groupName))
                throw new Exception($"The group with name {groupName} does not exist.");
            var newStudentList = new List<Student>();
            foreach (Student student in _studentList.Values)
            {
                if (student.GroupName == groupName)
                {
                    newStudentList.Add(student);
                }
            }

            return newStudentList;
        }

        public List<Student> FindStudents(int courseNumber)
        {
            var studentsFromOneCourse = new List<Student>();
            foreach (Student student in _studentList.Values)
            {
                if (student.GroupName.CourseNumber == courseNumber)
                {
                    studentsFromOneCourse.Add(student);
                }
            }

            return studentsFromOneCourse;
        }

        public Group FindGroup(GroupName groupName)
        {
            if (!_groupList.ContainsKey(groupName))
                throw new Exception($"The group with name {groupName.GroupNumber} does not exist.");
            return _groupList[groupName];
        }

        public List<Group> FindGroups(int courseNumber)
        {
            var groupsFromOneCourse = new List<Group>();
            foreach (GroupName group in _groupList.Keys)
            {
                if (group.CourseNumber == courseNumber)
                {
                    groupsFromOneCourse.Add(_groupList[group]);
                }
            }

            return groupsFromOneCourse;
        }

        public Student ChangeStudentGroup(Student student, Group newGroup)
        {
            if (!_studentList.ContainsKey(student.Id))
                throw new Exception($"The student with name {student.StudentName} does not exist.");
            if (!_groupList.ContainsKey(newGroup.GroupName))
                throw new Exception($"The new group with name {newGroup.GroupName} does not exist.");

            _studentList.Remove(student.Id);
            var newStudent = new Student(newGroup.GroupName, student.StudentName, student.MaxNumberOgnpGroup);
            _studentList.Add(newStudent.Id, newStudent);
            return newStudent;
        }

        public void ChangeStudentOgnp(Student student)
        {
            if (_studentList.ContainsKey(student.Id))
            {
                _studentList[student.Id] = student;
            }
        }

        public List<Student> GetStudentList() => new List<Student>(_studentList.Values);
    }
}
