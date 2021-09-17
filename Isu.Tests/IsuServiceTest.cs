using Isu.Models;
using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;
        
        [SetUp]
        public void Setup()
        {
            //TODO: implement
            _isuService = new IsuService();
        }
        
        [Test]
        public void AddStudentToGroup_GroupContainsStudent_ThrowException()
        {
            try
            {
                Group group = _isuService.AddGroup("M3207");
                _isuService.AddStudent(group, "Pavel Zavalnyuk");
                _isuService.AddStudent(group, "Pavel Zavalnyuk");
                Assert.Fail();
            }
            catch (IsuException) { }
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group = _isuService.AddGroup("M3207");
                Student st1 = _isuService.AddStudent(group, "Pavel Zavalnyuk");
                Student st2 = _isuService.AddStudent(group, "Nikolay Kondratiev"); 
                Student st3 = _isuService.AddStudent(group, "Alexandr Friz");
                Student st4 = _isuService.AddStudent(group, "Ira Magaryn");
                Student st5 = _isuService.AddStudent(group, "Alexandr Toxic");
                Student st6 = _isuService.AddStudent(group, "Dmitrii Linux");
                Student st7 = _isuService.AddStudent(group, "Semen Doroshenko");
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group = _isuService.AddGroup("M3207777");
            });
        }

        [Test]
        public void TransferStudentToGroupWhichDoesntExist_ThrowException()
        {
            Assert.Catch<IsuException>(() => 
            {
                var newGroup = new Group("M3211");
                Group group = _isuService.AddGroup("M3209");
                Student student = _isuService.AddStudent(group, "Pavel Zavalnyuk");
                
                _isuService.ChangeStudentGroup(student, newGroup);
            });
        }
    }
}