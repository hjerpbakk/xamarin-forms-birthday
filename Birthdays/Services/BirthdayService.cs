using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Birthdays.Models;
using Newtonsoft.Json;

namespace Birthdays.Services {
    public class BirthdayService {
        const string BaseAddress = "http://178.62.18.75:8081";

        public async Task<Person[]> GetBirthdays() {
            using (var httpClient = new HttpClient {
                BaseAddress = new Uri(BaseAddress)
            }) {
                var json = await httpClient.GetStringAsync("api/birthday/trondheim/10");
                var birthdays = JsonConvert.DeserializeObject<Birthdays>(json);
                var persons = birthdays.TodaysBirthdays.Concat(birthdays.NextBirthdays).ToArray();
                return persons;
            }
        }

        public async Task SaveBirthday(Person person) {
            using (var httpClient = new HttpClient {
                BaseAddress = new Uri(BaseAddress)
            }) {
                var personWithLocation = new PersonWithLocation(person);
                var json = JsonConvert.SerializeObject(personWithLocation);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await httpClient.PostAsync("api/birthday", content);
                result.EnsureSuccessStatusCode();
            }
        }

        class Birthdays {
            public Person[] TodaysBirthdays { get; set; }
            public Person[] NextBirthdays { get; set; }
        }

        class PersonWithLocation {
            public PersonWithLocation(Person person) {
                Name = person.Name;
                Date = DateTime.Parse(person.Birthday);
            }

            public string Name { get; }
            public DateTime Date { get; }
            public string Location { get; } = "Trondheim";
        }
    }
}
