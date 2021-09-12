using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
                if (name.Length > 6 || name[0] != 'M' || name[1] != '3' || (int.TryParse(sub, out n) == false))
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
                if (_groups.Contains(group))
                {
                    var student = new Student(name);
                    group.AddStud(student);
                    return student;
                }

                throw new IsuException("this group doesnt exist");
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
                foreach (Group group in _groups)
                {
                    return group.GetByName(name);
                }

                throw new IsuException("name of student not found");
            }

            public List<Student> FindStudents(string groupName)
            {
                foreach (Group group in _groups)
                {
                    if (group.GetGroupName() == groupName)
                    {
                        return group.GetByGroupName();
                    }
                }

                throw new IsuException("group not found");
            }

            public List<Student> FindStudents(CourseNumber courseNumber)
            {
                var unitedStudents = new List<Student>();
                foreach (Group group in _groups)
                {
                    if (group.GetCourseNumFromGroup() == (int)courseNumber)
                    {
                        unitedStudents.AddRange(group.GetStudents());
                    }
                }

                if (unitedStudents.Count == 0)
                {
                    throw new IsuException("this course doesnt exist");
                }

                return unitedStudents;
            }

            public Group FindGroup(string groupName)
            {
                foreach (Group group in _groups)
                {
                    if (group.GetGroupName() == groupName)
                    {
                        return group;
                    }
                }

                throw new IsuException("group not found");
            }

            public List<Group> FindGroups(CourseNumber courseNumber)
            {
                var unitedGroups = new List<Group>();
                foreach (Group group in _groups)
                {
                    if (group.GetCourseNumFromGroup() == (int)courseNumber)
                    {
                        unitedGroups.Add(group);
                    }
                }

                if (unitedGroups.Count == 0)
                {
                    throw new IsuException("this course doesnt exist");
                }

                return unitedGroups;
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
