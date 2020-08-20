using System;

namespace Network.Models
{
    public class RetryModel
    {
        public TimeSpan SleepPeriod { get; set; }

        public int RetryCount { get; set; }
    }
}
