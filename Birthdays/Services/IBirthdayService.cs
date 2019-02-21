using System.Threading.Tasks;
using Birthdays.Models;
using Xamarin.Forms.Internals;

namespace Birthdays.Services {
    [Preserve(AllMembers = true)]
    public interface IBirthdayService {
        Task<Person[]> GetBirthdays();
        Task SaveBirthday(Birthday birthday);
        Task DeleteBirthday(Person person);
    }
}
