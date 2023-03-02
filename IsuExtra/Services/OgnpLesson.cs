using IsuExtra.Tools;

namespace IsuExtra.Services
{
    public class OgnpLesson
    {
        public OgnpLesson(OgnpGroup ognp, int classRoom, string teacherName, int classNumber, int classDay)
        {
            if (classNumber is < 1 or > 15)
            {
                throw new IsuExtraException("There is no lesson at this day.");
            }

            if (classDay is < 1 or > 6)
            {
                throw new IsuExtraException("There can not be a lesson at this day.");
            }

            Ognp = ognp;
            TeacherName = teacherName;
            ClassRoom = classRoom;
            ClassDay = classDay;
            ClassNumber = classNumber;
        }

        public int ClassNumber { get; }
        public string TeacherName { get; }
        public int ClassRoom { get; }
        public int ClassDay { get; }
        public OgnpGroup Ognp { get; }
    }
}