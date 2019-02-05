using System.Threading.Tasks;
using Birthdays.Models;

namespace Birthdays.Services {
    public interface IBirthdayService {
        Task<Person[]> GetBirthdays();
        Task SaveBirthday(Birthday birthday);
        Task DeleteBirthday(Person person);
    }
}
