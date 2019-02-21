using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Birthdays.Views;
using LightInject;
using Birthdays.ViewModels;
using Birthdays.Services;
using Birthdays.Models;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Birthdays {
    public partial class App : Application {
        readonly ServiceContainer serviceContainer;

        public App() {
            InitializeComponent();

            serviceContainer = new ServiceContainer();

            serviceContainer.RegisterInstance(new Location("bodø"));
            serviceContainer.RegisterSingleton<IBirthdayService, BirthdayService>();

            serviceContainer.RegisterSingleton<BirthdaysViewModel>();
            serviceContainer.RegisterSingleton<AdminViewModel>();

            MainPage = new MainPage(serviceContainer.GetInstance<BirthdaysViewModel>(), serviceContainer.GetInstance<AdminViewModel>());
        }

        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}
