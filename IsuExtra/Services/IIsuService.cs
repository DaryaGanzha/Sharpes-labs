using System;
using System.Collections.Generic;

namespace IsuExtra.Services
{
    public interface IIsuService
    {
        Group AddGroup(GroupName name, int maxStudent);
        Student AddStudent(Group group, string name, int maxNumberOgnpGroup);

        Student GetStudent(Guid id);
        Student FindStudent(string name);
        List<Student> FindStudents(GroupName groupName);
        List<Student> FindStudents(int courseNumber);

        Group FindGroup(GroupName groupName);
        List<Group> FindGroups(int courseNumber);

        Student ChangeStudentGroup(Student student, Group newGroup);
        void ChangeStudentOgnp(Student student);
        List<Student> GetStudentList();
    }
}