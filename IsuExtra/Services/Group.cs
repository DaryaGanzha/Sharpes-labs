using System.Collections.Generic;

namespace IsuExtra.Services
{
    public class Group
    {
        public Group(GroupName groupName, int maxStudentInGroup)
        {
            GroupName = groupName;
            MaxStudentInGroup = maxStudentInGroup;
        }

        public int MaxStudentInGroup { get; }
        public GroupName GroupName { get; }
        public List<BasicLesson> BasicLessonsList { get; } = new List<BasicLesson>();
    }
}