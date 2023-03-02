namespace Isu.Services
{
    public class Student
    {
        private string _studentName;
        private GroupName _groupName;
        private int _id;

        public Student(GroupName groupName, string studentName)
        {
            _groupName = groupName;
            _studentName = studentName;

            var newId = Id.GetInstance();
            _id = newId.GetId();
        }

        public string GetStudentName() => _studentName;
        public GroupName GetGroupName() => _groupName;
        public int GetId() => _id;

        public void ChangeStudentGroup(GroupName newGroup)
        {
            _groupName = newGroup;
        }
    }
}