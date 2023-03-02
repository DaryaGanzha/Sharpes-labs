using IsuExtra.Tools;

namespace IsuExtra.Services
{
    public class GroupName
    {
        public GroupName(MegaFaculty faculty, int program, int courseNumber, string groupNumber)
        {
            if (program is > 4 or < 3)
            {
                throw new IsuExtraException($"The program {program} does not exist.");
            }

            if (courseNumber is < 1 or > 6)
            {
                throw new IsuExtraException($"The course {courseNumber} does not exist.");
            }

            GroupNumber = groupNumber;
            CourseNumber = courseNumber;
            Faculty = faculty;
            Program = program;
        }

        public string GroupNumber { get; }
        public int CourseNumber { get; }
        public MegaFaculty Faculty { get; }
        public int Program { get; }
    }
}