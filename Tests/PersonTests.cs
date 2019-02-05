using System;
using Birthdays.Helpers;
using Birthdays.Models;
using Xunit;

namespace Tests {
    public class PersonTests {
        [Fact]
        public void VerifyPerson() {
            var currentDate = new DateTime(2019, 2, 6);
            using (var context1 = new DateTimeProviderContext(currentDate)) {
                var birthdate = new DateTime(1983, 9, 8);

                var runar = new Person("Runar", birthdate, 0);

                Assert.Equal("Runar", runar.Name);
                Assert.Equal(birthdate, DateTime.Parse(runar.Birthday));
                Assert.Equal((uint)35, runar.Age);
                Assert.Equal(0, runar.Id);
            }
        }
    }
}
