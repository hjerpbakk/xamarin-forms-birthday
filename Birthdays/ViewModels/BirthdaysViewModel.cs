using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Birthdays.Models;
using Birthdays.Services;

namespace Birthdays.ViewModels {
    public class BirthdaysViewModel {
        readonly BirthdayService birthdayService;

        public BirthdaysViewModel() {
            birthdayService = new BirthdayService();
        }

        public Person ClosestBirthDay { get; private set; }
        public ObservableCollection<Person> FutureBirthdays { get; private set; }


        public async Task FetchBirthdays() {
            try {
                var birthdays = await birthdayService.GetBirthdays();
                ClosestBirthDay = birthdays[0];
                FutureBirthdays = new ObservableCollection<Person>(birthdays.Skip(1));
            } catch (Exception e) {
                // TODO: Do some error handling
            }
        }
    }
}
