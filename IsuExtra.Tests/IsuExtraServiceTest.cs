using System.Collections.Generic;
using NUnit.Framework;
using IsuExtra.Models;
using IsuExtra.Services;
using IsuExtra.Tools;
namespace IsuExtra.Tests
{
    public class Tests
    {
        private IIsuExtraService _isuExtraService;
        private Faculty _ic, _ct, _sft, _inter;
        private Group _m3207;
        private Student _pavel, _sasha;
        private Ognp _cyber, _web, _linux, _mobileDev;
        private Lesson _math, _os;
        private OgnpGroup _k1, _k2;
        private OgnpGroup _s1;
        private OgnpGroup _w1;
        private OgnpGroup _l1;

        [SetUp]
        public void Setup()
        {
            _isuExtraService = new IsuExtraService();
            _ic = _isuExtraService.AddFaculty("ic", "M3");
            _ct = _isuExtraService.AddFaculty("ct", "N2");
            _sft = _isuExtraService.AddFaculty("sft", "Y1");
            _inter = _isuExtraService.AddFaculty("inter", "T3");
            _m3207 = _isuExtraService.AddGroup("M3207");
            _pavel = _isuExtraService.AddStudent(_m3207, "pavel");
            _sasha = _isuExtraService.AddStudent(_m3207, "sasha");
            _cyber = _isuExtraService.AddOgnp("cyber", _ct);
            _web = _isuExtraService.AddOgnp("web", _inter);
            _linux = _isuExtraService.AddOgnp("linux", _ic);
            _mobileDev = _isuExtraService.AddOgnp("mobileDev", _sft);
            _math = _isuExtraService.AddLessonToTimetable("math", Time.EightTwenty, Day.Monday, "isaeva", 312, _m3207);
            _os = _isuExtraService.AddLessonToTimetable("os", Time.EightTwenty, Day.Tuesday, "KotDimos", 311, _m3207);
            _k1 = _isuExtraService.AddOgnpGroup("k1", _cyber);
            _k2 = _isuExtraService.AddOgnpGroup("k2", _cyber);
            _s1 = _isuExtraService.AddOgnpGroup("s1", _mobileDev);
            _w1 = _isuExtraService.AddOgnpGroup("w1", _web);
            _l1 = _isuExtraService.AddOgnpGroup("l1", _linux);
        }
        
        [Test]
        public void AddLessonToOgnpGroup()
        {
            Lesson kali = _isuExtraService.AddLessonToOgnpGroup("kali", Time.Ten, Day.Monday, "KotDimos", 100, _k1);
            var copyKaliLesson = new Lesson("kali", "KotDimos", 100);
            var expectedTimetable = new Dictionary<int, List<(int, Lesson)>>() {
                { (int)Day.Monday, new List<(int, Lesson)>() {
                    {((int)Time.Ten, copyKaliLesson)}
                }}
            };
            CollectionAssert.AreEqual(expectedTimetable,_k1.GetTimetable());
        }
        
        [Test]
        public void AddStudentToOgnpGroup()
        {
            _isuExtraService.AddStudentToOgnpGroup(_pavel, _cyber, _k1);
            CollectionAssert.Contains(_k1.GetStudents(), _pavel);
        }

        [Test]
        public void AddStudentInSameOgnp_ThrowException()
        {
            Assert.Catch<IsuExtraException>(() =>
            {
                _isuExtraService.AddStudentToOgnpGroup(_pavel, _cyber, _k1);
                _isuExtraService.AddStudentToOgnpGroup(_pavel, _cyber,_k2);
            });
        }
        
        [Test]
        public void AddStudentToOgnp_MoreThanTwice_ThrowException()
        {
            Assert.Catch<IsuExtraException>(() =>
            {
                _isuExtraService.AddStudentToOgnpGroup(_pavel, _cyber, _k1);
                _isuExtraService.AddStudentToOgnpGroup(_pavel, _mobileDev, _s1);
                _isuExtraService.AddStudentToOgnpGroup(_pavel,_web, _w1);
            });
        }

        [Test]
        public void AddStudentToOgnp_InHisFaculty_ThrowException()
        {
            Assert.Catch<IsuExtraException>(() =>
            {
                _isuExtraService.AddStudentToOgnpGroup(_pavel, _linux, _l1);
            });
        }
        
        [Test]
        public void AddStudentToOgnp_TimetableIntersection_ThrowException()
        {
            Assert.Catch<IsuExtraException>(() =>
            {
               _isuExtraService.AddLessonToOgnpGroup("kali", Time.EightTwenty, Day.Monday, "KotDimos", 100, _k1);
               _isuExtraService.AddStudentToOgnpGroup(_pavel, _cyber, _k1);
            });
        }

        [Test]
        public void GetStudentNotInOgnp()
        {
            List<Student> expectedStudents = new() {_pavel, _sasha};
            List<Student> notInOgnpStudents = _isuExtraService.GetStudentNotInOgnp(_m3207);
            CollectionAssert.AreEqual(expectedStudents, notInOgnpStudents);
        }
        
        [Test]
        public void GetOgnpGroups()
        {
            List<OgnpGroup> ognpGroups = new() {_k1, _k2};
            CollectionAssert.AreEqual(ognpGroups, _cyber.GetOgnpGroups());
        }
        
        [Test]
        public void GetStudentsInOgnpGroup()
        {
            _isuExtraService.AddStudentToOgnpGroup(_pavel, _cyber, _k1);
            List<Student> students = new() {_pavel};
            CollectionAssert.AreEqual(students, _isuExtraService.GetStudentsInOgnpGroup(_k1, _cyber));
        }
        
        [Test]
        public void RemoveStudentFromOgnp()
        {
            _isuExtraService.AddStudentToOgnpGroup(_pavel, _cyber, _k1);
            _isuExtraService.RemoveStudentFromOgnp(_pavel, _cyber);
            CollectionAssert.DoesNotContain(_k1.GetStudents(), _pavel);
        }
    }
}