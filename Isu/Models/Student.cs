using System;

namespace Isu.Models
{
    public class Student : IEquatable<Student>
    {
        private readonly int _id;
        private readonly string _name;

        public Student() { }
        public Student(string name)
        {
            _name = name;
            _id = _name.GetHashCode();
        }

        public int GetId() => _id;
        public string GetName() => _name;

        public bool Equals(Student other)
        {
            if (other == null)
                return false;
            return _name == other._name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is not Student studentObj)
                return false;
            else
                return Equals(studentObj);
        }

        public override int GetHashCode() => (_name, _id).GetHashCode();
    }
}
