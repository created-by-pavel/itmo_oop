using System;

namespace Banks.Models
{
    public delegate void EventDelegate();
    public class Time
    {
        private DateTime _dateTime;
        public Time()
        {
            _dateTime = DateTime.Today;
        }

        public event EventDelegate MyEvent = null;
        public void SkipDay()
        {
            _dateTime = _dateTime.AddDays(1);
            MyEvent.Invoke();
        }

        public void SkipMonth()
        {
            DateTime tmp = _dateTime.AddMonths(1);
            while (_dateTime.Day != tmp.Day || _dateTime.Month != tmp.Month)
            {
                SkipDay();
            }
        }

        public void SkipYear()
        {
            DateTime tmp = _dateTime.AddYears(1);
            while (_dateTime.Day != tmp.Day || _dateTime.Month != tmp.Month || _dateTime.Year != tmp.Year)
            {
                SkipDay();
            }
        }

        public DateTime GetTime() => _dateTime;
    }
}