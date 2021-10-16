using System.Collections.Generic;
using IsuExtra.Models;

namespace IsuExtra.Services
{
    public interface IIsuExtraService
    {
        Faculty AddFaculty(string name, string symbols);
        Group AddGroup(string name);
        Student AddStudent(Group group, string name);
        Lesson AddLessonToTimetable(string name, Time time, Day day, string teacherName, int roomNum, Group group);
        Lesson AddLessonToOgnpGroup(string name, Time time, Day day, string teacherName, int roomNum, OgnpGroup ognpGroup);
        Ognp AddOgnp(string name, Faculty faculty);
        OgnpGroup AddOgnpGroup(string name, Ognp ognp);
        void AddStudentToOgnpGroup(Student student, Ognp ognp, OgnpGroup ognpGroup);
        void RemoveStudentFromOgnp(Student student, Ognp ognp);
        List<OgnpGroup> GetOgnpGroups(Ognp ognp);
        List<Student> GetStudentsInOgnpGroup(OgnpGroup ognpGroup, Ognp ognp);
        List<Student> GetStudentNotInOgnp(Group group);
    }
}