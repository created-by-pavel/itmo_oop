using System.Collections.Generic;
using System.Linq;

namespace IsuExtra.Models
{
    public class Course
    {
        private readonly List<Group> _groups = new ();
        public List<Group> GetGroups() => _groups.ToList();

        public void AddGroup(Group group)
        {
            _groups.Add(group);
        }
    }
}