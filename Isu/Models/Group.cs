using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Tools;
namespace Isu.Models
{
    public class Group : IEquatable<Group>
        {
            private const short StudentsCountMax = 6;
            private readonly List<Student> _students;
            private readonly string _groupName;
            private readonly CourseNumber _courseNum;
            public Group(string groupName)
            {
                int num = int.Parse(Convert.ToString(groupName[2]));
                _courseNum = (CourseNumber)num;
                _students = new List<Student>();
                _groupName = groupName;
            }

            private short StudentsCount => (short)_students.Count;
            public string GetGroupName() => _groupName;

            public List<Student> GetStudents()
            {
                List<Student> copyStudents = _students;
                return copyStudents;
            }

            public void AddStud(Student student)
            {
                if (StudentsCount >= StudentsCountMax)
                {
                    throw new IsuException("count of students > max");
                }

                if (_students.Contains(student))
                {
                    throw new IsuException("this student is already in this group");
                }

                _students.Add(student);
            }

            public Student GetById(int id)
            {
                return _students.First(s => s.GetId() == id);
            }

            public Student GetByName(string name)
            {
                return _students.First(s => s.GetName() == name);
            }

            public bool ContainsStudent(Student student) => _students.Contains(student);

            public int GetCourseNumFromGroup() => (int)_courseNum;
            public void RemoveStudent(Student student)
            {
                _students.Remove(student);
            }

            public bool Equals(Group other)
            {
                if (other == null) return false;
                return _groupName == other._groupName;
            }

            public override bool Equals(object obj)
            {
                if (obj is not Group groupObj) return false;
                return Equals(groupObj);
            }

            public override int GetHashCode() => _groupName.GetHashCode() ^ _courseNum.GetHashCode();
        }
}