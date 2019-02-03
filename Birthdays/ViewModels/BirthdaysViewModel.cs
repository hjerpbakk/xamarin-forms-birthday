using System;
using Birthdays.Models;

namespace Birthdays.ViewModels {
    public class BirthdaysViewModel {
        public BirthdaysViewModel() {
            ClosestBirthDay = new Person("Birthday Boi", new DateTime(1984, 2, 6));
        }

        public Person ClosestBirthDay { get; }
    }
}
