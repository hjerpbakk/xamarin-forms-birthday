using System;
using System.Threading.Tasks;
using Birthdays.Models;
using Birthdays.Services;
using Birthdays.ViewModels;
using Tests.Helpers;
using Xunit;

namespace Tests {
    public class BirthdaysViewModelTests {
        [Fact]
        public async Task FetchBirthdays() {
            var birthdayViewModel = new BirthdaysViewModel(new BirthdayServiceFake());
            using (var propertyChangedTracker = new PropertyChangeTracker(birthdayViewModel)) {
                await birthdayViewModel.FetchBirthdays();

                Assert.NotNull(birthdayViewModel.ClosestBirthDay);
                Assert.True(birthdayViewModel.FutureBirthdays.Count > 0);
                propertyChangedTracker.VerifyNumberOfNotifications(2);
                propertyChangedTracker.VerifyNotificationOfName("ClosestBirthDay");
                propertyChangedTracker.VerifyNotificationOfName("FutureBirthdays");
            }
        }

        class BirthdayServiceFake : IBirthdayService {
            public Task DeleteBirthday(Person person) {
                throw new System.NotImplementedException();
            }

            public Task<Person[]> GetBirthdays() {
                return Task.FromResult(new[] {
                    new Person("Runar", new DateTime(1983, 9, 8), 1),
                    new Person("Anders", new DateTime(1960, 12, 24), 2)
                });
            }

            public Task SaveBirthday(Birthday birthday) {
                throw new System.NotImplementedException();
            }
        }
    }
}
