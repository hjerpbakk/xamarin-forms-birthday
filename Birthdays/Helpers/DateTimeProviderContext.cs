using System;
using System.Collections;
using System.Threading;

namespace Birthdays.Helpers {
    public class DateTimeProviderContext : IDisposable {
        static readonly ThreadLocal<Stack> threadScopedStack;

        static DateTimeProviderContext()
            => threadScopedStack = new ThreadLocal<Stack>(() => new Stack());

        public DateTimeProviderContext(DateTime contextDateTimeNow) {
            ContextDateTimeNow = contextDateTimeNow;
            threadScopedStack.Value.Push(this);
        }

        public DateTime ContextDateTimeNow { get; }

        public static DateTimeProviderContext Current {
            get {
                if (threadScopedStack.Value.Count == 0) {
                    return null;
                }

                return (DateTimeProviderContext)threadScopedStack.Value.Peek();
            }
        }

        public void Dispose() => threadScopedStack.Value.Pop();
    }
}
