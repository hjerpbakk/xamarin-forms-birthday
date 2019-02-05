using System;

namespace Birthdays.Models {
    public class Birthday {
        public Birthday(string name, DateTime birthdate) {
            Name = name;
            Birthdate = birthdate;
        }

        public string Name { get; }
        public DateTime Birthdate { get; }
    }
}
