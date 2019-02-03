using System;
using System.Collections.ObjectModel;
using Birthdays.Models;

namespace Birthdays.ViewModels {
    public class BirthdaysViewModel {
        public BirthdaysViewModel() {
            ClosestBirthDay = new Person("Birthday Boi", new DateTime(1984, 2, 6));
            FutureBirthdays = new ObservableCollection<Person> {
                new Person("Paul", new DateTime(1983, 3, 7)),
                new Person("Leto", new DateTime(1982, 4, 8)),
                new Person("Vladimir", new DateTime(1981, 5, 9)),
                new Person("Jessica", new DateTime(1980, 6, 10)),
                new Person("Duncan", new DateTime(1979, 7, 11))
            };
        }

        public Person ClosestBirthDay { get; }
        public ObservableCollection<Person> FutureBirthdays { get; }
    }
}
