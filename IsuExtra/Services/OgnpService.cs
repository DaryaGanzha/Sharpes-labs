using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace IsuExtra.Services
{
    public class OgnpService
    {
        private List<Ognp> _ognpList = new List<Ognp>();
        private List<OgnpGroup> _ognpGroupList = new List<OgnpGroup>();

        public Ognp AddOgnp(string nameOgnp, MegaFaculty faculty)
        {
            var newOgnp = new Ognp(nameOgnp, faculty);
            _ognpList.Add(newOgnp);
            return newOgnp;
        }

        public OgnpGroup AddOgnpGroup(string ognpGroupName, Ognp ognp, int maxNumberStudent)
        {
            var newOgnpGroup = new OgnpGroup(ognpGroupName, ognp, maxNumberStudent);
            _ognpGroupList.Add(newOgnpGroup);
            return newOgnpGroup;
        }

        public OgnpGroup AddNewOgnpClass(OgnpGroup group, List<OgnpLesson> classList)
        {
            var newOgnpGroup = new OgnpGroup(group.OgnpGroupName, group.Ognp, group.MaxNumberOfStudent);
            newOgnpGroup.AddLessonList(classList);
            return newOgnpGroup;
        }

        public List<Ognp> GetOgnpList() => _ognpList;
        public List<OgnpGroup> GetOgnpGroup() => _ognpGroupList;
    }
}