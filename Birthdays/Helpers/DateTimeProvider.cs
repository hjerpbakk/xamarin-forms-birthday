using System;

namespace Birthdays.Helpers {
    public class DateTimeProvider {
        static Lazy<DateTimeProvider> instance;

        static DateTimeProvider()
            => instance = new Lazy<DateTimeProvider>(() => new DateTimeProvider());

        DateTimeProvider() {
        }

        public static DateTimeProvider Instance {
            get { return instance.Value; }
        }

        public DateTime Now {
            get {
                return DateTimeProviderContext.Current == null
                     ? DateTime.Now
                     : DateTimeProviderContext.Current.ContextDateTimeNow;
            }
        }
    }
}
