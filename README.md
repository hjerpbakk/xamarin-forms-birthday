# xamarin-forms-birthday
Never miss a birthday again! This educational app was created as part of a Xamarin Forms course.

## Create new solution

Use `Blank Forms App` template. Update packages, build and run, it should work.

### Key takeaway

Know the difference between the different projects and understand how this can run on your phone.

## Show mock-up of the app to be created

<img src="mockup.png" height="677" width="376" />

### Key takeaway

Always mock-up your app before starting to code to avoid unecessary coding.

## Navigation and Tab bar

Delete the `MainPage`.

Create a new folder `Views`.

Add a new `MainPage` with XAML, inheriting from `ContenPage`. Change the inheritance to `TabbedPage`. The XAML should look like this:

```XAML
<?xml version="1.0" encoding="UTF-8"?>
<TabbedPage xmlns="http://xamarin.com/schemas/2014/forms" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" xmlns:views="clr-namespace:Birthdays.Views" x:Class="Birthdays.Views.MainPage">
    <TabbedPage.Children>
        <NavigationPage Title="Birthdays">
            <x:Arguments>
                <views:BirthdaysPage />
            </x:Arguments>
        </NavigationPage>
        <NavigationPage Title="Settings">
            <x:Arguments>
                <views:SettingsPage />
            </x:Arguments>
        </NavigationPage>
    </TabbedPage.Children>
</TabbedPage>
```

Three problems now: 

1. MainPage has a new namespace
2. `BirthdaysPage` doesn't exist
3. `SettingsPage`doesn't exist

To fix this: 

1. Add `using Birthdays.Views;` to `App.xaml.cs`.
2. Create a `ContentPage` with XAML called `BirthdaysPage`.
3. Create a `ContentPage` with XAML called `SettingsPage`.

Build the app and run it.

### Key takeaway

Creating a skeleton of the app first makes it easier to add inn details later. XAML is used to describe the user interface in Xamarin Forms.

## Add titles to the new pages

Set the property `Title` of the two new `ContentPages`.

### Key takeaway

Navigation bars inherit the title of their active child. Each tab has its own navigation stack.

## Create a Grid for the Birthdays page

Create a simple grid consisting of two eqaul vertical parts like this:

```XAML
<ContentPage.Content>
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="*" />
            <RowDefinition Height="*" />
        </Grid.RowDefinitions>
        <ContentView BackgroundColor="Red" />
        <ContentView Grid.Row="1" BackgroundColor="Green" />
    </Grid>
</ContentPage.Content>
```

### Key takeaway

Grid is an excellent way to lay out a small number of items on the screen. Verify your design using simple techniques before proceeding with details.

## Create view for closest birthday

Create a `ContentView` with XAML which will contain the closest birthday, `ClosestBirthdayView`:

```XAML
<ContentView.Content>
    <StackLayout>
        <Label HorizontalTextAlignment="Center" FontSize="24" VerticalOptions="Start" Margin="0,30,0,0" Text="Birthday Boi" />
        <Label HorizontalTextAlignment="Center" FontSize="32" VerticalOptions="CenterAndExpand" Text="35 years" />
        <Label HorizontalTextAlignment="Center" FontSize="24" VerticalOptions="End" Margin="0,0,0,30" Text="6.2.1984" />
    </StackLayout>
</ContentView.Content>
```

```XAML
xmlns:views="clr-namespace:Birthdays.Views"
...
<views:ClosestBirthdayView />
```

### Key takeway

Create small components to build up your UI. These must be referenced in XAML using the correct namespace. Know and utilize different layout options.

## Create all birthdays view

Create a `ContentView` with XAML which will show a list of all birthdays, `BirthdaysView`:

```XAML
<ContentView xmlns="http://xamarin.com/schemas/2014/forms" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" x:Class="Birthdays.Views.BirthdaysView">
    <ContentView.Content>
        <ListView>
            <ListView.ItemsSource>
                <x:Array Type="{x:Type x:String}">
                    <x:String>Paul</x:String>
                    <x:String>Leto</x:String>
                    <x:String>Vladimir</x:String>
                    <x:String>Jessica</x:String>
                    <x:String>Duncan</x:String>
                </x:Array>
            </ListView.ItemsSource>
            <ListView.ItemTemplate>
                <DataTemplate>
                    <TextCell Text="{Binding}" Detail="Bursdag" />
                </DataTemplate>
            </ListView.ItemTemplate>
        </ListView>
    </ContentView.Content>
</ContentView>
```

### Key takeaway

A `ListView` is useful to show lists. `DataTemplates` defines the UI of items in lists or tables. `Bindings` sets vales in the UI based on other data.

## A short stylistic detour

Replace `Application.Resources` in `App.xaml` with:

```XAML
<Application.Resources>
    <ResourceDictionary>
        <Style TargetType="NavigationPage">
            <Setter Property="BarBackgroundColor" Value="#F9F9F9" />
        </Style>
    </ResourceDictionary>
</Application.Resources>
```

This will make the navigation bar be less ugly, at least on iOS.

### Key takeaway

Global styles are necessary and can be specified in `App.xaml`.

## Use MVVM and DataBinding to populate the UI

Add a `Models` folder and the class `Person`:

```csharp
using System;

namespace Birthdays.Models {
    public class Person {
        public Person(string name, DateTime date) {
            Name = name;
            Birthday = date.ToShortDateString();
            Age = GetAge(date);
        }

        public string Name { get; }
        public string Birthday { get; }
        public uint Age { get; }

        static uint GetAge(DateTime birthDate) {
            var now = DateTime.Now;
            int age = now.Year - birthDate.Year;
            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day)) {
                age--;
            }

            return (uint)age;
        }
    }
}

```

Add a `ViewModels` folder and the class `BirthdaysViewModel`:

```csharp
using System;
using Birthdays.Models;

namespace Birthdays.ViewModels {
    public class BirthdaysViewModel {
        public BirthdaysViewModel() {
            ClosestBirthDay = new Person("Birthday Boi", new DateTime(1984, 2, 6));
        }

        public Person ClosestBirthDay { get; }
    }
}
```

Update `ClosestBirthdayView` to bind to a people object:

```xaml
<Label Text="{Binding ClosestBirthDay.Name}" ...
<Label Text="{Binding ClosestBirthDay.Age}" ...
<Label Text="{Binding ClosestBirthDay.Birthday}" ...
```

And set the `BindingContext` of `BirthdaysPage.xaml.cs`:

```csharp
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
```

Run the app and success means it looks *kind-of the same*!

### Key takeaway

**MVVM** and **data binding** are the two most important aspects of any Xamarin Forms app. They enable decoupling, easy state managment and makes the app testable! If you remember only two things, remember these patterns.

## Bind the list too

Add an `ObserveableCollection` to `BirthdaysViewModel` and populate it with dummy data:

```csharp
using System;
using System.Collections.ObjectModel;
using Birthdays.Models;

namespace Birthdays.ViewModels {
    public class BirthdaysViewModel {
        public BirthdaysViewModel() {
            ClosestBirthDay = new Person("Birthday Boi", new DateTime(1984, 2, 6));
            FutureBirthdays = new ObservableCollection<Person> {
                new Person("Paul", new DateTime(1983, 3, 7)),
                new Person("Leto", new DateTime(1982, 4, 8)),
                new Person("Vladimir", new DateTime(1981, 5, 9)),
                new Person("Jessica", new DateTime(1980, 6, 10)),
                new Person("Duncan", new DateTime(1979, 7, 11))
            };
        }

        public Person ClosestBirthDay { get; }
        public ObservableCollection<Person> FutureBirthdays { get; }
    }
}
```

Update the `BirthdaysView` to use bindings fitting the ViewModel:

```xaml
<?xml version="1.0" encoding="UTF-8"?>
<ContentView xmlns="http://xamarin.com/schemas/2014/forms" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" x:Class="Birthdays.Views.BirthdaysView">
    <ContentView.Content>
        <ListView ItemsSource="{Binding FutureBirthdays}">
            <ListView.ItemTemplate>
                <DataTemplate>
                    <TextCell Text="{Binding Name}" Detail="{Binding Birthday}" />
                </DataTemplate>
            </ListView.ItemTemplate>
        </ListView>
    </ContentView.Content>
</ContentView>
```

### Key takeaway

**MVVM** and **data binding** are the two most important aspects of any Xamarin Forms app. They enable decoupling, easy state managment and makes the app testable! If you remember only two things, remember these patterns.

## Using a service to populate the app

Add the `NuGet-package` `Newtonsoft.Json` to the `Birthdays` project.

Create a folder called `Services` and add the class `BirthdayService`:

```csharp
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Birthdays.Models;
using Newtonsoft.Json;

namespace Birthdays.Services {
    public class BirthdayService {
        public async Task<Person[]> GetBirthdays() {
            using (var httpClient = new HttpClient {
                BaseAddress = new Uri("http://vt-ekvtool1:8081")
            }) {
                var json = await httpClient.GetStringAsync("api/birthday/trondheim");
                var birthdays = JsonConvert.DeserializeObject<Birthdays>(json);
                var persons = birthdays.TodaysBirthdays.Concat(birthdays.NextBirthdays).ToArray();
                return persons;
            }
        }

        class Birthdays {
            public Person[] TodaysBirthdays { get; set; }
            public Person[] NextBirthdays { get; set; }
        }
    }
}
```

Since the service doesn't use HTTPS, iOS at least will block the connection. We need to enable unsecure communication to this service. Open `Info.plist` from the `Birthdays.iOS` project and add the following:

```XML
<dict>
    <key>NSExceptionDomains</key>
    <dict>
        <key>vt-ekvtool1</key>
        <dict>
            <key>NSIncludesSubdomains</key>
            <true/>
            <key>NSExceptionAllowsInsecureHTTPLoads</key>
            <true/>
            <key>NSExceptionMinimumTLSVersion</key>
            <string>TLSv1.2</string>
        </dict>
    </dict>
</dict>
```

Use the service in `BirthdaysViewModel`:

```csharp
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Birthdays.Models;
using Birthdays.Services;

namespace Birthdays.ViewModels {
    public class BirthdaysViewModel {
        readonly BirthdayService birthdayService;

        public BirthdaysViewModel() {
            birthdayService = new BirthdayService();
        }

        public Person ClosestBirthDay { get; private set; }
        public ObservableCollection<Person> FutureBirthdays { get; private set; }


        public async Task FetchBirthdays() {
            try {
                var birthdays = await birthdayService.GetBirthdays();
                ClosestBirthDay = birthdays[0];
                FutureBirthdays = new ObservableCollection<Person>(birthdays.Skip(1));
            } catch (Exception) {
                // TODO: Do some error handling
            }
        }
    }
}
```

And to fetch birthdays every time `BirthdaysPage` comes into view, change `BirthdaysPage.xaml.cs` to:

```csharp
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
```

And no data should be shown on screen. One piece is cleary missing from the data binding puzzle.

### Key takeaway

`JSON` and `HTTP` is the universal language of online services. `Postman` can be used to explore APIs. `HttpClient` the C# way of making HTTP-request, `Newtonsoft.Json` is the most used JSON-parser. `async` and `await` is the best thing since slices bread. `async void` is an anti-pattern and usage should be kept to a minimum. And sometimes you need to tweak your model to incorporate the real world.

## Proper data binding

Implement `INotifyPropertyChanged` in the `BirthdaysViewModel`: 

```csharp
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Birthdays.Models;
using Birthdays.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Birthdays.ViewModels {
    public class BirthdaysViewModel : INotifyPropertyChanged {
        readonly BirthdayService birthdayService;

        public BirthdaysViewModel() {
            birthdayService = new BirthdayService();
        }

        public Person ClosestBirthDay { get; private set; }
        public ObservableCollection<Person> FutureBirthdays { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public async Task FetchBirthdays() {
            try {
                var birthdays = await birthdayService.GetBirthdays();
                ClosestBirthDay = birthdays[0];
                FutureBirthdays = new ObservableCollection<Person>(birthdays.Skip(1));
                OnPropertyChanged(nameof(ClosestBirthDay));
                OnPropertyChanged(nameof(FutureBirthdays));
            } catch (Exception) {
                // TODO: Do some error handling
            }
        }

        void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
```

Run the app and everything should work!

### Key takeaway

`INotifyPropertyChanged` is your new favourite interface. It binds the MVVM world together and is your bridge from ViewModel to View. Understand how this interface contains the event which makes the UI ask the ViewModel for new information!

## Create a screen to add birthdays

Change `SettingsPage` to:

```xaml
<?xml version="1.0" encoding="UTF-8"?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" xmlns:valueConverters="clr-namespace:Birthdays.Views.ValueConverters" x:Class="Birthdays.Views.SettingsPage" Title="Settings">
    <ContentPage.Resources>
        <ResourceDictionary>
            <valueConverters:NegateBooleanConverter x:Key="negateBooleanConverter" />
        </ResourceDictionary>
    </ContentPage.Resources>
    <ContentPage.Content>
        <StackLayout VerticalOptions="Center">
            <Label Text="Add new birthday" FontSize="32" FontAttributes="Bold" Margin="15,30,15,15" />
            <Grid Margin="15,0,15,0">
                <Grid.ColumnDefinitions>
                    <ColumnDefinition Width="Auto" />
                    <ColumnDefinition Width="*" />
                </Grid.ColumnDefinitions>
                <Grid.RowDefinitions>
                    <RowDefinition Height="Auto" />
                    <RowDefinition Height="Auto" />
                </Grid.RowDefinitions>
                <Label Text="Name" VerticalOptions="Center" HorizontalOptions="End" />
                <Entry Grid.Column="1" Placeholder="Name" Text="{Binding Name}" VerticalOptions="Center" />
                <Label Grid.Row="1" Text="Birthday" VerticalOptions="Center" HorizontalOptions="End" />
                <DatePicker Grid.Row="1" Grid.Column="1" MinimumDate="01/01/1900" MaximumDate="{Binding Today}" Date="{Binding Birthday}" VerticalOptions="Center" />
            </Grid>
            <ActivityIndicator HeightRequest="50" IsRunning="true" IsVisible="{Binding ShowButton, Converter={StaticResource negateBooleanConverter}}}" />
            <Button HeightRequest="50" Text="Save" Command="{Binding SaveCommand}" IsVisible="{Binding ShowButton}" />
        </StackLayout>
    </ContentPage.Content>
</ContentPage>
```

### Key takeaway

Different layout options can be combined to get the desired view. Use a `ValueConverter` to change how a value is interpreted in XAML.

## Bind the settings view

Create a new ViewModel, `AdminViewModel`:

```csharp
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Birthdays.ViewModels {
    public class AdminViewModel : INotifyPropertyChanged {
        string name;
        DateTime birthday;
        bool showButton;

        public AdminViewModel() {
            SaveCommand = new Command(async () => await Save(), () => !string.IsNullOrEmpty(Name));
            Today = Birthday = DateTime.Today;
            ShowButton = true;
        }

        public DateTime Today { get; }

        public string Name {
            get { return name; }
            set {
                name = value;
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        public DateTime Birthday {
            get { return birthday; }
            set {
                birthday = value;
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        public bool ShowButton {
            get { return showButton; }
            set { showButton = value; OnPropertyChanged(); }
        }

        public Command SaveCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        async Task Save() {
            try {
                ShowButton = false;
                // TODO: Use a real service
                await Task.Delay(3000);
                ShowButton = true;
            } catch (Exception) {
                // TODO: Error handling
            }
        }

        void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
```

And bind the view, creating the view model in `SettingsPage.xaml.cs`:

```csharp
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
```

### Key takeaway

Use commands to bind actions to the view. All commands using external sources must be async. Give visual feedback both during progress and on completion. Disable or hide controls that should not be used at a given time.