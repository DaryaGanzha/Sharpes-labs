namespace IsuExtra.Services
{
    public class Ognp
    {
        public Ognp(string ognpName, MegaFaculty faculty)
        {
            OgnpName = ognpName;
            FacultyName = faculty;
        }

        public string OgnpName { get; }
        public MegaFaculty FacultyName { get; }
    }
}