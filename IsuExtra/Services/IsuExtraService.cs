using System;
using System.Collections.Generic;
using System.Linq;
using IsuExtra.Models;
using IsuExtra.Tools;

namespace IsuExtra.Services
{
    public class IsuExtraService : IIsuExtraService
    {
        private readonly List<Faculty> _faculties = new ();
        public Faculty AddFaculty(string name, string symbols)
        {
            var faculty = new Faculty(name, symbols);
            _faculties.Add(faculty);
            return faculty;
        }

        public Group AddGroup(string name)
        {
            var group = new Group(name);
            string str = name[..^3];
            Faculty faculty = _faculties.FirstOrDefault(f => f.GetSymbols() == str);
            if (faculty == null) throw new IsuExtraException("faculty doesnt exist");
            faculty.AddGroup(group);
            return group;
        }

        public Student AddStudent(Group group, string name)
        {
            if (_faculties.Any(f => f.GetCourse(group.GetCourseNumber).GetGroups().Contains(group)))
            {
                var student = new Student(name);
                group.AddStudent(student);
                return student;
            }

            throw new IsuExtraException("group doesnt exist");
        }

        public Lesson AddLessonToTimetable(string name, Time time, Day day, string teacherName, int roomNum, Group group)
        {
            if (_faculties.Any(f => f.GetCourse(group.GetCourseNumber).GetGroups().Contains(group)))
            {
                var lesson = new Lesson(name, teacherName, roomNum);
                group.AddLesson(lesson, (int)time, (int)day);
                return lesson;
            }

            throw new IsuExtraException("group doesnt exist");
        }

        public Lesson AddLessonToOgnpGroup(string name, Time time, Day day, string teacherName, int roomNum, OgnpGroup ognpGroup)
        {
            if (_faculties.Any(f => f.GetOgnp().GetOgnpGroups().Contains(ognpGroup)))
            {
                var lesson = new Lesson(name, teacherName, roomNum);
                ognpGroup.AddLesson(lesson, (int)time, (int)day);
                return lesson;
            }

            throw new IsuExtraException("OgnpGroup doesnt exist");
        }

        public Ognp AddOgnp(string name, Faculty faculty)
        {
            if (!_faculties.Contains(faculty)) throw new IsuExtraException("faculty doesnt exist");
            var ognp = new Ognp(name);
            faculty.AddOgnp(ognp);
            return ognp;
        }

        public OgnpGroup AddOgnpGroup(string name, Ognp ognp)
        {
            if (_faculties.Any(f => f.GetOgnp() == ognp))
            {
                var ognpGroup = new OgnpGroup(name);
                ognp.AddGroup(ognpGroup);
                return ognpGroup;
            }

            throw new IsuExtraException("ognp doesnt exist");
        }

        public void AddStudentToOgnpGroup(Student student, Ognp ognp, OgnpGroup ognpGroup)
        {
            if (!ognp.GetOgnpGroups().Contains(ognpGroup)) throw new IsuExtraException("this ognp and ognpgroup're not connetcted");
            Group group = FindStudentGroup(student);
            if (group == null) throw new IsuExtraException("student doesnt exist");
            if (GetOgnpCountByStudent(student) >= 2) throw new IsuExtraException("ognpCount >= 2");
            if (CheckExistanceInOgnp(student, ognp)) throw new IsuExtraException("student is already exist");
            Faculty faculty = FindStudentFaculty(student);
            if (faculty.GetOgnp() == ognp) throw new IsuExtraException("cant do it because of faculty");
            List<(int, int)> ognpGroupDateTime = new ();
            List<(int, int)> groupDateTime = new ();
            foreach (var dateTimeLessonKVP in ognpGroup.GetTimetable())
            {
                int date = dateTimeLessonKVP.Key;
                ognpGroupDateTime.AddRange(dateTimeLessonKVP.Value.Select(timeLesson => (date, timeLesson.Item1)));
            } // Item1 is a day

            foreach (var dateTimeLessonKVP in group.GetTimetable())
            {
                int date = dateTimeLessonKVP.Key;
                groupDateTime.AddRange(dateTimeLessonKVP.Value.Select(timeLesson => (date, timeLesson.Item1)));
            } // Item1 is a day

            var result = ognpGroupDateTime.Intersect(groupDateTime);
            if (result.Any()) throw new IsuExtraException("intersection");
            ognpGroup.AddStudent(student);
        }

        public void RemoveStudentFromOgnp(Student student, Ognp ognp)
        {
            if (_faculties.Any(f => f.GetOgnp() == ognp))
            {
                ognp.FindOgnpGroupByStudent(student)?.RemoveStudent(student);
                return;
            }

            throw new IsuExtraException("ognp doesnt exist");
        }

        public List<OgnpGroup> GetOgnpGroups(Ognp ognp)
        {
            if (_faculties.Any(f => f.GetOgnp() == ognp))
            {
                return ognp.GetOgnpGroups();
            }

            throw new IsuExtraException("ognp doesnt exist");
        }

        public List<Student> GetStudentsInOgnpGroup(OgnpGroup ognpGroup, Ognp ognp)
        {
            if (_faculties.Any(f => f.GetOgnp() == ognp) && ognp.GetOgnpGroups().Contains(ognpGroup))
            {
                return ognpGroup.GetStudents();
            }

            throw new IsuExtraException("OgnpGroup doesnt exist");
        }

        public List<Student> GetStudentNotInOgnp(Group group)
        {
            if (_faculties.Any(f => f.GetCourse(group.GetCourseNumber).GetGroups().Contains(group)))
            {
                return group.GetStudents().Where(s => GetOgnpCountByStudent(s) < 2).ToList();
            }

            throw new IsuExtraException("group doesnt exist");
        }

        private Group FindStudentGroup(Student student)
        {
            const int coursesCount = 4;
            foreach (Faculty f in _faculties)
            {
                for (int i = 0; i < coursesCount; i++)
                {
                    Group group = f.GetCourse(i).GetGroups().FirstOrDefault(g => g.GetStudents().Contains(student));
                    if (group != null) return group;
                }
            }

            return null;
        }

        private Faculty FindStudentFaculty(Student student)
        {
            const int coursesCount = 4;
            foreach (Faculty f in _faculties)
            {
                for (int i = 0; i < coursesCount; i++)
                {
                    List<Group> groups = f.GetCourse(i).GetGroups();
                    if (groups.Select(g => g.GetStudents()).Any(students => students.Contains(student)))
                    {
                        return f;
                    }
                }
            }

            return null;
        }

        private bool CheckExistanceInOgnp(Student student, Ognp ognp)
        {
            return ognp.GetOgnpGroups().Any(g => g.GetStudents().Contains(student));
        }

        private int GetOgnpCountByStudent(Student student)
        {
            return _faculties.SelectMany(f => f.GetOgnp().GetOgnpGroups()).Count(g => g.GetStudents().Contains(student));
        }
    }
}
