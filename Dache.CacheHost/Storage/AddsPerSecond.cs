using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dache.CacheHost.Storage
{
    class AddsPerSecondAttribute : CounterAttribute
    {

        public override void ApplyPerformanceCounter(Core.Performance.CustomPerformanceCounterManager manager)
        {
            manager.AddsPerSecond.RawValue++;
        }
    }
}
