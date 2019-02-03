using System;

namespace Birthdays.Models {
    public class Person {
        public Person(string name, DateTime date) {
            Name = name;
            Birthday = date.ToShortDateString();
            Age = GetAge(date);
        }

        public string Name { get; }
        public string Birthday { get; }
        public uint Age { get; }

        static uint GetAge(DateTime birthDate) {
            var now = DateTime.Now;
            int age = now.Year - birthDate.Year;
            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day)) {
                age--;
            }

            return (uint)age;
        }
    }
}
