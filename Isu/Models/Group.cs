using System.Collections.Generic;
using System.Linq;
using Isu.Tools;
namespace Isu.Models
{
    public class Group
        {
            private const short StudentsCountMax = 6;
            private readonly List<Student> _students;
            private readonly string _groupName;
            private readonly CourseNumber _courseNum;
            private short _studentsCount;
            public Group(string groupName)
            {
                int num = groupName[2] - '0';
                _courseNum = (CourseNumber)num;
                _students = new List<Student>();
                _groupName = groupName;
                _studentsCount = 0;
            }

            public string GetGroupName() => _groupName;

            public List<Student> GetStudents() => _students;

            public void AddStud(Student student)
            {
                if (_studentsCount + 1 > StudentsCountMax)
                {
                    throw new IsuException("count of students > max");
                }

                if (_students.Contains(student))
                {
                    throw new IsuException("this student is already in this group");
                }

                _students.Add(student);
                _studentsCount++;
            }

            public Student GetById(int id)
            {
                return _students.First(s => s.GetId() == id);
            }

            public Student GetByName(string name)
            {
                return _students.First(s => s.GetName() == name);
            }

            public List<Student> GetByGroupName() => _students;

            public bool ContainsStudent(Student student) => _students.Contains(student);

            public int GetCourseNumFromGroup() => (int)_courseNum;
            public void RemoveStudent(Student student)
            {
                _students.Remove(student);
            }
        }
}