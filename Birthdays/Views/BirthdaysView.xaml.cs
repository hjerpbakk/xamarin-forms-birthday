using System;
using Birthdays.Models;
using Birthdays.ViewModels;
using Xamarin.Forms;

namespace Birthdays.Views {
    public partial class BirthdaysView : ContentView {
        public BirthdaysView() {
            InitializeComponent();
        }

        public async void OnDelete(object sender, EventArgs e) {
            var menuItem = (MenuItem)sender;
            var person = (Person)menuItem.CommandParameter;
            var birthdaysViewModel = (BirthdaysViewModel)BindingContext;
            await birthdaysViewModel.RemoveBirthday(person);
        }
    }
}
