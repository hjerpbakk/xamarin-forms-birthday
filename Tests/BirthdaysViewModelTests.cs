using System.Threading.Tasks;
using Birthdays.ViewModels;
using Tests.Helpers;
using Xunit;

namespace Tests {
    public class BirthdaysViewModelTests {
        [Fact]
        public async Task FetchBirthdays() {
            var birthdayViewModel = new BirthdaysViewModel();
            using (var propertyChangedTracker = new PropertyChangeTracker(birthdayViewModel)) {
                await birthdayViewModel.FetchBirthdays();

                Assert.NotNull(birthdayViewModel.ClosestBirthDay);
                Assert.True(birthdayViewModel.FutureBirthdays.Count > 0);
                propertyChangedTracker.VerifyNumberOfNotifications(2);
                propertyChangedTracker.VerifyNotificationOfName("ClosestBirthDay");
                propertyChangedTracker.VerifyNotificationOfName("FutureBirthdays");
            }
        }
    }
}
