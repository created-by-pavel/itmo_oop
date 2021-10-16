using System;
using System.Collections.Generic;
namespace IsuExtra.Models
{
    public class Lesson
    {
        private readonly string _lessonName;
        private readonly string _id;
        private readonly string _teacherName;
        private int _roomNum;
        public Lesson(string name, string teacherName, int roomNum)
        {
            _lessonName = name;
            _teacherName = teacherName;
            _roomNum = roomNum;
            _id = Guid.NewGuid().ToString();
        }

        public bool Equals(Lesson other)
        {
            if (other == null)
                return false;
            return _lessonName == other._lessonName && _teacherName == other._teacherName;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is not Lesson lessonObj)
                return false;
            return Equals(lessonObj);
        }

        public override int GetHashCode() => _id.GetHashCode() ^ _lessonName.GetHashCode();
    }
}