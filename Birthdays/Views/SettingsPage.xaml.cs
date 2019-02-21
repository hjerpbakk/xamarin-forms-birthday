using Birthdays.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Birthdays.Views {
    public partial class SettingsPage : ContentPage {
        public SettingsPage(AdminViewModel adminViewModel) {
            InitializeComponent();
            BindingContext = adminViewModel;
        }
    }
}
