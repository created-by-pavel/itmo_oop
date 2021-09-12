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
            _isuService = null;
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            var isu = new IsuService();
            Group group = isu.AddGroup("M3207");
            
            try
            {
                isu.AddStudent(group, "Pavel Zavalnyuk"); 
                isu.AddStudent(group, "Pavel Zavalnyuk");
            }
            
            catch(IsuException ep)
            {
                Assert.Fail(ep.Message);
            }
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                var isu = new IsuService();
                Group group = isu.AddGroup("M3207");
                Student st1 = isu.AddStudent(group, "Pavel Zavalnyuk");
                Student st2 = isu.AddStudent(group, "Nikolay Kondratiev"); 
                Student st3 = isu.AddStudent(group, "Alexandr Friz");
                Student st4 = isu.AddStudent(group, "Ira Magaryn");
                Student st5 = isu.AddStudent(group, "Alexandr Toxic");
                Student st6 = isu.AddStudent(group, "Dmitrii Linux");
                Student st7 = isu.AddStudent(group, "Semen Doroshenko");
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            // assert
            Assert.Catch<IsuException>(() =>
            {
                var isu = new IsuService();
                Group group = isu.AddGroup("M3207777");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Assert.Catch<IsuException>(() => 
            {
                var isu = new IsuService();
                var newGroup = new Group("M3211");
                isu.AddGroup("M3209");
                isu.AddGroup("M3207");
                var student = new Student("Pavel Zavalnyuk");
                isu.ChangeStudentGroup(student, newGroup);
            });
        }
    }
}