using Birthdays.ViewModels;
using Xamarin.Forms;

namespace Birthdays.Views {
    public partial class BirthdaysPage : ContentPage {
        public BirthdaysPage() {
            InitializeComponent();
            BindingContext = new BirthdaysViewModel();
        }
    }
}
