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
        const string BirthdayAPI = "api/birthday";
        const string DefaultLocation = "bodø";

        public async Task<Person[]> GetBirthdays() {
            using (var httpClient = HttpClientFactory.CreateClient()) {
                const int MaxResults = 10;
                var json = await httpClient.GetStringAsync($"{BirthdayAPI}/{DefaultLocation}/{MaxResults}");
                var birthdays = JsonConvert.DeserializeObject<Birthdays>(json);
                var persons = birthdays.TodaysBirthdays.Concat(birthdays.NextBirthdays).ToArray();
                return persons;
            }
        }

        public async Task SaveBirthday(Birthday birthday) {
            using (var httpClient = HttpClientFactory.CreateClient()) {
                var personWithLocation = new BirthdayWithLocation(birthday);
                var json = JsonConvert.SerializeObject(personWithLocation);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await httpClient.PostAsync(BirthdayAPI, content);
                result.EnsureSuccessStatusCode();
            }
        }

        public async Task DeleteBirthday(Person person) {
            using (var httpClient = HttpClientFactory.CreateClient()) {
                await httpClient.DeleteAsync($"{BirthdayAPI}/{person.Id}");
            }
        }

        class Birthdays {
            public Person[] TodaysBirthdays { get; set; }
            public Person[] NextBirthdays { get; set; }
        }

        class BirthdayWithLocation {
            readonly Birthday birthday;

            public BirthdayWithLocation(Birthday birthday) {
                this.birthday = birthday;
            }

            public string Name { get { return birthday.Name; } }
            public string Date { get { return birthday.Birthdate.ToShortDateString(); } }
            public string Location { get { return DefaultLocation; } }
        }

        static class HttpClientFactory {
            public static HttpClient CreateClient()
                => new HttpClient {
                    BaseAddress = new Uri(BaseAddress)
                };
        }
    }
}
