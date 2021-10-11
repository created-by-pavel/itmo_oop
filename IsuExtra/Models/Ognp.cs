using System.Collections.Generic;
using System.Linq;

namespace IsuExtra.Models
{
    public class Ognp
    {
        private string _ognpName;
        private List<OgnpGroup> _ognpGroups = new ();

        public Ognp(string name)
        {
            _ognpName = name;
        }

        public List<OgnpGroup> GetOgnpGroups() => _ognpGroups.ToList();

        public void AddGroup(OgnpGroup ognpGroup)
        {
            _ognpGroups.Add(ognpGroup);
        }

        public OgnpGroup FindOgnpGroupByStudent(Student student)
        {
            return _ognpGroups.FirstOrDefault(g => g.GetStudents().Contains(student));
        }
    }
}