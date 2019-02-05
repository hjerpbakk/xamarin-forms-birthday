using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using Xunit;

namespace Tests.Helpers {
    public class PropertyChangeTracker : IDisposable {
        readonly INotifyPropertyChanged changer;
        readonly List<string> notifications;

        public PropertyChangeTracker(INotifyPropertyChanged changer) {
            this.changer = changer;
            notifications = new List<string>();
            changer.PropertyChanged += Changer_PropertyChanged;
        }

        void Changer_PropertyChanged(object sender, PropertyChangedEventArgs e)
            => notifications.Add(e.PropertyName);

        public void Dispose()
            => changer.PropertyChanged -= Changer_PropertyChanged;

        public void VerifyNumberOfNotifications(int expected)
            => Assert.Equal(expected, notifications.Count);

        public void VerifyNotificationOfName(string propertyName, int times = 1)
            => Assert.Equal(times, notifications.Count(p => propertyName == p));
    }
}
