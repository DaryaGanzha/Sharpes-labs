using System.Collections.Generic;

namespace Isu.Services
{
    public interface IIsuService
    {
        Group AddGroup(GroupName name, int maxStudent);
        Student AddStudent(Group group, string name);

        Student GetStudent(int id);
        Student FindStudent(string name);
        List<Student> FindStudents(GroupName groupName);
        List<Student> FindStudents(int courseNumber);

        Group FindGroup(GroupName groupName);
        List<Group> FindGroups(int courseNumber);

        Student ChangeStudentGroup(Student student, Group newGroup);
    }
}