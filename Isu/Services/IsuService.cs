using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using Isu.Models;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
        {
            private readonly List<Group> _groups = new ();

            public Group AddGroup(string name)
            {
                int n;
                string sub = name.Substring(2);
                if (name.Length != 5 || !name.StartsWith("M3") || !int.TryParse(sub, out n))
                {
                    throw new IsuException("bad group name");
                }

                if (_groups.Any(g => g.GetGroupName() == name))
                {
                    throw new IsuException("this group is already exist");
                }

                var newGroup = new Group(name);
                _groups.Add(newGroup);
                return newGroup;
            }

            public Student AddStudent(Group group, string name)
            {
                if (!_groups.Contains(group)) throw new IsuException("this group doesnt exist");
                var student = new Student(name);
                group.AddStud(student);
                return student;
            }

            public Student GetStudent(int id)
            {
                foreach (Group group in _groups)
                {
                    return group.GetById(id);
                }

                throw new IsuException("id not found");
            }

            public Student FindStudent(string name)
            {
                var st = new Student();
                foreach (Group group in _groups)
                {
                    st = group.GetByName(name);
                }

                return st;
            }

            public List<Student> FindStudents(string groupName)
            {
                Group group = _groups.FirstOrDefault(g => g.GetGroupName() == groupName);
                return group?.GetStudents();
            }

            public List<Student> FindStudents(CourseNumber courseNumber)
            {
                var unitedStudents = new List<Student>();
                foreach (Group group in _groups.Where(g => g.GetCourseNumFromGroup() == (int)courseNumber))
                {
                    unitedStudents.AddRange(group.GetStudents());
                }

                return unitedStudents;
            }

            public Group FindGroup(string groupName)
            {
                return _groups.FirstOrDefault(g => g.GetGroupName() == groupName);
            }

            public List<Group> FindGroups(CourseNumber courseNumber)
            {
                return _groups.Where(g => g.GetCourseNumFromGroup() == (int)courseNumber).ToList();
            }

            public void ChangeStudentGroup(Student student, Group newGroup)
            {
                foreach (Group group in _groups)
                {
                    if (group.ContainsStudent(student))
                    {
                        group.RemoveStudent(student);
                        if (_groups.Contains(newGroup))
                        {
                            group.AddStud(student);
                        }
                        else
                        {
                            throw new IsuException("this group doesnt exist");
                        }
                    }
                    else
                    {
                        throw new IsuException("every group doesnt contain this student");
                    }
                }
            }
        }
}
