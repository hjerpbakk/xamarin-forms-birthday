using Birthdays.ViewModels;
using Xamarin.Forms;

namespace Birthdays.Views {
    public partial class BirthdaysPage : ContentPage {
        readonly BirthdaysViewModel birthdaysViewModel;

        public BirthdaysPage() {
            InitializeComponent();
            BindingContext = birthdaysViewModel = new BirthdaysViewModel();
        }

        protected async override void OnAppearing() {
            base.OnAppearing();
            await birthdaysViewModel.FetchBirthdays();
        }
    }
}
