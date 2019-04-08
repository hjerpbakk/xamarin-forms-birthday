using System;
using System.Collections.Generic;
using Birthdays.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Birthdays.Views {
    public partial class MainPage : TabbedPage {
        public MainPage(BirthdaysViewModel birthdaysViewModel, AdminViewModel adminViewModel) {
            InitializeComponent();
            Children.Add(new NavigationPage(new BirthdaysPage(birthdaysViewModel)) { Title = "Birthdays", Icon = "birthday" });
            Children.Add(new NavigationPage(new SettingsPage(adminViewModel)) { Title = "Settings", Icon="settings" });
        }
    }
}
