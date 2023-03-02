using System;
using System.Collections.Generic;

namespace IsuExtra.Services
{
    public class Student
    {
        public Student(GroupName groupName, string studentName, int maxNumberOgnpGroup)
        {
            GroupName = groupName;
            StudentName = studentName;
            MaxNumberOgnpGroup = maxNumberOgnpGroup;
            var id = Guid.NewGuid();
            Id = id;
        }

        public string StudentName { get; }
        public GroupName GroupName { get; }
        public Guid Id { get; }
        public int MaxNumberOgnpGroup { get; }
        public List<OgnpGroup> StudentOgnpGroupList { get; set; } = new List<OgnpGroup>();
        public MegaFaculty StudentFaculty => GroupName.Faculty;

        public void ChangeStudentOgnpGroupList(List<OgnpGroup> studentOgnpGroupList)
        {
            this.StudentOgnpGroupList = studentOgnpGroupList;
        }

        public OgnpGroup GetStudentOgnp(int ognpNumber)
        {
            return this.StudentOgnpGroupList[ognpNumber];
        }
    }
}