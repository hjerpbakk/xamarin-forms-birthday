using Birthdays.ViewModels;
using Xamarin.Forms;

namespace Birthdays.Views {
    public partial class SettingsPage : ContentPage {
        public SettingsPage() {
            InitializeComponent();
            BindingContext = new AdminViewModel();
        }
    }
}
