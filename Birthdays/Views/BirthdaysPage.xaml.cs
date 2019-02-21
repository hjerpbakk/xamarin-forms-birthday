using Birthdays.Services;
using Birthdays.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Birthdays.Views {
    public partial class BirthdaysPage : ContentPage {
        readonly BirthdaysViewModel birthdaysViewModel;

        public BirthdaysPage(BirthdaysViewModel birthdaysViewModel) {
            InitializeComponent();
            BindingContext = this.birthdaysViewModel = birthdaysViewModel;
        }

        protected async override void OnAppearing() {
            base.OnAppearing();
            await birthdaysViewModel.FetchBirthdays();
        }
    }
}
