using System;
using System.Collections;
using System.Threading;

namespace Birthdays.Helpers {
    public class DateTimeProviderContext : IDisposable {
        readonly Stack contextStack = new Stack();

        static ThreadLocal<Stack> ThreadScopeStack = new ThreadLocal<Stack>(() => new Stack());

        public DateTime ContextDateTimeNow;

        public DateTimeProviderContext(DateTime contextDateTimeNow) {
            ContextDateTimeNow = contextDateTimeNow;
            ThreadScopeStack.Value.Push(this);
        }

        public static DateTimeProviderContext Current {
            get {
                if (ThreadScopeStack.Value.Count == 0) {
                    return null;
                }

                return (DateTimeProviderContext)ThreadScopeStack.Value.Peek();
            }
        }

        public void Dispose() => ThreadScopeStack.Value.Pop();
    }
}
