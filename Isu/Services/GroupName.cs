using System;
using System.Text.RegularExpressions;
using Isu.Tools;

namespace Isu.Services
{
    public class GroupName
    {
        private string _groupName;
        private int _courseName;

        public GroupName(string groupName)
        {
            if (!IsGroupAllowed(groupName))
                throw new IsuException($"The group with name {groupName} does not exist.");
            _groupName = groupName;
            _courseName = (int)char.GetNumericValue(groupName[2]);
        }

        public string GetGroupName() => _groupName;
        public int GetCourseName() => _courseName;

        private bool IsGroupAllowed(string groupName) => Regex.IsMatch(groupName, "M3[1-4][0-9][0-9]");
    }
}