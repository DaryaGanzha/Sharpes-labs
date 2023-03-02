using System;
using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private const int MaxNumberStudents = 20;
        private Dictionary<GroupName, Group> _groupList = new Dictionary<GroupName, Group>();
        private Dictionary<int, Student> _studentList = new Dictionary<int, Student>();
        private Dictionary<GroupName, int> _groupSize = new Dictionary<GroupName, int>();

        public Group AddGroup(GroupName name, int maxStudentInGroup)
        {
            if (_groupSize.ContainsKey(name)) throw new IsuException("This group already exists.");
            _groupSize.Add(name, 0);
            var newGroup = new Group(name, maxStudentInGroup);
            _groupList.Add(name, newGroup);
            return newGroup;
        }

        public Student AddStudent(Group group, string name)
        {
            if (!_groupSize.ContainsKey(group.GetGroupName()))
                throw new IsuException($"Group with name {group.GetGroupName().GetGroupName()} not exist.");
            if (_groupSize[group.GetGroupName()] == group.GetMaxStudentInGroup())
                throw new IsuException("There are too many students in the group.");
            _groupSize[group.GetGroupName()] = _groupSize[group.GetGroupName()] + 1;
            var newStudent = new Student(group.GetGroupName(), name);
            _studentList.Add(newStudent.GetId(), newStudent);
            return newStudent;
        }

        public Student GetStudent(int id)
        {
            if (!_studentList.ContainsKey(id)) throw new IsuException($"The student with {id} does not exist.");
            return _studentList[id];
        }

        public Student FindStudent(string name)
        {
            foreach (Student i in _studentList.Values)
            {
                if (i.GetStudentName() == name)
                {
                    return i;
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
                if (student.GetGroupName() == groupName)
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
                if (student.GetGroupName().GetCourseName() == courseNumber)
                {
                    studentsFromOneCourse.Add(student);
                }
            }

            return studentsFromOneCourse;
        }

        public Group FindGroup(GroupName groupName)
        {
            if (!_groupList.ContainsKey(groupName))
                throw new Exception($"The group with name {groupName.GetGroupName()} does not exist.");
            return _groupList[groupName];
        }

        public List<Group> FindGroups(int courseNumber)
        {
            var groupsFromOneCourse = new List<Group>();
            foreach (GroupName group in _groupList.Keys)
            {
                if (group.GetCourseName() == courseNumber)
                {
                    groupsFromOneCourse.Add(_groupList[group]);
                }
            }

            return groupsFromOneCourse;
        }

        public Student ChangeStudentGroup(Student student, Group newGroup)
        {
            if (!_studentList.ContainsKey(student.GetId()))
                throw new Exception($"The student with name {student.GetStudentName()} does not exist.");
            if (!_groupList.ContainsKey(newGroup.GetGroupName()))
                throw new Exception($"The new group with name {newGroup.GetGroupName()} does not exist.");

            _studentList.Remove(student.GetId());
            var newStudent = new Student(newGroup.GetGroupName(), student.GetStudentName());
            _studentList.Add(newStudent.GetId(), newStudent);
            return newStudent;
        }
    }
}
