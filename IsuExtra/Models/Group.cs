using System;
using System.Collections.Generic;
using System.Linq;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class Group : IEquatable<Group>
    {
        private const short StudentsCountMax = 6;
        private readonly string _groupName;
        private readonly List<Student> _students;
        private readonly Dictionary<int, List<(int, Lesson)>> _timetable;
        public Group() { }

        public Group(string groupName)
        {
            _groupName = groupName;
            _students = new List<Student>();
            _timetable = new Dictionary<int, List<(int, Lesson)>>();
        }

        public int GetCourseNumber => Convert.ToInt32(GetName()[2].ToString());

        private short StudentsCount => (short)_students.Count;

        public string GetName() => _groupName;
        public List<Student> GetStudents() => _students.ToList();

        public Dictionary<int, List<(int, Lesson)>> GetTimetable()
        {
            Dictionary<int, List<(int, Lesson)>> copy = _timetable;
            return copy;
        }

        public void AddStudent(Student student)
        {
            if (StudentsCount >= StudentsCountMax)
            {
                throw new IsuExtraException("count of students > max");
            }

            if (_students.Contains(student))
            {
                throw new IsuExtraException("this student is already in this group");
            }

            _students.Add(student);
        }

        public void RemoveStudent(Student student)
        {
            _students.Remove(student);
        }

        public void AddLesson(Lesson lesson, int time, int day)
        {
            if (_timetable.ContainsKey(day) && !_timetable[day].Contains((time, lesson)))
            {
                _timetable[day].Add((time, lesson));
                return;
            }

            if (!_timetable.ContainsKey(day))
            {
                List<(int, Lesson)> list = new () { (time, lesson) };
                _timetable.Add(day, list);
                return;
            }

            throw new IsuExtraException("timetable intersection");
        }

        public bool Equals(Group other)
        {
            if (other == null) return false;
            return string.Equals(_groupName, other._groupName, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            if (obj is not Group groupObj) return false;
            return Equals(groupObj);
        }

        public override int GetHashCode() => _groupName.GetHashCode() ^ _timetable.GetHashCode();
    }
}