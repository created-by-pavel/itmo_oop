using System;

namespace IsuExtra.Models
{
    public class Student : IEquatable<Student>
    {
        private readonly string _id;
        private readonly string _name;
        public Student(string name)
        {
            _name = name;
            _id = Guid.NewGuid().ToString();
        }

        public string GetId()
        {
            string copy = _id;
            return copy;
        }

        public string GetName()
        {
            string copy = _name;
            return copy;
        }

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
            return Equals(studentObj);
        }

        public override int GetHashCode() => _name.GetHashCode() ^ _id.GetHashCode();
    }
}
