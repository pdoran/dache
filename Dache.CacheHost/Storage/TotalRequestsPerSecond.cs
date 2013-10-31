using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dache.CacheHost.Storage
{
    class TotalRequestsPerSecondAttribute : CounterAttribute

    {
        public override void ApplyPerformanceCounter(Core.Performance.CustomPerformanceCounterManager manager)
        {
            manager.TotalRequestsPerSecond.RawValue++;
        }
    }
}
