﻿using System;
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
