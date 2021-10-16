using System;
using System.Collections.Generic;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class Faculty
    {
        private readonly string _name;
        private readonly string _symbols;
        private readonly List<Course> _courses;
        private int _ognpCount = 0;
        private Ognp _ognp;

        public Faculty(string name, string symbols)
        {
            _name = name;
            _symbols = symbols;
            Course course1 = new (), course2 = new (), course3 = new (), course4 = new ();
            _courses = new List<Course>(4) { course1, course2, course3, course4 };
        }

        public void AddGroup(Group group)
        {
            string sub = group.GetName()[1..];
            if (group.GetName().Length != 5 || !int.TryParse(sub, out int n)) throw new IsuExtraException("invalid groupName");
            _courses[Convert.ToInt32(group.GetName()[2].ToString())].AddGroup(group);
        }

        public Course GetCourse(int num)
        {
            Course copy = _courses[num];
            return copy;
        }

        public string GetSymbols()
        {
            string copy = _symbols;
            return copy;
        }

        public Ognp GetOgnp()
        {
            Ognp copy = _ognp;
            return copy;
        }

        public void AddOgnp(Ognp ognp)
        {
            if (_ognpCount >= 1) throw new IsuExtraException("too much ognp, body");
            _ognp = ognp;
            _ognpCount++;
        }

        public bool Equals(Faculty other)
        {
            if (other == null)
                return false;
            return _name == other._name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is not Faculty facultyObj)
                return false;
            return Equals(facultyObj);
        }

        public override int GetHashCode() => _name.GetHashCode() ^ _ognp.GetHashCode();
    }
}