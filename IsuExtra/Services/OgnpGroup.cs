using System;
using System.Collections.Generic;

namespace IsuExtra.Services
{
    public class OgnpGroup : IEquatable<OgnpGroup>
    {
        public OgnpGroup(string ognpGroupName, Ognp ognp, int maxNamberOfStudent)
        {
            Ognp = ognp;
            MaxNumberOfStudent = maxNamberOfStudent;
            OgnpGroupName = ognpGroupName;
        }

        public string OgnpGroupName { get; }
        public Ognp Ognp { get; }
        public int MaxNumberOfStudent { get; }
        public List<OgnpLesson> OgnpLessonsList { get; private set; } = new List<OgnpLesson>();

        public void AddLessonList(List<OgnpLesson> classList)
        {
            OgnpLessonsList = classList;
        }

        public bool Equals(OgnpGroup other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return OgnpGroupName == other.OgnpGroupName && Equals(Ognp, other.Ognp) && MaxNumberOfStudent == other.MaxNumberOfStudent && Equals(OgnpLessonsList, other.OgnpLessonsList);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            return Equals((OgnpGroup)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(OgnpGroupName, Ognp, MaxNumberOfStudent, OgnpLessonsList);
        }
    }
}