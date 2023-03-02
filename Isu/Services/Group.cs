using System.Collections.Generic;

namespace Isu.Services
{
    public class Group
    {
        private GroupName _groupName;
        private int _maxStudentInGroup;

        public Group(GroupName groupName, int maxStudentInGroup)
        {
            _groupName = groupName;
            _maxStudentInGroup = maxStudentInGroup;
        }

        public GroupName GetGroupName() => _groupName;
        public int GetMaxStudentInGroup() => _maxStudentInGroup;

        /*public void AddStudent(Student student)
        {
            _studentsList.Add(student);
        }

        public void DeleteStudent(Student student)
        {
            _studentsList.Remove(student);
        }*/
    }
}