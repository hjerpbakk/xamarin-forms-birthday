# xamarin-forms-birthday
Never miss a birthday again! This educational app was created as part of a Xamarin Forms course.

## Create new solution

Use `Blank Forms App` template. Update packages, build and run, it should work.

### Key takeaway

Know the difference between the different projects and understand how this can run on your phone.

## Show mock-up of the app to be created

![]()

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