using System;
using System.Linq;
using System.Threading.Tasks;
using Birthdays.Models;
using Birthdays.Services;
using Xunit;

namespace Tests {
    public class BirthdayServiceTests {
        [Fact]
        public async Task VerifyBirthdayService() {
            var uniqueLocation = Guid.NewGuid().ToString("N");
            var birthdayService = new BirthdayService(uniqueLocation);

            var birthdate = DateTime.Today + TimeSpan.FromDays(1);
            var uniqueName = Guid.NewGuid().ToString("N");
            var birthday = new Birthday(uniqueName, birthdate);

            await birthdayService.SaveBirthday(birthday);

            var persons = await birthdayService.GetBirthdays();

            Assert.True(persons.Any());
            Assert.Equal(birthday.Name, persons[0].Name);

            await birthdayService.DeleteBirthday(persons[0]);

            persons = await birthdayService.GetBirthdays();

            Assert.False(persons.Any());
        }
    }
}
