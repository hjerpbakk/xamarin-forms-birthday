using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Birthdays.Models;
using Newtonsoft.Json;

namespace Birthdays.Services {
    public class BirthdayService {
        public async Task<Person[]> GetBirthdays() {
            using (var httpClient = new HttpClient {
                BaseAddress = new Uri("http://vt-ekvtool1:8081")
            }) {
                var json = await httpClient.GetStringAsync("api/birthday/trondheim");
                var birthdays = JsonConvert.DeserializeObject<Birthdays>(json);
                var persons = birthdays.TodaysBirthdays.Concat(birthdays.NextBirthdays).ToArray();
                return persons;
            }
        }

        class Birthdays {
            public Person[] TodaysBirthdays { get; set; }
            public Person[] NextBirthdays { get; set; }
        }
    }
}
