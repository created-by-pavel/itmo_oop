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
            _courses = new List<Course>(4);
            Course course1 = new Course();
            Course course2 = new Course();
            Course course3 = new Course();
            Course course4 = new Course();
            _courses.Add(course1);
            _courses.Add(course2);
            _courses.Add(course3);
            _courses.Add(course4);
        }

        public void AddGroup(Group group)
        {
            _courses[Convert.ToInt32(group.GetName()[2].ToString())].AddGroup(group);
        }

        public Course GetCourse(int num) => _courses[num];

        public string GetSymbols() => _symbols;
        public Ognp GetOgnp() => _ognp;
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