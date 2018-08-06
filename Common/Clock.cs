using System;

namespace NWrath.Synergy.Common
{
    public static class Clock
    {
        public static DateTime Now
        {
            get => _timeProvider.Invoke();
        }

        public static DateTime Today { get => Now.Date; }

        private static Func<DateTime> _timeProvider = () => DateTime.Now;

        public static void SetTimeProvider(Func<DateTime> provider)
        {
            _timeProvider = provider ?? (() => DateTime.Now);
        }
    }
}