namespace Isu.Models
{
    public class Student
    {
        private readonly int _id;
        private readonly string _name;

        public Student(string name)
        {
            _name = name;
            _id = _name.GetHashCode();
        }

        public int GetId() => _id;
        public string GetName() => _name;
    }
}