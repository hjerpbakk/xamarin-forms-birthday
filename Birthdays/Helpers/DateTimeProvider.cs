using System;
namespace Birthdays.Helpers {
    public class DateTimeProvider {
        static Lazy<DateTimeProvider> instance = new Lazy<DateTimeProvider>(() => new DateTimeProvider());

        DateTimeProvider() {
        }

        public static DateTimeProvider Instance {
            get { return instance.Value; }
        }

        Func<DateTime> _defaultCurrentFunction = () => DateTime.UtcNow;

        public DateTime Now {
            get {
                return DateTimeProviderContext.Current == null
                     ? _defaultCurrentFunction.Invoke()
                     : DateTimeProviderContext.Current.ContextDateTimeNow;
            }
        }
    }
}
