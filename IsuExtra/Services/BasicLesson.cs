using System.Net.NetworkInformation;

namespace IsuExtra.Services
{
    public class BasicLesson
    {
        public BasicLesson(int classRoom, string teacher, int classNumber, int time)
        {
            ClassRoom = classRoom;
            Teacher = teacher;
            ClassNumber = classNumber;
            ClassDay = time;
        }

        public int ClassRoom { get; }
        public string Teacher { get; }
        public int ClassNumber { get; }
        public int ClassDay { get; }
    }
}